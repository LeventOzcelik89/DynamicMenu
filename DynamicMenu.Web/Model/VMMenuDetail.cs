using static DynamicMenu.Web.Model.VMMenuDetail;

namespace DynamicMenu.Web.Model
{
    public class VMMenuDetail : VMMenuGroupDetail
    {
        public int MenuId { get; set; }
    }

    public class VMMenuGroupDetail
    {
        public int? MenuGroupId { get; set; }
    }

}
