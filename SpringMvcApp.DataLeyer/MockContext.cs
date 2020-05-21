using Microsoft.EntityFrameworkCore;
using SpringMvcApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringMvcApp.DataLeyer
{
   public class MockContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasKey(pf => pf.Id);
            builder.Entity<User>().Property(pf => pf.UserName).HasMaxLength(50);
            builder.Entity<User>().Property(pf => pf.Password).HasMaxLength(20);
            builder.Entity<User>().Property(pf => pf.ConfirmPassword).HasMaxLength(20);
            builder.Entity<User>().Property(pf => pf.Email).HasMaxLength(20);
            builder.Entity<User>().Property(pf => pf.Photo).HasMaxLength(20);
        }

        public DbSet<User> Users { get; set; }
    }
}
