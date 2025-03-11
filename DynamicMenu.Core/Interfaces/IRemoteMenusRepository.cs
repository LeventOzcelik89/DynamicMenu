using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Enums;

namespace DynamicMenu.Core.Interfaces
{
    public interface IRemoteMenusRepository
    {
        Task<RemoteMenuConfig> GetByIdAsync(int id);
        Task<IEnumerable<RemoteMenuConfig>> GetAllAsync();
        //Task<IEnumerable<MenuItem>> GetByAppTypeAsync(AppType appType);
        //Task<IEnumerable<MenuItem>> GetByRoleIdAsync(int roleId);
        //Task<IEnumerable<MenuItem>> GetByMenuIdAsync(int menuId);
        Task<RemoteMenuConfig> AddAsync(RemoteMenuConfig menuItem);
        Task UpdateAsync(RemoteMenuConfig menuItem);
        Task DeleteAsync(int id);
    }
} 