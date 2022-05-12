using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AirlineService.DataModel.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSetting _appSetting;
        private DataContext _airlineContext;
        public AuthenticationService(IOptions<AppSetting> appSetting, DataContext airlineContext)
        {
            _appSetting = appSetting.Value;
            _airlineContext = airlineContext;
        }
  
        public UserDTO Authenticate(string userName, string password)
        {
            var user = _airlineContext.User.SingleOrDefault(x => x.UserName == userName && x.Password == password);
            if(user == null)
            {
                return null;
            }

            var tokenHander = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Version, "V3.1"),

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHander.CreateToken(tokenDescriptor);

            UserDTO DTO = new UserDTO()
            {
                UserId = user.Id,
                Username = user.UserName,
                Password = user.Password,
                Token = tokenHander.WriteToken(token),
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
            //user.Token = tokenHander.WriteToken(token);

            //user.Password = null;
            return DTO;

        }
    }
}
