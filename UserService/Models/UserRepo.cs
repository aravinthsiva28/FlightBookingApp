using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class UserRepo : IUserRepo
    {
        private DataContext _userContext;

        public UserRepo(DataContext userContext)
        {
            _userContext = userContext;
        }

        
        public UserModel Getuser(int id)
        {
            try
            {
                UserModel user = _userContext.User.FirstOrDefault(x => x.Id == id);

                return user;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public IEnumerable<UserModel> GetUserList()
        {
            return _userContext.User.ToList();
        }

        public void AddUser(UserModel model)
        {
            try
            {
                _userContext.User.Add(model);
                _userContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void RemoveUser(int id)
        {
            try
            {
                UserModel user = _userContext.User.FirstOrDefault(x => x.Id == id);
                if(user != null)
                {
                    _userContext.User.Remove(user);
                    _userContext.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       

        public bool MakeAdmin(int id)
        {
            try
            {
                UserModel user = _userContext.User.FirstOrDefault(x => x.Id == id);

                if (user != null)
                {
                    _userContext.User.Update(user);
                    user.IsAdmin = true;
                    _userContext.SaveChanges();

                    return true;
                }

                return false;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
