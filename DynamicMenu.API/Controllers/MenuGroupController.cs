using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
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

        // Listeleme
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<MenuGroup>>> GetAll()
        {
            var menuGroups = await _menuGroupRepository.GetAllAsync();
            return Ok(menuGroups);
        }

        // Tekil Getir
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuGroup>> GetById(int id)
        {
            var menuGroup = await _menuGroupRepository.GetByIdAsync(id);
            if (menuGroup == null)
                return NotFound();
            return Ok(menuGroup);
        }

        // Ekle
        [HttpPost]
        public async Task<ActionResult<MenuGroup>> Create([FromBody] MenuGroup menuGroup)
        {
            var created = await _menuGroupRepository.AddAsync(menuGroup);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // Güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuGroup menuGroup)
        {
            if (id != menuGroup.Id)
                return BadRequest();
            await _menuGroupRepository.UpdateAsync(menuGroup);
            return NoContent();
        }

        // Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuGroupRepository.DeleteAsync(id);
            return NoContent();
        }
    }
} 