namespace DynamicMenu.API.DTOs
{
    public class MenuBaseItemDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string TextEn { get; set; }
        public string IconPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}