using System;
using System.Collections.Generic;
using System.Text;

namespace SpringMvcApp.Tests.Exceptions
{
   public class UserNotFoundExceptions : Exception
    {
        public string message= "User Not Found";

        public UserNotFoundExceptions()
        {
           
        }
        public UserNotFoundExceptions(string messages)
            {
            message = messages;
            }
    }
}



