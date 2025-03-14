using DynamicMenu.API.DTOs;
using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Enums;
using DynamicMenu.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using DynamicMenu.Infrastructure.Helpers;
using DynamicMenu.API.Models.Dtos;
using DynamicMenu.Application;
using DynamicMenu.Infrastructure.Repositories;
using DynamicMenu.Core.Models;
using Newtonsoft;

namespace DynamicMenu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRemoteMenusRepository _remoteMenuConfigRepository;
        private readonly DynamicMenuDbContext _context;

        public DevController(
            IMenuItemRepository menuItemRepository,
            IMenuRepository menuRepository,
            IRemoteMenusRepository remoteMenuConfigRepository,
            DynamicMenuDbContext context)
        {
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _remoteMenuConfigRepository = remoteMenuConfigRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<bool>> Rename()
        {

            var files = System.IO.Directory.GetFiles(@"d:\icons");
            foreach (var file in files)
            {
                System.IO.File.Move(file, file.ToString().Replace("icons", @"icons\renamed").Replace("@2x", ""));
            }

            return true;

        }

    }
}