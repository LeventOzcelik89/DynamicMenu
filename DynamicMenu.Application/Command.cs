using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System;

namespace DynamicMenu.Application

{


    public class Command
    {
        private readonly IRemoteMenusRepository _remoteMenusRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuRepository _menuRepository;

        public Command(IRemoteMenusRepository remoteMenusRepository, IMenuItemRepository menuItemRepository, IMenuRepository menuRepository)
        {
            _remoteMenusRepository = remoteMenusRepository;
            _menuItemRepository = menuItemRepository;
            _menuRepository = menuRepository;
        }

        public Command()
        {

        }

        public async Task<MenuConfigResponse> ExecuteAsync()
        {

            var clientResponse = new MenuConfigResponse();
            try
            {
                List<MenuEntityModel> EntityList = new List<MenuEntityModel>();

                RemoteConfigManager manager = new RemoteConfigManager(_remoteMenusRepository);

                EntityList = manager.GetRemoteMenuConfigList(Convert.ToInt32(menuTypeId));
                clientResponse.AllTransactionsMenu = manager.MenuCategoryModelMaker(EntityList[0].SubMenuList);
                clientResponse.CardsAccountsMenu = manager.MenuCategoryModelMaker(EntityList[1].SubMenuList);
                clientResponse.ProfileMenu = manager.MenuCategoryModelMaker(EntityList[2].SubMenuList);
                clientResponse.ApplicationsMenu = manager.MenuCategoryModelMaker(EntityList[3].SubMenuList);
                clientResponse.AssetsMenu = manager.MenuCategoryModelMaker(EntityList[4].SubMenuList);
                clientResponse.MenuCacheDate = CacheDate;

                //  AllTransactionsMenu
                var menu = await _menuRepository.AddAsync(new Menu { CreatedDate = DateTime.Now, Description = "AllTransactionsMenu", Name = "AllTransactionsMenu", IsActive = true });
                makeMenu(clientResponse.AllTransactionsMenu.FirstOrDefault().MenuList, menu.Id);

                //  CardsAccountsMenu
                menu = await _menuRepository.AddAsync(new Menu { CreatedDate = DateTime.Now, Description = "CardsAccountsMenu", Name = "CardsAccountsMenu", IsActive = true });
                makeMenu(clientResponse.CardsAccountsMenu.FirstOrDefault().MenuList, menu.Id);

                //  ProfileMenu
                menu = await _menuRepository.AddAsync(new Menu { CreatedDate = DateTime.Now, Description = "ProfileMenu", Name = "ProfileMenu", IsActive = true });
                makeMenu(clientResponse.ProfileMenu.FirstOrDefault().MenuList, menu.Id);

                //  ApplicationsMenu
                menu = await _menuRepository.AddAsync(new Menu { CreatedDate = DateTime.Now, Description = "ApplicationsMenu", Name = "ApplicationsMenu", IsActive = true });
                makeMenu(clientResponse.ApplicationsMenu.FirstOrDefault().MenuList, menu.Id);

            }
            catch (Exception ex)
            {
                cacheStatus = 2;
                var errorText = "MenuTypeCommand: " + ex + " " + ex.StackTrace;
            }

            return await Task.FromResult(clientResponse);

        }

        public void makeMenu(List<MenuModel> itemx, int menuId)
        {

            int sortOrder = 1;
            foreach (var item in itemx)
            {
                explode(item, menuId, ref sortOrder, null);
            }

        }

        public void explode(MenuModel itemx, int menuId, ref int sortOrder, int? pid = null)
        {

            var mItem = new MenuItem
            {
                SortOrder = sortOrder++,
                Text = itemx.ItemText,
                TextEn = itemx.ItemTextEn,
                Description = itemx.ItemDesc,
                DescriptionEn = itemx.ItemDescEn,
                Keyword = itemx.ItemKey,
                IconPath = itemx.ItemKey,
                NewTag = itemx.IsNew,
                CreatedDate = DateTime.Now,
                MenuId = menuId,
                Pid = pid
            };

            var mmItem = _menuItemRepository.AddAsync(mItem).Result;

            var subs = itemx.SubList.FirstOrDefault()?.MenuList;
            if (subs != null && subs.Count() > 0)
            {

                var childSortOrder = 1;
                foreach (var sub in subs)
                {
                    explode(sub, menuId, ref childSortOrder, mmItem.Id);
                }


            }

        }


    }

}