using DynamicMenu.Core.Entities;

namespace DynamicMenu.Core.Entities
{
    public class MenuItemRole
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }

        public MenuItem MenuItem { get; set; }
    }
} 