using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
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

        // Ekle
        [HttpPost]
        public async Task<ActionResult<Menu>> Create([FromBody] Menu menu)
        {
            var created = await _menuRepository.AddAsync(menu);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // Güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Menu menu)
        {
            if (id != menu.Id)
                return BadRequest();
            await _menuRepository.UpdateAsync(menu);
            return NoContent();
        }

        // Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}