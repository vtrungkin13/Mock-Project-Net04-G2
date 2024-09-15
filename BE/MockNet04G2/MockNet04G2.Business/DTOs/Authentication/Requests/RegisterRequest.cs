using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.DTOs.Authentication.Requests
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime Dob { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
