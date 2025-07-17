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
        private readonly IMenuBaseItemRepository _MenuBaseItemRepository;
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
            _MenuBaseItemRepository = MenuBaseItemRepository;
            _remoteMenuConfigRepository = remoteMenuConfigRepository;
            _cacheService = cacheService;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<MenuBaseItemDto>>> GetAll()
        {
            var cacheKey = $"{CacheKeyPrefix}all";
            var cachedItems = await _cacheService.GetAsync<List<MenuBaseItemDto>>(cacheKey);

            if (cachedItems != null)
                return cachedItems;

            var items = await _MenuBaseItemRepository.GetAllAsync();
            var dtos = items.Select(MapToDto).ToList();

            await _cacheService.SetAsync(cacheKey, dtos, TimeSpan.FromMinutes(30));
            return dtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuBaseItemDto>> GetById(int id)
        {
            var cacheKey = $"{CacheKeyPrefix}{id}";
            var cachedItem = await _cacheService.GetAsync<MenuBaseItemDto>(cacheKey);

            if (cachedItem != null)
                return cachedItem;

            var item = await _MenuBaseItemRepository.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            var dto = MapToDto(item);
            await _cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(30));
            return dto;
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

            var created = await _MenuBaseItemRepository.AddAsync(menuBaseItem);
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, MapToDto(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuBaseItemDto updateDto)
        {
            var existingItem = await _MenuBaseItemRepository.GetByIdAsync(id);
            if (existingItem == null)
                return NotFound();

            existingItem.Text = updateDto.Text;
            existingItem.TextEn = updateDto.TextEn;
            existingItem.IconPath = updateDto.IconPath;
            existingItem.ModifiedDate = DateTime.UtcNow;

            await _MenuBaseItemRepository.UpdateAsync(existingItem);
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}{id}");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _MenuBaseItemRepository.DeleteAsync(id);
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}all");
            await _cacheService.RemoveAsync($"{CacheKeyPrefix}{id}");
            return NoContent();
        }

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