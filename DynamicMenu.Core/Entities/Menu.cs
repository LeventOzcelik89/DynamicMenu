using DynamicMenu.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicMenu.Core.Entities
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public MenuTargetEnum MenuTarget { get; set; }
        public int MenuGroupId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public virtual MenuGroup MenuGroup { get; set; }
    }
}