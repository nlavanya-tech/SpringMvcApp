using SpringMvcApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpringMvcApp.BusinessLayer.Interfaces
{
  public  interface IUserServices
    {
        Task<User> RegisterAsync(User user);
        Task<int> Signin(string UserName, string Password);
        Task<User> GetUser(int UserId);
        Task<IEnumerable<User>> GetAllUserAsync();
    }
}
