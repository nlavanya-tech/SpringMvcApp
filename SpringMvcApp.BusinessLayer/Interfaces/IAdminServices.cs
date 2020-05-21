using SpringMvcApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpringMvcApp.BusinessLayer.Interfaces
{
    public interface IAdminServices
    {
        Task<User> DeleteUserAsync(int UserId);
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<bool> UpdateUserAsync(User user);
        Task<User> GetUserById(int UserId);
    }
}
