using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Zhaoxi.NETCore31.CoreWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            Console.WriteLine($"This is UsersController Get Invoke");

            return await new UserService().UserAll();
        }
    }

    public class UserService
    {
        #region DataInit
        private List<User> _UserList = new List<User>()
        {
            new User()
            {
                Id=1,
                Account="Administrator",
                Email="57265177@qq.com",
                Name="Eleven",
                Password="1234567890",
                LoginTime=DateTime.Now,
                Role="Admin"
            },
             new User()
            {
                Id=1,
                Account="Apple",
                Email="57265177@qq.com",
                Name="Apple",
                Password="1234567890",
                LoginTime=DateTime.Now,
                Role="Admin"
            },
              new User()
            {
                Id=1,
                Account="Cole",
                Email="57265177@qq.com",
                Name="Cole",
                Password="1234567890",
                LoginTime=DateTime.Now,
                Role="Admin"
            },
        };
        #endregion

        public User FindUser(int id)
        {
            return this._UserList.Find(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> UserAll()
        {
            return this._UserList;
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime LoginTime { get; set; }
    }
}