using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Models.Entities;

namespace UsersManagement.Repository
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public UsersDbContext(DbContextOptions options):base(options)
        {

        }
    }
}
