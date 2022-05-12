using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;
using UserService.RabbitMQ;

namespace UserService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserRepo _user;
        private IMessageSender _messageSender;
        public UsersController(IUserRepo user, IMessageSender messageSender)
        {
           _user = user;
            _messageSender = messageSender;

        }

        //GET api/users/UserList
        [HttpGet]
        [Route("UserList")]
        public IEnumerable<UserModel> GetUserList()
        {
            return _user.GetUserList();
        }

        //get api/users/usersearch/{id}
        [HttpGet("{id}")]
        [Route("User/{id}")]
        public UserModel GetUserList(int id)
        {
            return _user.Getuser(id);
        }

        [HttpPost]
        [Route("RegisterUser")]
        public void PostUser([FromBody] UserModel model)
        {
            _user.AddUser(model);
        }


        [HttpDelete]
        [Route("DeregisterUser")]
        public void DeleteUser(int id)
        {
            _user.RemoveUser(id);
        }

        [HttpPut]
        [Route("AdminUser/{id}")]
        public IActionResult MaKeAdmin(int id)
        {
            bool result =_user.MakeAdmin(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("SendMessage/{message}")]
        public void SendMessage(string message)
        {
            BaseMessage header = new BaseMessage()
            {
               value =1,Name = message
            };
            _messageSender.SendMessage(header, "Test-queue");

        }


    }
}
