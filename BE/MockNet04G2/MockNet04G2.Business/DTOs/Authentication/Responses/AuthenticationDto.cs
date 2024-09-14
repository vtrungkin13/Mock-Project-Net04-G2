using MockNet04G2.Business.DTOs.Users.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.DTOs.Authentication.Responses
{
    public class AuthenticationDto
    {
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public UserDetailDto User { get; set; }
    }
}
