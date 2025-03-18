using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Enums;

namespace DynamicMenu.Core.Interfaces
{
    public interface IMenuRepository
    {
        Task<Menu> GetByIdAsync(int id);
        Task<IEnumerable<Menu>> GetByMenuGroupIdAsync(int id);
        Task<IEnumerable<Menu>> GetAllAsync();
        Task<Menu> AddAsync(Menu menuItem);
        Task UpdateAsync(Menu menuItem);
        Task DeleteAsync(int id);
    }
} 