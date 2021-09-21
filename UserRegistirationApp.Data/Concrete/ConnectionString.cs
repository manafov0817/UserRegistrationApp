using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistirationApp.Data.Concrete
{
    public static class ConnectionString
    {
        private static string DefaultConnection { get; set; } = "Server=localhost; Port=5432; Database=UserDbContext; username=postgres; password=manafov165";
    
        public static string GetConnectionString()
        {
            return DefaultConnection;
        }
    }
}
