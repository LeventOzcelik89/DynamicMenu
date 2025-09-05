using DynamicMenu.API.DTOs;
using DynamicMenu.Core.Entities;

namespace DynamicMenu.API.Models
{

    //  int menuId, int menuGroupId, MenuItemProcessDTO[] items

    public class MenuItemProcessDTO
    {
        public int menuId { get; set; }
        public int menuGroupId { get; set; }
        public MenuItemProcessItems[] items { get; set; }
    }

    public class MenuItemProcessItems
    {
        public MenuItemResponseUpdate menuItem { get; set; }
        public MenuItemProcessType processType { get; set; }
    }

    public enum MenuItemProcessType
    {
        add = 1,
        edit = 2,
        remove = 3,
    }

}
