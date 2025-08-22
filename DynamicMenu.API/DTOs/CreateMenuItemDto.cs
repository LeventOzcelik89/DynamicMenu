namespace DynamicMenu.API.DTOs
{
    public class CreateMenuItemDto
    {
        public required string Keyword { get; set; }
        public int? Pid { get; set; }
        public bool IsNew { get; set; }
        public int SortOrder { get; set; }

        public int MenuId { get; set; }
        public int MenuBaseItemId { get; set; }
        public int MenuGroupId { get; set; }
    }
}