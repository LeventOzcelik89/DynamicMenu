using DynamicMenu.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicMenu.Core.Entities
{
    [Table("MenuGroup")]
    public class MenuGroup : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public MenuTypeEnum MenuType { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}