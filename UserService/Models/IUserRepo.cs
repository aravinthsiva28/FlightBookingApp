using System;
using System.Collections.Generic;
using UserService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public interface IUserRepo
    {
        UserModel Getuser(int id);
        IEnumerable<UserModel> GetUserList();
        void AddUser(UserModel model);
        void RemoveUser(int id);
        bool MakeAdmin(int id);

    }
}
