namespace DynamicMenu.Core.Models
{
    public class MenuItemExport
    {
        public string parent {  get; set; }
        public string key { get; set; }
        public string text { get; set; }
        public bool isNew { get; set; }
        public string icon { get; set; }
        public int sortOrder { get; set; }
        public MenuItemExport[] items { get; set; } = null;
    }

}