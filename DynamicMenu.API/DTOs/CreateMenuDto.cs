using DynamicMenu.Core.Enums;

namespace DynamicMenu.API.DTOs
{
    public class CreateMenuDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int MenuGroupId { get; set; }
        public MenuTargetEnum MenuTarget { get; set; }
    }
}