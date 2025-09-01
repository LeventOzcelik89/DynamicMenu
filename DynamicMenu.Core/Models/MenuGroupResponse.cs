using DynamicMenu.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMenu.Core.Models
{
    public class MenuGroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public MenuType MenuType { get; set; }
    }

    public class MenuGroupModelResponse
    {
        public MenuGroupResponse menuGroup { get; set; }
        public MenuTargetResponse menus { get; set; }
    }

    public class MenuTargetResponse
    {
        public List<MenuItemResponse>? Transactions { get; set; }
        public List<MenuItemResponse>? Cards { get; set; }
        public List<MenuItemResponse>? Profile { get; set; }
        public List<MenuItemResponse>? Applications { get; set; }
    }

}
