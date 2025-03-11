namespace DynamicMenu.Core.Models
{
    public class MenuImportModel
    {
        public List<MenuListItem> MenuList { get; set; }
    }

    public class MenuListItem
    {
        public List<SubListItem> SubList { get; set; }
        public string ItemDesc { get; set; }
        public string ItemKey { get; set; }
        public string ItemText { get; set; }
        //public int DisplayType { get; set; }
        public string PopupMessage { get; set; }
        public bool IsNew { get; set; }
        //public string CategoryName { get; set; }
    }

    public class SubListItem
    {
        public List<MenuListItem> MenuList { get; set; }
        //public string CategoryName { get; set; }
    }
} 