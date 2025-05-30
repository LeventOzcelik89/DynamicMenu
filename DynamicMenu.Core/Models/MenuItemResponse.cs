using DynamicMenu.Core.Entities;

namespace DynamicMenu.Core.Models
{
    public class MenuItemResponse
    {
        public int id { get; set; }
        public int pid { get; set; }
        public string key { get; set; }
        public string text { get; set; }
        public string textEn { get; set; }
        public bool isNew { get; set; }
        public string icon { get; set; }
        public int sortOrder { get; set; }
        
        public MenuBaseItem MenuBaseItem { get; set; }
        public MenuItemResponse[] items { get; set; } = null;
    }

}