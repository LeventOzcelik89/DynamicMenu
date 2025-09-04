using DynamicMenu.Core.Enums;

namespace DynamicMenu.API.DTOs
{
    public class UpdateMenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int MenuGroupId { get; set; }
        public MenuTargetEnum MenuTarget { get; set; }
    }
}