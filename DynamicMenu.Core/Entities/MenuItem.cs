using DynamicMenu.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicMenu.Core.Entities
{
    [Table("MenuItem")]
    public class MenuItem : BaseEntity
    {
        public int MenuBaseItemId { get; set; }
        public required string Keyword { get; set; }
        public int? Pid { get; set; }
        public bool IsNew { get; set; }
        public int SortOrder { get; set; }
        public int MenuId { get; set; }
        public int MenuGroupId { get; set; }

        public virtual MenuGroup? MenuGroup { get; set; }
        public virtual MenuBaseItem? MenuBaseItem { get; set; }
        public virtual MenuItem? Parent { get; set; }
        public virtual ICollection<MenuItem>? Children { get; set; }
    }
} 