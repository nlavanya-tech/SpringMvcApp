using SpringMvcApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpringMvcApp.BusinessLayer.Services.Repository
{
 public interface IAdminRepository
    {
       
        Task<User> DeleteUserAsync(int UserId);
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<bool> UpdateUserAsync(User user);
        Task<User> GetUserByIdAsync(int UserId);
       
    }
}
