using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Enums;

namespace DynamicMenu.Core.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<MenuItem> GetByIdAsync(int id);
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<IEnumerable<MenuItem>> GetAllHierarchicalAsync();
        Task<IEnumerable<MenuItem>> GetByAppTypeAsync(AppType appType);
        //Task<IEnumerable<MenuItem>> GetByRoleIdAsync(int roleId);
        Task<IEnumerable<MenuItem>> GetByMenuGroupIdAsync(int menuGroupId);
        Task<IEnumerable<MenuItem>> GetByMenuIdAsync(int menuId);
        Task<IEnumerable<MenuItem>> GetByMenuGroupIdMenuIdAsync(int menuGroupId, int menuId);
        Task<IEnumerable<MenuItem>> GetByMenuIdsAsync(IEnumerable<int> menuId);
        Task<MenuItem> AddAsync(MenuItem menuItem);
        Task<bool> UpdateAsync(MenuItem menuItem);
        Task DeleteAsync(int id);
    }
} 