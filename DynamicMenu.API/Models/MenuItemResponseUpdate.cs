using DynamicMenu.API.DTOs;

namespace DynamicMenu.API.Models
{
    public class MenuItemResponseUpdate
    {
        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Keyword { get; set; }
        public bool IsNew { get; set; }
        public int SortOrder { get; set; }

        public UpdateMenuBaseItemDto MenuBaseItem { get; set; }
        public MenuItemResponseUpdate[] items { get; set; } = null;
    }
}
