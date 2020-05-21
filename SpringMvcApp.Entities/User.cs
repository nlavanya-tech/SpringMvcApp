using System;
using System.Collections.Generic;
using System.Text;

namespace SpringMvcApp.Entities
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string ConfirmPassword { get; set; }
        public virtual string Email { get; set; }
        public virtual string Photo { get; set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
