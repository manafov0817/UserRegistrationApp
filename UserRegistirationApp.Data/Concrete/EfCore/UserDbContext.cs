using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistirationApp.Entity.Entities.MigrationEntities;

namespace UserRegistirationApp.Data.Concrete.EfCore
{
    public class UserDbContext:DbContext
    {
        public UserDbContext()
        {

        }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString.GetConnectionString());
        }
    }
}
