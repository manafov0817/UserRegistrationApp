using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistirationApp.Entity.Entities;

namespace UserRegistirationApp.Data.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        bool ExsistsByUserName(string name);
        bool ExsistsById(int userId);
    }
}
