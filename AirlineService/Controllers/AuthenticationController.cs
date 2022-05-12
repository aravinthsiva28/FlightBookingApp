using AirlineService.DataModel.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AirlineService.Controllers
{
    [Route("api/Authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authService;
        

       
        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
  
        }
        [HttpPost]
        public IActionResult Post([FromBody] UserLogin model)
        {
            var user = _authService.Authenticate(model.userName, model.password);
            if (user == null)
            {
                return BadRequest(new { message = "UserName or Password Incorrect" });
            }

            return Ok(user);
        }
        
        
      

    }
}
