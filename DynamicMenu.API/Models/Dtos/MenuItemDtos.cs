namespace DynamicMenu.API.Models.Dtos
{
    public class UpdateMenuItemDto
    {
        public string Keyword { get; set; }
        public int MenuBaseItemId { get; set; }
        public int? Pid { get; set; }
        public string Text { get; set; }
        public string TextEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        //public bool DisplayType { get; set; }
        //public byte AppId { get; set; }
        public bool NewTag { get; set; }
        public string IconPath { get; set; }
        public int SortOrder { get; set; }
    }

    public class CreateMenuItemWithChildrenDto
    {
        public string Keyword { get; set; }
        public string Text { get; set; }
        public int? ParentId { get; set; }
        //public bool DisplayType { get; set; }
        public bool NewTag { get; set; }
        public int? MenuId { get; set; }
    }
}