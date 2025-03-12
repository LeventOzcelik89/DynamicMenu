using DynamicMenu.API.DTOs;
using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Enums;
using DynamicMenu.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using DynamicMenu.Infrastructure.Helpers;
using DynamicMenu.API.Models.Dtos;
using DynamicMenu.Application;
using DynamicMenu.Infrastructure.Repositories;
using DynamicMenu.Core.Models;
using Newtonsoft;

namespace DynamicMenu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRemoteMenusRepository _remoteMenuConfigRepository;
        private readonly DynamicMenuDbContext _context;

        public PresentController(
            IMenuItemRepository menuItemRepository,
            IMenuRepository menuRepository,
            IRemoteMenusRepository remoteMenuConfigRepository,
            DynamicMenuDbContext context)
        {
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _remoteMenuConfigRepository = remoteMenuConfigRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemExport>>> GetAll(int menuId)
        {

            var menu = await _menuRepository.GetByIdAsync(menuId);
            if (menu == null)
            {
                return NotFound();
            }

            var res = new List<MenuItemExport>();
            var items = await _menuItemRepository.GetByMenuIdAsync(menu.Id);
            foreach (var item in items.Where(a => a.Pid == null).ToArray())
            {

                res.Add(new MenuItemExport
                {
                    key = item.Keyword,
                    text = item.Text,
                    icon = item.IconPath,
                    isNew = item.NewTag,
                    sortOrder = item.SortOrder,
                    items = GetMenuItems(items.ToArray(), item)
                });


            }

            return res;
        }

        private MenuItemExport[] GetMenuItems(MenuItem[] items, MenuItem item)
        {

            var itemss = items.Where(a => a.Pid == item.Id).ToArray();
            if (!itemss.Any())
            {
                return null;
            }

            var res = new List<MenuItemExport>();
            foreach (var subItem in itemss)
            {

                res.Add(new MenuItemExport
                {
                    key = subItem.Keyword,
                    text = subItem.Text,
                    icon = subItem.IconPath,
                    isNew = subItem.NewTag,
                    sortOrder = subItem.SortOrder,
                    items = GetMenuItems(items, subItem)
                });

            }

            return res.ToArray();

        }


        //public void makeMenu(List<MenuItem> items)
        //{

        //    int sortOrder = 1;
        //    foreach (var item in items)
        //    {
        //        explode(items, item);
        //    }

        //}

        //public void explode(List<MenuItem> items, MenuItem item)
        //{

        //    var childs = items.Where(a => a.Pid == item.Id).ToArray();

        //    foreach

        //    var mItem = new MenuItem
        //    {
        //        SortOrder = sortOrder++,
        //        Text = itemx.ItemText,
        //        TextEn = itemx.ItemTextEn,
        //        Description = itemx.ItemDesc,
        //        DescriptionEn = itemx.ItemDescEn,
        //        Keyword = itemx.ItemKey,
        //        IconPath = itemx.ItemKey,
        //        NewTag = itemx.IsNew,
        //        CreatedDate = DateTime.Now,
        //        MenuId = menuId,
        //        Pid = pid
        //    };

        //    var mmItem = _menuItemRepository.AddAsync(mItem).Result;

        //    var subs = itemx.SubList.FirstOrDefault()?.MenuList;
        //    if (subs != null && subs.Count() > 0)
        //    {

        //        var childSortOrder = 1;
        //        foreach (var sub in subs)
        //        {
        //            explode(sub, menuId, ref childSortOrder, mmItem.Id);
        //        }


        //    }

        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<MenuItemDto>> GetById(int id)
        //{
        //    var cacheKey = $"{CacheKeyPrefix}{id}";
        //    var cachedItem = await _cacheService.GetAsync<MenuItemDto>(cacheKey);

        //    if (cachedItem != null)
        //        return cachedItem;

        //    var item = await _menuItemRepository.GetByIdAsync(id);
        //    if (item == null)
        //        return NotFound();

        //    var dto = MapToDto(item);
        //    await _cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(30));
        //    return dto;
        //}

        //[HttpGet("by-app/{appId}")]
        //public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetByAppType(byte appId)
        //{
        //    var items = await _menuItemRepository.GetByAppTypeAsync((AppType)appId);
        //    return items.Select(MapToDto).ToList();
        //}

        ////[HttpGet("by-role/{roleId}")]
        ////public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetByRole(int roleId)
        ////{
        ////    var items = await _menuItemRepository.GetByRoleIdAsync(roleId);
        ////    return items.Select(MapToDto).ToList();
        ////}

        //[HttpGet("by-menuId/{menuId}")]
        //public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetByRole(int menuId)
        //{
        //    var items = await _menuItemRepository.GetByMenuIdAsync(menuId);
        //    return items.Select(MapToDto).ToList();
        //}

        //[HttpPost]
        //public async Task<ActionResult<MenuItemDto>> Create(CreateMenuItemDto createDto)
        //{
        //    var menuItem = new MenuItem
        //    {
        //        Keyword = createDto.Keyword,
        //        Pid = createDto.Pid,
        //        Text = createDto.Text,
        //        TextEn = createDto.TextEn,
        //        Description = createDto.Description,
        //        DescriptionEn = createDto.DescriptionEn,
        //        //DisplayType = createDto.DisplayType,
        //        //AppId = (AppType)createDto.AppId,
        //        NewTag = createDto.NewTag,
        //        IconPath = createDto.IconPath,
        //        SortOrder = createDto.SortOrder,
        //        CreatedDate = DateTime.UtcNow,
        //        //MenuItemRoles = createDto.RoleIds.Select(roleId => new MenuItemRole { RoleId = roleId }).ToList()
        //    };

        //    var created = await _menuItemRepository.AddAsync(menuItem);
        //    await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");

        //    return CreatedAtAction(
        //        nameof(GetById),
        //        new { id = created.Id },
        //        MapToDto(created));
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuItemDto updateDto)
        //{
        //    var existingItem = await _menuItemRepository.GetByIdAsync(id);
        //    if (existingItem == null)
        //        return NotFound();

        //    if (updateDto.Pid.HasValue && updateDto.Pid != existingItem.Pid)
        //    {
        //        var hasCircularDependency = await CheckCircularDependency(id, updateDto.Pid.Value);
        //        if (hasCircularDependency)
        //            return BadRequest("Circular dependency detected");
        //    }

        //    existingItem.Pid = updateDto.Pid;
        //    existingItem.Keyword = updateDto.Keyword;
        //    existingItem.Text = updateDto.Text;
        //    existingItem.TextEn = updateDto.TextEn;
        //    existingItem.Description = updateDto.Description;
        //    existingItem.DescriptionEn = updateDto.DescriptionEn;
        //    //existingItem.DisplayType = updateDto.DisplayType;
        //    //existingItem.AppId = (AppType)updateDto.AppId;
        //    existingItem.NewTag = updateDto.NewTag;
        //    existingItem.IconPath = updateDto.IconPath;
        //    existingItem.SortOrder = updateDto.SortOrder;
        //    existingItem.ModifiedDate = DateTime.UtcNow;

        //    //existingItem.MenuItemRoles = updateDto.RoleIds
        //    //    .Select(roleId => new MenuItemRole { RoleId = roleId })
        //    //    .ToList();

        //    await _menuItemRepository.UpdateAsync(existingItem);
        //    await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
        //    await _cacheService.RemoveAsync($"{CacheKeyPrefix}{id}");

        //    return NoContent();
        //}

        //private async Task<bool> CheckCircularDependency(int sourceId, int targetPid)
        //{
        //    var current = await _menuItemRepository.GetByIdAsync(targetPid);
        //    while (current?.Pid != null)
        //    {
        //        if (current.Pid == sourceId)
        //            return true;
        //        current = await _menuItemRepository.GetByIdAsync(current.Pid.Value);
        //    }
        //    return false;
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _menuItemRepository.DeleteAsync(id);
        //    await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
        //    await _cacheService.RemoveAsync($"{CacheKeyPrefix}{id}");
        //    return NoContent();
        //}

        //[HttpPost("import")]
        //public async Task<IActionResult> ImportMenu(IFormFile jsonFile)
        //{
        //    if (jsonFile == null || jsonFile.Length == 0)
        //        return BadRequest("No file uploaded");

        //    using var reader = new StreamReader(jsonFile.OpenReadStream());
        //    var jsonContent = await reader.ReadToEndAsync();

        //    var helper = new MenuImportHelper(_context);
        //    await helper.ImportMenuFromJson(jsonContent);

        //    await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");

        //    return Ok("Menu imported successfully");
        //}

        //[HttpPut("reorder")]
        //public async Task<IActionResult> ReorderMenuItems([FromBody] List<MenuItem> items)
        //{
        //    foreach (var item in items)
        //    {
        //        var existingItem = await _menuItemRepository.GetByIdAsync(item.Id);
        //        if (existingItem != null)
        //        {
        //            existingItem.SortOrder = item.SortOrder;
        //            await _menuItemRepository.UpdateAsync(existingItem);
        //        }
        //    }

        //    await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
        //    return Ok();
        //}

        //[HttpPost("{id}/duplicate")]
        //public async Task<ActionResult<MenuItemDto>> DuplicateMenuItem(int id)
        //{
        //    var sourceItem = await _menuItemRepository.GetByIdAsync(id);
        //    if (sourceItem == null)
        //        return NotFound();

        //    var duplicate = new MenuItem
        //    {
        //        Keyword = $"{sourceItem.Keyword}_copy",
        //        Text = $"{sourceItem.Text} (Kopya)",
        //        TextEn = sourceItem.TextEn,
        //        Description = sourceItem.Description,
        //        DescriptionEn = sourceItem.DescriptionEn,
        //        //DisplayType = sourceItem.DisplayType,
        //        //AppId = sourceItem.AppId,
        //        NewTag = sourceItem.NewTag,
        //        IconPath = sourceItem.IconPath,
        //        SortOrder = sourceItem.SortOrder + 1,
        //        Pid = sourceItem.Pid,
        //        MenuId = sourceItem.MenuId,
        //        CreatedDate = DateTime.UtcNow
        //    };

        //    var created = await _menuItemRepository.AddAsync(duplicate);

        //    //// Rolleri kopyala
        //    //foreach (var role in sourceItem.MenuItemRoles)
        //    //{
        //    //    created.MenuItemRoles.Add(new MenuItemRole
        //    //    {
        //    //        RoleId = role.RoleId
        //    //    });
        //    //}

        //    await _menuItemRepository.UpdateAsync(created);
        //    await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");

        //    return MapToDto(created);
        //}

        //[HttpPost("create-with-children")]
        //public async Task<ActionResult<MenuItemDto>> CreateWithChildren([FromBody] CreateMenuItemWithChildrenDto dto)
        //{
        //    var menuItem = new MenuItem
        //    {
        //        Keyword = dto.Keyword,
        //        Text = dto.Text,
        //        Pid = dto.ParentId,
        //        //DisplayType = dto.DisplayType,
        //        NewTag = dto.NewTag,
        //        MenuId = dto.MenuId ?? 1,
        //        CreatedDate = DateTime.UtcNow
        //    };

        //    var created = await _menuItemRepository.AddAsync(menuItem);
        //    await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");

        //    return MapToDto(created);
        //}

        //[HttpPost("seed")]
        //public async Task<IActionResult> SeedTestData()
        //{
        //    var testMenu = new List<MenuItem>
        //    {
        //        new MenuItem
        //        {
        //            Keyword = "dashboard",
        //            Text = "Ana Sayfa",
        //            //DisplayType = true,
        //            SortOrder = 0,
        //            CreatedDate = DateTime.UtcNow,
        //            //CategoryName = "Main",
        //            //MenuItemRoles = new List<MenuItemRole> { new() { RoleId = 1 } }
        //        },
        //        new MenuItem
        //        {
        //            Keyword = "transactions",
        //            Text = "İşlemler",
        //            //DisplayType = true,
        //            SortOrder = 1,
        //            CreatedDate = DateTime.UtcNow,
        //            //CategoryName = "Transactions",
        //            //MenuItemRoles = new List<MenuItemRole> { new() { RoleId = 1 } },
        //            Children = new List<MenuItem>
        //            {
        //                new()
        //                {
        //                    Keyword = "PT0101",
        //                    Text = "Para Transferi",
        //                    //DisplayType = true,
        //                    SortOrder = 0,
        //                    NewTag = true,
        //                    CreatedDate = DateTime.UtcNow,
        //                    //CategoryName = "Transfers",
        //                    //MenuItemRoles = new List<MenuItemRole> { new() { RoleId = 1 } }
        //                },
        //                new()
        //                {
        //                    Keyword = "OD0108",
        //                    Text = "Ödemeler",
        //                    //DisplayType = true,
        //                    SortOrder = 1,
        //                    CreatedDate = DateTime.UtcNow,
        //                    //MenuItemRoles = new List<MenuItemRole> { new() { RoleId = 1 } }
        //                }
        //            }
        //        },
        //        new MenuItem
        //        {
        //            Keyword = "settings",
        //            Text = "Ayarlar",
        //            //DisplayType = true,
        //            SortOrder = 2,
        //            CreatedDate = DateTime.UtcNow,
        //            //MenuItemRoles = new List<MenuItemRole> { new() { RoleId = 1 } }
        //        }
        //    };

        //    foreach (var item in testMenu)
        //    {
        //        await _menuItemRepository.AddAsync(item);
        //    }

        //    await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
        //    return Ok("Test data seeded successfully");
        //}

        ////  json file'ları import etmek için method.
        //[HttpPost("import-new")]
        //public async Task<IActionResult> ImportNew()
        //{
        //    await new Command(_remoteMenuConfigRepository, _menuItemRepository, _menuRepository).ExecuteAsync();

        //    return Ok("Test data seeded successfully");
        //}

        //private static MenuItemDto MapToDto(MenuItem item)
        //{
        //    if (item == null) return null;

        //    return new MenuItemDto
        //    {
        //        Id = item.Id,
        //        Keyword = item.Keyword ?? string.Empty,
        //        Pid = item.Pid,
        //        Text = item.Text ?? item.Keyword ?? string.Empty,
        //        TextEn = item.TextEn ?? item.Text ?? item.Keyword ?? string.Empty,
        //        Description = item.Description ?? string.Empty,
        //        DescriptionEn = item.DescriptionEn ?? string.Empty,
        //        //DisplayType = item.DisplayType,
        //        //AppId = (byte)item.AppId,
        //        NewTag = item.NewTag,
        //        IconPath = item.IconPath ?? string.Empty,
        //        SortOrder = item.SortOrder,
        //        //RoleIds = item.MenuItemRoles?.Select(r => r.RoleId).ToList() ?? new List<int>(),
        //        Children = item.Children?.Select(MapToDto).Where(x => x != null).ToList() ?? new List<MenuItemDto>()
        //    };
        //}
    }
}