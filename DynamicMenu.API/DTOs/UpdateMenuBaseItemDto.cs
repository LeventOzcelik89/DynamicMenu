namespace DynamicMenu.API.DTOs
{
    public class UpdateMenuBaseItemDto
    {
        public UpdateMenuBaseItemDto() { }
        public UpdateMenuBaseItemDto(int id, string text, string textEn, string iconPath)
        {
            this.Id = id;
            this.Text = text;
            this.TextEn = textEn;
            this.IconPath = iconPath;
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public string TextEn { get; set; }
        public string IconPath { get; set; }
    }
}