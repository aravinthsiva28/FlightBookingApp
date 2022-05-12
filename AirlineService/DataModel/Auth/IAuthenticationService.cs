using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel.Auth
{
    public interface IAuthenticationService
    {
        UserDTO Authenticate(string userName, string password);
    }
}
