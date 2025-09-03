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
    public class MenuBaseItemController : ControllerBase
    {
        private readonly IMenuBaseItemRepository _menuBaseItemRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRemoteMenusRepository _remoteMenuConfigRepository;
        private readonly ICacheService _cacheService;
        private const string CacheKeyPrefix = "menu_";
        private readonly DynamicMenuDbContext _context;

        public MenuBaseItemController(
            IMenuBaseItemRepository MenuBaseItemRepository,
            IMenuRepository menuRepository,
            IRemoteMenusRepository remoteMenuConfigRepository,
            ICacheService cacheService,
            DynamicMenuDbContext context)
        {
            _menuRepository = menuRepository;
            _menuBaseItemRepository = MenuBaseItemRepository;
            _remoteMenuConfigRepository = remoteMenuConfigRepository;
            _cacheService = cacheService;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<MenuBaseItemDto>>> GetAll()
        {
            //var cacheKey = $"{CacheKeyPrefix}all";
            //var cachedItems = await _cacheService.GetAsync<List<MenuBaseItemDto>>(cacheKey);

            //if (cachedItems != null)
            //    return cachedItems;

            var items = await _menuBaseItemRepository.GetAllAsync();
            var dtos = items.Select(MapToDto).ToList();

            //await _cacheService.SetAsync(cacheKey, dtos, TimeSpan.FromMinutes(30));
            return dtos;
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert([FromBody] CreateMenuBaseItemDto item)
        {
            var dto = new MenuBaseItem
            {
                CreatedDate = DateTime.Now,
                IconPath = item.IconPath,
                Text = item.Text,
                TextEn = item.TextEn
            };
            var res = await _menuBaseItemRepository.AddAsync(dto);
            return Ok(new ResultStatus<MenuBaseItem> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = res, success = true });
        }

        [HttpPost("Update")]
        public async Task<ActionResult> Update([FromBody] UpdateMenuBaseItemDto item)
        {
            //  var dbItem = await _menuBaseItemRepository.GetByIdAsync(item.Id);
            var dto = new MenuBaseItem
            {
                Id = item.Id,
                IconPath = item.IconPath,
                Text = item.Text,
                TextEn = item.TextEn
            };
            var res = await _menuBaseItemRepository.UpdateAsync(dto);
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "Güncelleme işlemi tamamlandı" }, objects = res, success = true });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _menuBaseItemRepository.DeleteAsync(id);
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "Silme işlemi tamamlandı" }, objects = result, success = true });
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var cacheKey = $"{CacheKeyPrefix}{id}";
            //var cachedItem = await _cacheService.GetAsync<MenuBaseItemDto>(cacheKey);

            //if (cachedItem != null)
            //    return cachedItem;

            var item = await _menuBaseItemRepository.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            var dto = new MenuBaseItemDto
            {
                Id = item.Id,
                IconPath = item.IconPath,
                Text = item.Text,
                TextEn = item.TextEn,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate
            };
            //await _cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(30));
            return Ok(new ResultStatus<MenuBaseItemDto> { feedback = new FeedBack { message = "Silme işlemi tamamlandı" }, objects = dto, success = true });
        }

        //  todo Buraya gelmeden bir adım önce MenuBaseItem Add yapmamız gerekiyor.
        [HttpPost]
        public async Task<ActionResult<MenuBaseItemDto>> Create(CreateMenuBaseItemDto createDto)
        {
            var menuBaseItem = new MenuBaseItem
            {
                Text = createDto.Text,
                TextEn = createDto.TextEn,
                IconPath = createDto.IconPath,
                ModifiedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
            };

            var created = await _menuBaseItemRepository.AddAsync(menuBaseItem);
            //await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, MapToDto(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuBaseItemDto updateDto)
        {
            var existingItem = await _menuBaseItemRepository.GetByIdAsync(id);
            if (existingItem == null)
                return NotFound();

            existingItem.Text = updateDto.Text;
            existingItem.TextEn = updateDto.TextEn;
            existingItem.IconPath = updateDto.IconPath;
            existingItem.ModifiedDate = DateTime.UtcNow;

            await _menuBaseItemRepository.UpdateAsync(existingItem);
            //await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
            //await _cacheService.RemoveAsync($"{CacheKeyPrefix}{id}");

            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _menuBaseItemRepository.DeleteAsync(id);
        //    //await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
        //    //await _cacheService.RemoveAsync($"{CacheKeyPrefix}{id}");
        //    return NoContent();
        //}

        private static MenuBaseItemDto MapToDto(MenuBaseItem item)
        {
            if (item == null) return null;

            return new MenuBaseItemDto
            {
                Id = item.Id,
                IconPath = item.IconPath,
                Text = item.Text,
                TextEn = item.TextEn,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate
            };
        }

    }
}