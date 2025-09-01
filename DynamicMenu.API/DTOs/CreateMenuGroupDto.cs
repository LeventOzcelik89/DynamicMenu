using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Enums;

namespace DynamicMenu.API.DTOs
{
    public class CreateMenuGroupDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public MenuTypeEnum MenuType { get; set; }
    }
}