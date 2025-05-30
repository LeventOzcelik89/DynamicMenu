using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Enums;

namespace DynamicMenu.Core.Interfaces
{
    public interface IMenuBaseItemRepository
    {
        Task<MenuBaseItem> GetByIdAsync(int id);
        Task<IEnumerable<MenuBaseItem>> GetAllAsync();
        Task<MenuBaseItem> AddAsync(MenuBaseItem MenuBaseItem);
        Task UpdateAsync(MenuBaseItem MenuBaseItem);
        Task DeleteAsync(int id);
    }
} 