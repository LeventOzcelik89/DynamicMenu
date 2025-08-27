using DynamicMenu.API.DTOs;
using DynamicMenu.Core.Entities;

namespace DynamicMenu.API.Models
{
    public class MenuItemProcessDTO
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
