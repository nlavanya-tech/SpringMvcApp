using Dapper;
using SpringMvcApp.DataLeyer;

using SpringMvcApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpringMvcApp.BusinessLayer.Services.Repository
{
   public class UserRepository : IUserRepository
    {
       
        private readonly IDbConnectionFactory _dbConnectionFactory;
     
        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;

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

        public async Task<User> RegisterAsync(User user)
        {
            try
            {
               

                var connection = await _dbConnectionFactory.CreateConnectionAsync();
              
                await connection.ExecuteAsync("Insert into User (Id, UserName, Password, ConfirmPassword, Email, Photo) VALUES (@id, @userName,@password,@confirmPassword,@email,@photo)", new { id = user.Id, userName = user.UserName, password = user.Password, confirmPassword = user.ConfirmPassword, email = user.Email, photo = user.Photo });
                return user;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<int> Signin(string userName, string password)
        {
             String VALID_PASSWORD_REGEX = "((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%=:\\?]).{8,12})";
            try
            {
                bool passpat = Regex.IsMatch(password, VALID_PASSWORD_REGEX);
                if (userName != null && password != null && passpat)
                {
                    var ids = 0;
                    var connection =  await _dbConnectionFactory.CreateConnectionAsync();
                    var user= await connection.QuerySingleOrDefaultAsync("Select * from User Where UserName=@UserName and Password=@Password", new { UserName = userName, Password = password });
                    if (int.Parse(user.Id))
                    {
                        ids = int.Parse(user.Id);
                    }
                              
                    return ids;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<User> GetUser(int UserId)
        {
            var connection = await _dbConnectionFactory.CreateConnectionAsync();
           var user= await connection.QuerySingleOrDefaultAsync<User>("select * from User where Id=@id", new { id = UserId});
           
            return user;
           
        }




    }
}
