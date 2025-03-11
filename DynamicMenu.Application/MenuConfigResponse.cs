namespace DynamicMenu.Application
{

    public class NBMenuEntityModel
    {
        public string ItemId { get; set; }
        public List<NBMenuEntityModel> SubMenuList { get; set; }
        public NBMenuEntityModel Footer { get; set; }
        public string ItemKey { get; set; }
        public string FooterId { get; set; }
        public string CustomerType { get; set; }
        public short Display { get; set; }
        public short NewTag { get; set; }
        public short RequiresActivation { get; set; }
        public string ItemText { get; set; }
        public string ItemDesc { get; set; }
        public short ItemType { get; set; }
        public short NonbankingType { get; set; }
        public string PopupMessage { get; set; }
        public string SubMenu { get; set; }
        public string IconName { get; set; }
        public NBMenuEntityModel()
        {
            FooterId = string.Empty;
            ItemId = string.Empty;
            ItemKey = string.Empty;
            CustomerType = string.Empty;
            ItemText = string.Empty;
            ItemDesc = string.Empty;
            PopupMessage = string.Empty;
            SubMenuList = new List<NBMenuEntityModel>();
            SubMenu = string.Empty;
            IconName = string.Empty;
        }
    }

    public class MenuEntityModel
    {
        public string ItemId { get; set; }
        public List<MenuEntityModel> SubMenuList { get; set; }
        public string ItemKey { get; set; }
        public string RoleId { get; set; }
        public short Display { get; set; }
        public short NewTag { get; set; }
        public string ItemText { get; set; }
        public string ItemTextEn { get; set; }
        public string ItemDesc { get; set; }
        public string ItemDescEn { get; set; }
        public short ItemType { get; set; }
        public string PopupMessage { get; set; }
        public string PopupMessageEn { get; set; }
        public string SubMenu { get; set; }
        public MenuEntityModel()
        {
            ItemId = string.Empty;
            ItemKey = string.Empty;
            RoleId = string.Empty;
            ItemText = string.Empty;
            ItemDesc = string.Empty;
            PopupMessage = string.Empty;
            SubMenuList = new List<MenuEntityModel>();
            SubMenu = string.Empty;
        }
    }


    public class MenuConfigResponse

    {

        public List<MenuCategoryModel> AllTransactionsMenu { get; set; }

        public List<MenuCategoryModel> CardsAccountsMenu { get; set; }

        public List<MenuCategoryModel> ProfileMenu { get; set; }

        public List<MenuCategoryModel> ApplicationsMenu { get; set; }

        public List<MenuCategoryModel> AssetsMenu { get; set; }

        public string MenuCacheDate { get; set; }


        public MenuConfigResponse()

        {

            AllTransactionsMenu = new List<MenuCategoryModel>();

            CardsAccountsMenu = new List<MenuCategoryModel>();

            ProfileMenu = new List<MenuCategoryModel>();

            ApplicationsMenu = new List<MenuCategoryModel>();

            AssetsMenu = new List<MenuCategoryModel>();

        }

    }

    public class MenuModel : BaseMenuModel
    {
        public List<MenuCategoryModel> SubList { get; set; }
        public string ItemDesc { get; set; }
        public string ItemDescEn { get; set; }


        public MenuModel()
        {
            SubList = new List<MenuCategoryModel>();
            IsNew = false;
            PopupMessage = string.Empty;
            ItemText = string.Empty;
            ItemDesc = string.Empty;
        }
    }
    public class MenuCategoryModel : BaseMenuCategoryModel
    {
        public List<MenuModel> MenuList { get; set; }
    }

    public class BaseMenuModel
    {
        public string ItemKey { get; set; } // And-Ios Ortak key (barkod) 
        public string ItemText { get; set; } // Culture 'a göre dönmektedir.
        public string ItemTextEn { get; set; } // Culture 'a göre dönmektedir.
        public short DisplayType { get; set; } // 0 gizle 1 göster 2 pasif
        public string PopupMessage { get; set; } //DisplayType 2 ise tıklanınca Pop-up bas  
        public string PopupMessageEn { get; set; } //DisplayType 2 ise tıklanınca Pop-up bas  
        public bool IsNew { get; set; }
    }
    public class BaseMenuCategoryModel
    {
        public string CategoryName { get; set; }
    }

}
