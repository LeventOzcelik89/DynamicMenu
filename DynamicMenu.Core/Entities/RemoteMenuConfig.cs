using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMenu.Core.Entities
{
    public partial class RemoteMenuConfig
    {
        public int ID { get; set; }
        public string ItemId { get; set; }
        public string? SubMenu { get; set; }
        public string? ItemText { get; set; }
        public string? ItemEnText { get; set; }
        public string? ItemDesc { get; set; }
        public string? ItemEnDesc { get; set; }
        public short DisplayType { get; set; }
        public string? PopUpMessage { get; set; }
        public string? PopUpMessageEn { get; set; }
        public short ItemType { get; set; }
        public string? ItemKey { get; set; }
        public string RoleId { get; set; }
        public string AppId { get; set; }
        public short NewTag { get; set; }
        public short SpecialMobileRole { get; set; }
    }
}
