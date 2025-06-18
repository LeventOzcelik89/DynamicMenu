using DynamicMenu.API.DTOs;
using DynamicMenu.API.Models.Dtos;
using DynamicMenu.Application;
using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Enums;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Models;
using DynamicMenu.Infrastructure.Data;
using DynamicMenu.Infrastructure.Helpers;
using DynamicMenu.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DynamicMenu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuBaseItemRepository _menuBaseItemRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRemoteMenusRepository _remoteMenuConfigRepository;
        private readonly ICacheService _cacheService;
        private const string CacheKeyPrefix = "menu_";
        private readonly DynamicMenuDbContext _context;

        public MenuItemController(
            IMenuItemRepository menuItemRepository,
            IMenuRepository menuRepository,
            IRemoteMenusRepository remoteMenuConfigRepository,
            ICacheService cacheService,
            DynamicMenuDbContext context)
        {
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _remoteMenuConfigRepository = remoteMenuConfigRepository;
            _cacheService = cacheService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAll()
        {
            var cacheKey = $"{CacheKeyPrefix}all";
            var cachedItems = await _cacheService.GetAsync<List<MenuItemDto>>(cacheKey);

            if (cachedItems != null)
                return cachedItems;

            var items = await _menuItemRepository.GetAllAsync();
            var dtos = items.Select(MapToDto).ToList();

            await _cacheService.SetAsync(cacheKey, dtos, TimeSpan.FromMinutes(30));
            return dtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetById(int id)
        {
            var cacheKey = $"{CacheKeyPrefix}{id}";
            var cachedItem = await _cacheService.GetAsync<MenuItemDto>(cacheKey);

            if (cachedItem != null)
                return cachedItem;

            var item = await _menuItemRepository.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            var dto = MapToDto(item);
            await _cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(30));
            return dto;
        }

        [HttpGet("by-app/{appId}")]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetByAppType(byte appId)
        {
            var items = await _menuItemRepository.GetByAppTypeAsync((AppType)appId);
            return items.Select(MapToDto).ToList();
        }

        //[HttpGet("by-role/{roleId}")]
        //public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetByRole(int roleId)
        //{
        //    var items = await _menuItemRepository.GetByRoleIdAsync(roleId);
        //    return items.Select(MapToDto).ToList();
        //}

        [HttpGet("by-menuId/{menuId}")]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetByRole(int menuId)
        {
            var items = await _menuItemRepository.GetByMenuGroupIdAsync(menuId);
            return items.Select(MapToDto).ToList();
        }

        //  todo Buraya gelmeden bir adım önce MenuBaseItem Add yapmamız gerekiyor.
        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create(CreateMenuItemDto createDto)
        {

            var menuBaseItem = await _menuBaseItemRepository.GetByIdAsync(createDto.MenuBaseItemId);

            var menuItem = new MenuItem
            {
                Keyword = createDto.Keyword,
                Pid = createDto.Pid,
                //  Text = createDto.Text,
                //  TextEn = createDto.TextEn,
                //DisplayType = createDto.DisplayType,
                //AppId = (AppType)createDto.AppId,
                IsNew = createDto.IsNew,
                //  IconPath = createDto.IconPath,
                SortOrder = createDto.SortOrder,
                CreatedDate = DateTime.UtcNow,
                MenuBaseItemId = createDto.MenuBaseItemId,
                //MenuItemRoles = createDto.RoleIds.Select(roleId => new MenuItemRole { RoleId = roleId }).ToList()
            };

            var created = await _menuItemRepository.AddAsync(menuItem);
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                MapToDto(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuItemDto updateDto)
        {
            var existingItem = await _menuItemRepository.GetByIdAsync(id);
            if (existingItem == null)
                return NotFound();

            if (updateDto.Pid.HasValue && updateDto.Pid != existingItem.Pid)
            {
                var hasCircularDependency = await CheckCircularDependency(id, updateDto.Pid.Value);
                if (hasCircularDependency)
                    return BadRequest("Circular dependency detected");
            }

            existingItem.Pid = updateDto.Pid;
            existingItem.Keyword = updateDto.Keyword;
            existingItem.MenuBaseItemId = updateDto.MenuBaseItemId;
            //  existingItem.Text = updateDto.Text;
            //  existingItem.TextEn = updateDto.TextEn;
            existingItem.IsNew = updateDto.NewTag;
            //  existingItem.IconPath = updateDto.IconPath;
            existingItem.SortOrder = updateDto.SortOrder;
            existingItem.ModifiedDate = DateTime.UtcNow;

            //existingItem.MenuItemRoles = updateDto.RoleIds
            //    .Select(roleId => new MenuItemRole { RoleId = roleId })
            //    .ToList();

            await _menuItemRepository.UpdateAsync(existingItem);
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}{id}");

            return NoContent();
        }

        private async Task<bool> CheckCircularDependency(int sourceId, int targetPid)
        {
            var current = await _menuItemRepository.GetByIdAsync(targetPid);
            while (current?.Pid != null)
            {
                if (current.Pid == sourceId)
                    return true;
                current = await _menuItemRepository.GetByIdAsync(current.Pid.Value);
            }
            return false;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuItemRepository.DeleteAsync(id);
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}{id}");
            return NoContent();
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportMenu(IFormFile jsonFile)
        {
            if (jsonFile == null || jsonFile.Length == 0)
                return BadRequest("No file uploaded");

            using var reader = new StreamReader(jsonFile.OpenReadStream());
            var jsonContent = await reader.ReadToEndAsync();

            var helper = new MenuImportHelper(_context);
            await helper.ImportMenuFromJson(jsonContent);

            await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");

            return Ok("Menu imported successfully");
        }

        [HttpPut("reorder")]
        public async Task<IActionResult> ReorderMenuItems([FromBody] List<MenuItem> items)
        {
            foreach (var item in items)
            {
                var existingItem = await _menuItemRepository.GetByIdAsync(item.Id);
                if (existingItem != null)
                {
                    existingItem.SortOrder = item.SortOrder;
                    await _menuItemRepository.UpdateAsync(existingItem);
                }
            }

            await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
            return Ok();
        }

        [HttpPost("{id}/duplicate")]
        public async Task<ActionResult<MenuItemDto>> DuplicateMenuItem(int id)
        {
            var sourceItem = await _menuItemRepository.GetByIdAsync(id);
            if (sourceItem == null)
                return NotFound();

            var duplicate = new MenuItem
            {
                Keyword = $"{sourceItem.Keyword}_copy",
                //  Text = $"{sourceItem.Text} (Kopya)",
                //  TextEn = sourceItem.TextEn,
                IsNew = sourceItem.IsNew,
                //  IconPath = sourceItem.IconPath,
                SortOrder = sourceItem.SortOrder + 1,
                Pid = sourceItem.Pid,
                MenuBaseItemId = sourceItem.MenuBaseItemId,
                MenuGroupId = sourceItem.MenuGroupId,
                CreatedDate = DateTime.UtcNow
            };

            var created = await _menuItemRepository.AddAsync(duplicate);

            //// Rolleri kopyala
            //foreach (var role in sourceItem.MenuItemRoles)
            //{
            //    created.MenuItemRoles.Add(new MenuItemRole
            //    {
            //        RoleId = role.RoleId
            //    });
            //}

            await _menuItemRepository.UpdateAsync(created);
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");

            return MapToDto(created);
        }

        [HttpPost("create-with-children")]
        public async Task<ActionResult<MenuItemDto>> CreateWithChildren([FromBody] CreateMenuItemWithChildrenDto dto)
        {
            var menuItem = new MenuItem
            {
                Keyword = dto.Keyword,
                //  Text = dto.Text,
                Pid = dto.ParentId,
                //DisplayType = dto.DisplayType,
                IsNew = dto.NewTag,
                MenuGroupId = dto.MenuId ?? 1,
                CreatedDate = DateTime.UtcNow
            };

            var created = await _menuItemRepository.AddAsync(menuItem);
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");

            return MapToDto(created);
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedTestData()
        {
            var testMenu = new List<MenuItem>
            {
                new MenuItem
                {
                    Keyword = "dashboard",
                    //  Text = "Ana Sayfa",
                    //DisplayType = true,
                    SortOrder = 0,
                    CreatedDate = DateTime.UtcNow,
                    //CategoryName = "Main",
                    //MenuItemRoles = new List<MenuItemRole> { new() { RoleId = 1 } }
                },
                new MenuItem
                {
                    Keyword = "transactions",
                    //  Text = "İşlemler",
                    //DisplayType = true,
                    SortOrder = 1,
                    CreatedDate = DateTime.UtcNow,
                    //CategoryName = "Transactions",
                    //MenuItemRoles = new List<MenuItemRole> { new() { RoleId = 1 } },
                    Children = new List<MenuItem>
                    {
                        new()
                        {
                            Keyword = "PT0101",
                            //  Text = "Para Transferi",
                            //DisplayType = true,
                            SortOrder = 0,
                            IsNew = true,
                            CreatedDate = DateTime.UtcNow,
                            //CategoryName = "Transfers",
                            //MenuItemRoles = new List<MenuItemRole> { new() { RoleId = 1 } }
                        },
                        new()
                        {
                            Keyword = "OD0108",
                            //  Text = "Ödemeler",
                            //DisplayType = true,
                            SortOrder = 1,
                            CreatedDate = DateTime.UtcNow,
                            //MenuItemRoles = new List<MenuItemRole> { new() { RoleId = 1 } }
                        }
                    }
                },
                new MenuItem
                {
                    Keyword = "settings",
                    //  Text = "Ayarlar",
                    //DisplayType = true,
                    SortOrder = 2,
                    CreatedDate = DateTime.UtcNow,
                    //MenuItemRoles = new List<MenuItemRole> { new() { RoleId = 1 } }
                }
            };

            foreach (var item in testMenu)
            {
                await _menuItemRepository.AddAsync(item);
            }

            await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
            return Ok("Test data seeded successfully");
        }

        //  json file'ları import etmek için method.
        [HttpPost("import-new")]
        public async Task<IActionResult> ImportNew()
        {
            await new Command(_remoteMenuConfigRepository, _menuItemRepository, _menuRepository).ExecuteAsync();

            return Ok("Test data seeded successfully");
        }

        private static MenuItemDto MapToDto(MenuItem item)
        {
            if (item == null) return null;

            return new MenuItemDto
            {
                Id = item.Id,
                Keyword = item.Keyword ?? string.Empty,
                Pid = item.Pid,
                MenuBaseItemId = item.MenuBaseItemId,
                //  Text = item.Text ?? item.Keyword ?? string.Empty,
                //  TextEn = item.TextEn ?? item.Text ?? item.Keyword ?? string.Empty,
                //DisplayType = item.DisplayType,
                //AppId = (byte)item.AppId,
                NewTag = item.IsNew,
                //  IconPath = item.IconPath ?? string.Empty,
                //  SortOrder = item.SortOrder,
                //RoleIds = item.MenuItemRoles?.Select(r => r.RoleId).ToList() ?? new List<int>(),
                Children = item.Children?.Select(MapToDto).Where(x => x != null).ToList() ?? new List<MenuItemDto>()
            };
        }








        [HttpGet("GetMenuItemsByMenu/{menuGroupId}/{menuId}")]
        public async Task<ActionResult<List<MenuItemResponse>?>> GetMenuItemsByMenu(int menuGroupId, int menuId)
        {
            var res = await GetMenu(menuGroupId, menuId);
            return res;
        }

        private async Task<List<MenuItemResponse>?> GetMenu(int menuGroupId, int menuId)
        {

            var menuItems = new List<MenuItemResponse>();
            var items = await _menuItemRepository.GetByMenuGroupIdMenuIdAsync(menuGroupId, menuId);
            foreach (var item in items.Where(a => a.Pid == null).OrderBy(a => a.SortOrder).ToArray())
            {
                menuItems.Add(new MenuItemResponse
                {
                    id = item.Id,
                    key = item.Keyword,
                    text = item.MenuBaseItem.Text,
                    textEn = item.MenuBaseItem.TextEn,
                    icon = item.MenuBaseItem.IconPath,
                    isNew = item.IsNew,
                    items = GetMenuItems(items.ToArray(), item)
                });
            }

            return menuItems;

        }

        private MenuItemResponse[] GetMenuItems(MenuItem[] items, MenuItem item)
        {

            var itemss = items.Where(a => a.Pid == item.Id).OrderBy(a => a.SortOrder).ToArray();
            if (!itemss.Any())
            {
                return null;
            }

            var res = new List<MenuItemResponse>();
            foreach (var subItem in itemss)
            {

                res.Add(new MenuItemResponse
                {
                    id = subItem.Id,
                    pid = item.Id,
                    key = subItem.Keyword,
                    text = subItem.MenuBaseItem.Text,
                    textEn = subItem.MenuBaseItem.TextEn,
                    icon = subItem.MenuBaseItem.IconPath,
                    isNew = subItem.IsNew,
                    items = GetMenuItems(items, subItem)
                });

            }

            return res.ToArray();

        }

    }
}