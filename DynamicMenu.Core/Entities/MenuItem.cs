using DynamicMenu.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace DynamicMenu.Core.Entities
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        public string Keyword { get; set; }
        public int? Pid { get; set; }
        public string Text { get; set; }
        public string TextEn { get; set; }
        //public bool DisplayType { get; set; }
        //public AppType AppId { get; set; }
        public bool NewTag { get; set; }
        public string IconPath { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        //public string CategoryName { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }

        public virtual MenuItem Parent { get; set; }
        public virtual ICollection<MenuItem> Children { get; set; }
        //public ICollection<MenuItemRole> MenuItemRoles { get; set; }
    }
} 