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
    public class MenuGroupController : ControllerBase
    {
        private readonly IMenuGroupRepository _menuGroupRepository;

        public MenuGroupController(IMenuGroupRepository menuGroupRepository)
        {
            _menuGroupRepository = menuGroupRepository;
        }


        [HttpGet("GetAll")]
        public async Task<ResultStatus<IEnumerable<MenuGroup>>> GetAll(int? MenuGroupId = null)
        {
            var menuGroups = await _menuGroupRepository.GetAllAsync();
            return ResultStatus<IEnumerable<MenuGroup>>.Success(menuGroups, "");
        }


        [HttpPost("Insert")]
        public async Task<ResultStatus<MenuGroup>> Insert([FromBody] CreateMenuGroupDto item)
        {
            var dto = new MenuGroup
            {
                CreatedDate = DateTime.Now,
                Description = item.Description,
                IsActive = item.IsActive,
                MenuType = item.MenuType,
                Name = item.Name
            };
            var res = await _menuGroupRepository.AddAsync(dto);
            return ResultStatus<MenuGroup>.Success(dto);
        }

        [HttpPost("Update")]
        public async Task<ResultStatus<bool>> Update([FromBody] UpdateMenuGroupDto item)
        {
            //  API giderek işletmemiz gerekecek.
            var dto = new MenuGroup
            {
                Id = item.Id,
                Description = item.Description,
                IsActive = item.IsActive,
                MenuType = item.MenuType,
                Name = item.Name,
                ModifiedDate = DateTime.Now
            };
            var res = await _menuGroupRepository.UpdateAsync(dto);
            return ResultStatus<bool>.Success(res);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ResultStatus<bool>> Delete(int id)
        {
            var result = await _menuGroupRepository.DeleteAsync(id);
            return ResultStatus<bool>.Success(result, "Silme işlemi tamamlandı.");
        }

        [HttpGet("GetById/{id}")]
        public async Task<ResultStatus<MenuGroupDto>> GetById(int id)
        {
            //var cacheKey = $"{CacheKeyPrefix}{id}";
            //var cachedItem = await _cacheService.GetAsync<MenuBaseItemDto>(cacheKey);

            //if (cachedItem != null)
            //    return cachedItem;

            var item = await _menuGroupRepository.GetByIdAsync(id);
            if (item == null)
            {
                return ResultStatus<MenuGroupDto>.Error("İstenilen kayıt bulunamadı.");
            }

            var dto = new MenuGroupDto
            {
                Id = item.Id,
                Description = item.Description,
                IsActive = item.IsActive,
                MenuType = item.MenuType,
                Name = item.Name,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate,
            };
            //await _cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(30));
            return ResultStatus<MenuGroupDto>.Success(dto);
        }

    }
}