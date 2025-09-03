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

        // Listeleme
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetAll(int? MenuGroupId = null)
        {
            var menus = MenuGroupId.HasValue
                ? await _menuRepository.GetByMenuGroupIdAsync(MenuGroupId.Value)
                : await _menuRepository.GetAllAsync();

            return Ok(menus);
        }

        // Listeleme
        [HttpGet("GetByMenuGroup/{id}")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetByMenuGroup(int id)
        {
            var menus = await _menuRepository.GetByMenuGroupIdAsync(id);
            return Ok(menus);
        }


        // Tekil Getir
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetById(int id)
        {
            var menu = await _menuRepository.GetByIdAsync(id);
            if (menu == null)
                return NotFound();
            return Ok(menu);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] CreateMenuDto item)
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
            return Ok(new ResultStatus<Menu> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = res });
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateMenuDto item)
        {
            //  API giderek işletmemiz gerekecek.
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
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = res });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _menuRepository.DeleteAsync(id);
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "Silme işlemi tamamlandı" }, objects = res });
        }

    }
}