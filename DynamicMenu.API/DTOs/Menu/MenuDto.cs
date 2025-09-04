using DynamicMenu.Core.Enums;

namespace DynamicMenu.API.DTOs
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int MenuGroupId { get; set; }
        public MenuTargetEnum MenuTarget { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}