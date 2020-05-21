using SpringMvcApp.BusinessLayer.Interfaces;
using SpringMvcApp.BusinessLayer.Services.Repository;
using SpringMvcApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpringMvcApp.BusinessLayer.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly IAdminRepository _adminRepository;

        public AdminServices(IAdminRepository repository)
        {

            _adminRepository = repository;

        }
        public async Task<User> DeleteUserAsync(int UserId)
        {
            var user=await _adminRepository.DeleteUserAsync(UserId);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            try
            {
                var user = await _adminRepository.GetAllUserAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Task<User> GetUserById(int UserId)
        {
            try
            {
                var user = _adminRepository.GetUserByIdAsync(UserId);
                return user;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                var isupdated  = await _adminRepository.UpdateUserAsync(user);
                return isupdated;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
