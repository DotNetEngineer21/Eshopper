using EShopper.BLL.Models;
using EShopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.BLL.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User objUser);
        UserModel checkUser(string username, string password);
    }
}
