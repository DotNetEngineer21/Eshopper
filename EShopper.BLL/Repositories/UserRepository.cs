using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopper.DAL.Entities;
using EShopper.DAL.Context;
using EShopper.BLL.Models;

namespace EShopper.BLL.Repositories
{
    public class UserRepository : IUserRepository
    {
        Context db = new Context();
        public void AddUser(User objUser)
        {
            using (Context db = new Context())
            {
                User obj = new User();
                obj.UserId = Guid.NewGuid();
                obj.UserName = objUser.UserName;
                obj.Email = objUser.Email;
                obj.Password = objUser.Password;
                obj.Phone = objUser.Phone;
                obj.Address = objUser.Address;
                obj.Created = DateTime.Now;
                obj.Updated = DateTime.Now;
                db.Users.Add(obj);
                db.SaveChanges();

                UserRole objuserrole = new UserRole();
                objuserrole.UserRoleId = Guid.NewGuid();
                objuserrole.RoleId = new Guid("6D3EC4AB-7030-4DB3-8FB9-F4A51BA8AA5E");
                objuserrole.UserId =obj.UserId;                
                db.UserRoles.Add(objuserrole);
                db.SaveChanges();
            }
        }
        public UserModel checkUser(string username, string password)
        {
            using (Context db = new Context())
            {
                var user = db.Users.Where(a => a.UserName == username || a.Email==username && a.Password == password).FirstOrDefault();
                if (user != null)
                {
                    var userrole = db.UserRoles.Where(ur => ur.UserId == user.UserId).FirstOrDefault();
                    var role = db.Roles.Where(r => r.RoleId == userrole.RoleId).FirstOrDefault();
                    UserModel usermodel = new UserModel();
                    usermodel.UserId = user.UserId;
                    usermodel.UserName = user.UserName;
                    usermodel.Address = user.Address;
                    usermodel.Email = user.Email;
                    usermodel.Phone = user.Phone;
                    usermodel.RoleId = role.RoleId;
                    usermodel.RoleName = role.RoleName;
                    return usermodel;
                }
                return null;
            }
        }
    }
}
