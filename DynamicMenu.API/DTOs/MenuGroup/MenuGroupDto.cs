using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Enums;

namespace DynamicMenu.API.DTOs
{
    public class MenuGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public MenuTypeEnum MenuType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}