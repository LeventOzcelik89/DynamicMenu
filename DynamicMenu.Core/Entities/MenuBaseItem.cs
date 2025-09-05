using DynamicMenu.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicMenu.Core.Entities
{
    [Table("MenuBaseItem")]
    public class MenuBaseItem : BaseEntity
    {
        public string? Text { get; set; }
        public string? TextEn { get; set; }
        public string? IconPath { get; set; }
    }
}