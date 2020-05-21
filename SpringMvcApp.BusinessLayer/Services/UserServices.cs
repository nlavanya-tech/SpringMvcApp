using Dapper;
using Microsoft.EntityFrameworkCore;
using SpringMvcApp.BusinessLayer.Interfaces;
using SpringMvcApp.BusinessLayer.Services.Repository;
using SpringMvcApp.DataLeyer;

using SpringMvcApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringMvcApp.BusinessLayer.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
      //  private readonly MockContext _context;


        public UserServices(IUserRepository userRepository)
        {

            _userRepository = userRepository;
    
        }


        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            try
            {
               var user=await _userRepository.GetAllUserAsync();
                return user;
            }
            catch (Exception ex)
            { 
                throw (ex);
            }

        }

        public async Task<User> RegisterAsync(User user)
            {
                 try
                {

                var users = await _userRepository.RegisterAsync(user);
                return users;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }


        public async Task<int> Signin(string UserName, string Password)
        {
            try
            {

                var id =await _userRepository.Signin(UserName, Password);
                return id;
                
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public Task<User> GetUser(int UserId)
        {
            try
            {

                var user = _userRepository.GetUser(UserId);
                return user;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
