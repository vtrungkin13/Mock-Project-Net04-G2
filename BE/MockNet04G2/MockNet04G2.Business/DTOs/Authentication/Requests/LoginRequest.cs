using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.DTOs.Authentication.Requests
{
    public class LoginRequest
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
    }
}
