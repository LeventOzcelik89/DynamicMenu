namespace DynamicMenu.API.DTOs
{
    public class CreateMenuItemDto
    {
        public int MenuBaseItemId { get; set; }
        public required string Keyword { get; set; }
        //          public string? Text { get; set; }
        //          public string? TextEn { get; set; }
        //          public string? IconPath { get; set; }

        public int? Pid { get; set; }
        public bool IsNew { get; set; }
        public int SortOrder { get; set; }
    }
}