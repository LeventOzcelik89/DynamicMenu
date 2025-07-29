namespace DynamicMenu.API.DTOs
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public bool IsNew { get; set; }
        public string Keyword { get; set; }
        public int? Pid { get; set; }
        public string Text { get; set; }
        public string TextEn { get; set; }
        public bool NewTag { get; set; }
        public string IconPath { get; set; }
        public int SortOrder { get; set; }
        public int MenuBaseItemId { get; set; }
        public int MenuId { get; set; }
        public int MenuGroupId { get; set; }
        
        public List<MenuItemDto> Children { get; set; } = new();
    }
}