using DynamicMenu.Core.Interfaces;
using DynamicMenu.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMenu.Application
{
    public class RemoteConfigManager
    {


        private readonly IRemoteMenusRepository _remoteMenusRepository;

        public RemoteConfigManager(IRemoteMenusRepository remoteMenusRepository)
        {
            _remoteMenusRepository = remoteMenusRepository;
        }

        #region Non Banking Login Menu

        public List<NBMenuEntityModel> RemoteNBConfigListMaker(List<NBMenuEntityModel> entitiList, string[] itemid)

        {

            List<NBMenuEntityModel> subMenuItemList = new List<NBMenuEntityModel>();

            List<NBMenuEntityModel> EntityList = new List<NBMenuEntityModel>();

            EntityList = entitiList.GetRange(0, entitiList.Count);

            for (int i = 0; i < itemid.Length; i++)

            {

                subMenuItemList.Add(EntityList.FirstOrDefault(x => x.ItemId.Equals(itemid[i])));

                string[] parsedData = null;

                if (subMenuItemList.Count > 0)

                {

                    if (subMenuItemList[i] != null)

                    {

                        if (!String.IsNullOrEmpty(subMenuItemList[i].FooterId))

                        {

                            subMenuItemList[i].Footer = EntityList.FirstOrDefault(x => x.ItemId.Equals(subMenuItemList[i].FooterId));

                        }

                        if (!String.IsNullOrEmpty(subMenuItemList[i].SubMenu))

                        {

                            parsedData = subMenuItemList[i].SubMenu.Split(',');

                            subMenuItemList[i].SubMenuList = RemoteNBConfigListMaker(EntityList, parsedData);

                        }

                    }

                }

            }

            subMenuItemList.RemoveAll(item => item == null);

            return subMenuItemList;

        }

        #endregion

        #region After Login Menu


        public List<MenuEntityModel> GetRemoteMenuConfigList(int menuTypeId)
        {

            List<MenuEntityModel> remoteConfigClientItemsList = new List<MenuEntityModel>();
            List<MenuEntityModel> entitiList = new List<MenuEntityModel>();

            entitiList = GetRemoteConfigListFromDB(menuTypeId);

            MenuEntityModel firstCall = new MenuEntityModel();

            firstCall = entitiList.FirstOrDefault(x => x.ItemId.Equals("First"));

            string[] parsedData = null;

            parsedData = firstCall.SubMenu.Split(',');

            remoteConfigClientItemsList = RemoteConfigListMaker(entitiList, parsedData);

            return remoteConfigClientItemsList;
        }

        public List<MenuEntityModel> GetRemoteConfigListFromDB(int menuTypeId)
        {

            var list = _remoteMenusRepository.GetAllAsync().Result.Select(t => new MenuEntityModel
            {
                ItemId = t.ItemId,
                ItemKey = t.ItemKey,
                Display = t.DisplayType,
                NewTag = t.NewTag,
                SubMenu = t.SubMenu,
                ItemText = t.ItemText,
                ItemTextEn = t.ItemEnText,
                ItemDesc = t.ItemDesc,
                ItemDescEn = t.ItemEnDesc,
                PopupMessage = t.PopUpMessage,
                PopupMessageEn = t.PopUpMessageEn,
                ItemType = t.ItemType,
                RoleId = t.RoleId,
            }).ToList();

            return list;

        }

        public List<MenuCategoryModel> MenuCategoryModelMaker(List<MenuEntityModel> entitiList)

        {

            //List<RemoteNBConfigClientItemModel> subMenuItemList = new List<RemoteNBConfigClientItemModel>();

            List<MenuEntityModel> EntityList = new List<MenuEntityModel>();

            List<MenuCategoryModel> menuList = new List<MenuCategoryModel>();

            if (entitiList.Count == 0)

            {

                return menuList;

            }

            EntityList = entitiList.GetRange(0, entitiList.Count);

            for (int i = 0; i < EntityList.Count; i++)

            {

                MenuCategoryModel CategoryModel = new MenuCategoryModel();

                if (EntityList[i].SubMenuList.Count > 0)

                {
                    CategoryModel.MenuList = MenuModelMaker(EntityList[i].SubMenuList);
                }

                menuList.Add(CategoryModel);

            }

            return menuList;

        }

        public List<MenuModel> MenuModelMaker(List<MenuEntityModel> entitiList)
        {
            List<MenuEntityModel> EntityList = new List<MenuEntityModel>();
            List<MenuModel> menuList = new List<MenuModel>();
            EntityList = entitiList.GetRange(0, entitiList.Count);
            for (int i = 0; i < EntityList.Count; i++)
            {
                MenuModel menuModel = new MenuModel();

                menuModel.DisplayType = EntityList[i].Display;
                menuModel.ItemKey = EntityList[i].ItemKey;
                menuModel.ItemText = EntityList[i].ItemText?.Replace("\\n", "\n");
                menuModel.ItemTextEn = EntityList[i].ItemTextEn?.Replace("\\n", "\n");
                menuModel.ItemDesc = EntityList[i].ItemDesc?.Replace("\\n", "\n");
                menuModel.ItemDescEn = EntityList[i].ItemDescEn?.Replace("\\n", "\n");
                menuModel.PopupMessage = EntityList[i].PopupMessage;
                menuModel.PopupMessageEn = EntityList[i].PopupMessageEn;
                menuModel.IsNew = EntityList[i].NewTag == 1 ? true : false;


                if (EntityList[i].SubMenuList.Count > 0)
                {
                    menuModel.SubList = MenuCategoryModelMaker(EntityList[i].SubMenuList);
                }

                menuList.Add(menuModel);
            }
            return menuList;
        }

        public List<MenuEntityModel> RemoteConfigListMaker(List<MenuEntityModel> entitiList, string[] itemid)

        {

            List<MenuEntityModel> subMenuItemList = new List<MenuEntityModel>();

            List<MenuEntityModel> EntityList = new List<MenuEntityModel>();

            EntityList = entitiList.GetRange(0, entitiList.Count);

            for (int i = 0; i < itemid.Length; i++)

            {

                subMenuItemList.Add(EntityList.FirstOrDefault(x => x.ItemId.Equals(itemid[i])));

                string[] parsedData = null;

                if (subMenuItemList.Count > 0)

                {

                    if (subMenuItemList[i] != null)

                    {

                        if (!String.IsNullOrEmpty(subMenuItemList[i].SubMenu))

                        {

                            parsedData = subMenuItemList[i].SubMenu.Split(',');

                            subMenuItemList[i].SubMenuList = RemoteConfigListMaker(EntityList, parsedData);

                        }

                    }

                }

            }

            subMenuItemList.RemoveAll(item => item == null);

            return subMenuItemList;

        }

        #endregion

    }

}

