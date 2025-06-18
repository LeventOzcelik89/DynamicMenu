using DynamicMenu.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicMenu.Core.Entities
{
    [Table("MenuBaseItem")]
    public class MenuBaseItem
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public string TextEn { get; set; }
        public string IconPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //  public virtual ICollection<MenuItem> MenuItems { get; set; }

    }
}