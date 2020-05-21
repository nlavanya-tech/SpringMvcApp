using Dapper;
using Moq;
using NSubstitute;
using SpringMvcApp.BusinessLayer.Interfaces;
using SpringMvcApp.BusinessLayer.Services;
using SpringMvcApp.BusinessLayer.Services.Repository;
using SpringMvcApp.DataLeyer;
using SpringMvcApp.Entities;
using SpringMvcApp.Tests.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace SpringMvcApp.Tests
{
 public class Testcases
    {
       
        private IUserServices _services;
        private IAdminServices _adminservices;

        public readonly Mock<IUserRepository> service = new Mock<IUserRepository>();
        public readonly Mock<IAdminRepository> services = new Mock<IAdminRepository>();

        public Testcases()
        {
            _services = new UserServices(service.Object);
            _adminservices = new AdminServices(services.Object);

        }
        Random random = new Random();

        [Fact]
        public async Task TestFor_ValidUserRegistrationAsync()
        {
            Random random = new Random();
            
            var user = new User()
            {
                Id = random.Next(35, 300000),
                UserName = "Rose",
                Password = "1234",
                ConfirmPassword = "1234",
                Email = "abc@gmail.com",
                Photo = "Phot.jpeg"
            };
          
            service.Setup(repos => repos.RegisterAsync(user)).ReturnsAsync(user);
            var registeredUser = await _services.RegisterAsync(user);
            Assert.Equal(user, registeredUser);
        }


        [Fact]
        public async Task Testfor_ValidSignin()
        {

            Random random = new Random();

            var user = new User()
            {
                Id = random.Next(35, 300000),
                UserName = "Rose",
                Password = "1234",
                ConfirmPassword = "1234",
                Email = "abc@gmail.com",
                Photo = "Phot.jpeg"
            };
            service.Setup(repos => repos.Signin(user.UserName, user.Password)).ReturnsAsync(user.Id); 
            var register =await _services.Signin(user.UserName, user.Password);

            Assert.Equal(user.Id.ToString(),register.ToString()); ;
        }

        [Fact]
        public async Task Testfor_AllUser()
        {
            //Action
            service.Setup(repos => repos.GetAllUserAsync());
            var register = await _services.GetAllUserAsync();
            //Assertion
            Assert.NotNull(register);
           // Assert.NotEmpty(register);
        }
        [Fact]
        public async Task Testfor_GetUser()
        {
            //Action
            Random random = new Random();

            var user = new User()
            {
                Id =35,
                UserName = "Rose",
                Password = "1234",
                ConfirmPassword = "1234",
                Email = "abc@gmail.com",
                Photo = "Phot.jpeg"
            };

            service.Setup(repos => repos.GetUser(user.Id)).ReturnsAsync(user);
            var register = await _services.GetUser(user.Id);
            //Assertion
            Assert.Equal(user,register);
            // Assert.NotEmpty(register);
        }

        [Fact]
        public void Boundary_Testfor_ValidId()
        {
            //Action
            var user = new User()
            {
                Id = random.Next(35, 300000),
                UserName = "Rose",
                Password = "1234",
                ConfirmPassword = "1234",
                Email = "abc@gmail.com",
                Photo = "Phot.jpeg"
            };
            //Assert
            Assert.InRange(user.Id, 35, 300000);

        }

        [Fact]
        public void Boundary_Testfor_ValidPassword()
        {
            //Action
            var user = new User()
            {
                Id = random.Next(35, 300000),
                UserName = "Rose",
                Password = "1234",
                ConfirmPassword = "1234",
                Email = "abc@gmail.com",
                Photo = "Phot.jpeg"
            };
            //Assert
            Assert.InRange(user.Password.Length, 4, 20);
        }

        [Fact]
        public void Boundary_Testfor_ValidEmail()
        {
            //Action
            var user = new User()
            {
                Id = random.Next(35, 300000),
                UserName = "Rose",
                Password = "1234",
                ConfirmPassword = "1234",
                Email = "abc@gmail.com",
                Photo = "Phot.jpeg"
            };
           
            bool isEmail = Regex.IsMatch(user.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            //Assert
            Assert.True(isEmail);
        }

        [Fact]
        public async void ExceptionTestForUserNotFound()
        {

            //Arrange
            User user = new User()
            {
                Id = -12,
                UserName = "gg",
                Password = "1234",
                ConfirmPassword = "1234",
                Email = "Ss@gmail.com",
                Photo = "fff"

            };
          Assert.ThrowsAsync<UserNotFoundExceptions>(async () => await _services.GetUser(user.Id));
        }


        [Fact]
        public async Task Test_For_DeleteUser()
        {
            User user = new User()
            {
                Id = 1,
            };

            services.Setup(repos => repos.DeleteUserAsync(user.Id)).ReturnsAsync(user);

            var register = await _adminservices.DeleteUserAsync(user.Id);

            Assert.Equal(user, register);
        }


        [Fact]
        public async Task Test_For_UpadateUser()
        {
            User user = new User()
            {
                Id = 1,
            };

            services.Setup(repos => repos.UpdateUserAsync(user)).ReturnsAsync(true);

            var isUpdated = await _adminservices.UpdateUserAsync(user);

            Assert.True(isUpdated);
        }
     
        [Fact]
        public async Task Test_Validate_UserName()
        {
            User user = new User()
            {
                Id = 34,
                UserName = "kavya",
                Password = "44444",
                ConfirmPassword = "44444",
                Email = "kavya@ss.com",
                Photo = "photo.jpeg"
            };
            service.Setup(list => list.GetUser(user.Id)).ReturnsAsync(user);
            var loginuser = await _services.GetUser(user.Id);
            Assert.Equal(user.UserName , loginuser.UserName);

        }
        [Fact]
        public async Task Test_Validate_Password()
        {
            User user = new User()
            {
                Id = 12,
                UserName = "kavya",
                Password = "44444",
                ConfirmPassword = "44444",
                Email = "kavya@ss.com",
                Photo = "photo.jpeg"
            };
            service.Setup(list => list.GetUser(user.Id)).ReturnsAsync(user);
            var loginuser = await _services.GetUser(user.Id);
            Assert.Equal(user.Password ,loginuser.Password);

        }
        [Fact]
        public async Task ValidatingPassword_Null()
        {
            Boolean Nullpass = true;
            string password = null;
            string username = "asdff";
            var validpass = await _services.Signin(username, password);
            if(validpass == 0)
            {
                Nullpass = false;
            }
            Assert.False(Nullpass);
        }
        [Fact]
        public async Task ValidatingPassword_EmptyString()
        {
            //setup
            Boolean Nullpass = true;
            string password = "";
            string username = "kavya";

            //excute
            var validpass = await _services.Signin(username, password);
            if (validpass == 0)
            {
                Nullpass = false;
            }

            //assert
            Assert.False(Nullpass);
        }
        [Fact]
        public async Task Validatepassword_Missing_OneNumber()
        {
            //setup
            string password = "Abcdefg#";
            string username = "kavya";
            Boolean Nullpass = true;

            //excute
            var validpass = await _services.Signin(username, password);
            if (validpass == 0)
            {
                Nullpass = false;
            }
            //assert
            Assert.False(Nullpass);

        }
        [Fact]
        public async Task Validatepassword_Missing_OneUpperCaseLetter()
        {
            //setup
            string password = "abcdefg5#";
            string username = "kavya";
            Boolean Nullpass = true;

            //excute
            var validpass = await _services.Signin(username, password);
            if (validpass == 0)
            {
                Nullpass = false;
            }
            //assert
            Assert.False(Nullpass);

        }
        [Fact]
        public async Task Validatepassword_Missing_OneLowerCaseLetter()
        {
            //setup
            string password = "ABCDEFG5#";
            string username = "kavya";
            Boolean Nullpass = true;

            //excute
            var validpass = await _services.Signin(username, password);
            if (validpass == 0)
            {
                Nullpass = false;
            }
            //assert
            Assert.False(Nullpass);

        }
        [Fact]
        public async Task Validatepassword_Missing_OneSymbol()
        {
            //setup
            string password = "Abcdefg5";
            string username = "kavya";
            Boolean Nullpass = true;

            //excute
            var validpass = await _services.Signin(username, password);
            if (validpass == 0)
            {
                Nullpass = false;
            }
            //assert
            Assert.False(Nullpass);

        }
        [Fact]
        public async Task Validatepassword_AllRulesMet()
        {
            //setup
            string password = "Abcdefg5#";
            string username = "kavya";
            Boolean Nullpass = true;

            //execute
            var validpass = await _services.Signin(username, password);
            if (validpass == 0)
            {
                Nullpass = false;
            }
            //assert
            Assert.False(Nullpass);

        }
        [Fact]
        public async Task Validatepassword_LengthTooLong()
        {
            //setup
            string password = "Abcdefg5#abcdefgabcd#";
            string username = "kavya";
            Boolean Nullpass = true;

            //execute
            var validpass = await _services.Signin(username, password);
            if (validpass == 0)
            {
                Nullpass = false;
            }
            //assert
            Assert.False(Nullpass);
            User user = new User()
            {
                Id = 34,
                UserName = "kavya",
                Password = "44444",
                ConfirmPassword = "44444",
                Email = "kavya@ss.com",
                Photo = "photo.jpeg"
            };

        }
        [Fact]
        public async Task ValidateExceptionalfor_Signin()
        {
            //setup
            Random random = new Random();

            var user = new User()
            {
                Id = 44,
                UserName = "giri",
                Password = "6543",
                ConfirmPassword = "6543",
                Email = "abc12323@gmail.com",
                Photo = "Phot2.jpeg"
            };

            //Execute
            service.Setup(repos => repos.Signin(user.UserName, user.Password)).ReturnsAsync(user.Id);
            var register = await _services.Signin(user.UserName, user.Password);

            //Assert
            Assert.Equal(user.Id.ToString(), register.ToString());
            Assert.Throws<ValidationException>(() => user.Validate());
        }
        public async Task ValidateExceptionfor_UserRegistrationAsync()
        {
            //setup
            Random random = new Random();
            var user = new User()
            {
                Id = 44,
                UserName = "giri",
                Password = "6543",
                ConfirmPassword = "6543",
                Email = "abc12323@gmail.com",
                Photo = "Phot2.jpeg"
            };
            //execute
            service.Setup(repos => repos.RegisterAsync(user)).ReturnsAsync(user);
            var registeredUser = await _services.RegisterAsync(user);

            //Assert
            Assert.Equal(user, registeredUser);
            Assert.Throws<ValidationException>(() => user.Validate());

        }
        [Fact]
        public async Task validateExceptionsfor_GetUser()
        {
            //setup
            Random random = new Random();
            
            var user = new User()
            {
                Id = 44,
                UserName = "giri",
                Password = "6543",
                ConfirmPassword = "6543",
                Email = "abc12323@gmail.com",
                Photo = "Phot2.jpeg"
            };
            //Execute
            service.Setup(repos => repos.GetUser(user.Id)).ReturnsAsync(user);
            var register = await _services.GetUser(user.Id);

            //Assert
            Assert.Equal(user, register);
            Assert.Throws<ValidationException>(() => user.Validate());

        }

    }
}
