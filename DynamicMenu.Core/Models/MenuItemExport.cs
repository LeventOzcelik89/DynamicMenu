namespace DynamicMenu.Core.Models
{
    public class MenuItemExport
    {
        public int id { get; set; }
        public int pid { get; set; }
        public string key { get; set; }
        public string text { get; set; }
        public string textEn { get; set; }
        public bool isNew { get; set; }
        public string icon { get; set; }
        public int sortOrder { get; set; }
        public MenuItemExport[] items { get; set; } = null;
    }

}