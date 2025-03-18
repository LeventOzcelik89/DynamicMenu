using DynamicMenu.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicMenu.Core.Entities
{
    [Table("MenuGroup")]
    public class MenuGroup
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public MenuType MenuType { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}