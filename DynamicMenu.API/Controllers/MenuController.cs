using DynamicMenu.API.DTOs;
using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Models;
using DynamicMenu.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DynamicMenu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ResultStatus<IEnumerable<Menu>>> GetAll(int? MenuGroupId = null)
        {
            var menus = MenuGroupId.HasValue
                ? await _menuRepository.GetByMenuGroupIdAsync(MenuGroupId.Value)
                : await _menuRepository.GetAllAsync();

            return ResultStatus<IEnumerable<Menu>>.Success(menus);
        }


        [HttpPost("Insert")]
        public async Task<ResultStatus<Menu>> Insert([FromBody] CreateMenuDto item)
        {
            var dto = new Menu
            {
                CreatedDate = DateTime.Now,
                Description = item.Description,
                IsActive = item.IsActive,
                MenuTarget = item.MenuTarget,
                MenuGroupId = item.MenuGroupId,
                Name = item.Name
            };
            var res = await _menuRepository.AddAsync(dto);
            return ResultStatus<Menu>.Success(dto, "Kayıt işlemi tamamlandı.");
        }

        [HttpPost("Update")]
        public async Task<ResultStatus<bool>> Update([FromBody] UpdateMenuDto item)
        {
            var dto = new Menu
            {
                Id = item.Id,
                Description = item.Description,
                IsActive = item.IsActive,
                Name = item.Name,
                ModifiedDate = DateTime.Now,
                MenuGroupId = item.MenuGroupId,
                MenuTarget = item.MenuTarget
            };
            var res = await _menuRepository.UpdateAsync(dto);
            return ResultStatus<bool>.Success(res, "Güncelleme işlemi tamamlandı.");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ResultStatus<bool>> Delete(int id)
        {
            var result = await _menuBaseItemRepository.DeleteAsync(id);
            return ResultStatus<bool>.Success(result, "Silme işlemi tamamlandı.");
        }

        [HttpGet("GetById/{id}")]
        public async Task<ResultStatus<MenuDto>> GetById(int id)
        {
            //var cacheKey = $"{CacheKeyPrefix}{id}";
            //var cachedItem = await _cacheService.GetAsync<MenuBaseItemDto>(cacheKey);

            //if (cachedItem != null)
            //    return cachedItem;

            var item = await _menuRepository.GetByIdAsync(id);
            if (item == null)
            {
                return ResultStatus<MenuDto>.Error("İstenilen kayıt bulunamadı.");
            }

            var dto = new MenuDto
            {
                Id = item.Id,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate,

                Name = item.Name,
                Description = item.Description,
                IsActive = item.IsActive,
                MenuGroupId = item.MenuGroupId,
            };
            //await _cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(30));
            return ResultStatus<MenuDto>.Success(dto);
        }

    }
}