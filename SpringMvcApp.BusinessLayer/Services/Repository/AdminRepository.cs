using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SpringMvcApp.DataLeyer;
using SpringMvcApp.Entities;

namespace SpringMvcApp.BusinessLayer.Services.Repository
{
    public class AdminRepository : IAdminRepository
    {

        private readonly IDbConnectionFactory _dbConnectionFactory;

        public AdminRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;

        }

        public async Task<User> DeleteUserAsync(int UserId)
        {
            try
            {
                var connection = await _dbConnectionFactory.CreateConnectionAsync();
                return await connection.QuerySingleOrDefaultAsync<User>("delete * from User where Id=@id",new { id = UserId });
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            try
            {
                var connection = await _dbConnectionFactory.CreateConnectionAsync();
                return await connection.QueryAsync<User>("select * from User");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<User> GetUserByIdAsync(int UserId)
        {
            var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var user = await connection.QuerySingleOrDefaultAsync<User>("select * from User where Id=@id", new { id = UserId });
            return user;

        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                if(user != null)
                {
                    var connection = await _dbConnectionFactory.CreateConnectionAsync();
                    await connection.ExecuteAsync("update User set  UserName=@userName,Password=@password,ConfirmPassword=@confirmPassword,Email=@email,Photo=@photo", new {  userName = user.UserName, password = user.Password, confirmPassword = user.ConfirmPassword, email = user.Email, photo = user.Photo });
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
