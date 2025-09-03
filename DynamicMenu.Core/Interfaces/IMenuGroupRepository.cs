using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Enums;

namespace DynamicMenu.Core.Interfaces
{
    public interface IMenuGroupRepository
    {
        Task<MenuGroup> GetByIdAsync(int id);
        Task<IEnumerable<MenuGroup>> GetAllAsync();
        Task<MenuGroup> AddAsync(MenuGroup menuItem);
        Task<bool> UpdateAsync(MenuGroup menuItem);
        Task<bool> DeleteAsync(int id);
    }
} 