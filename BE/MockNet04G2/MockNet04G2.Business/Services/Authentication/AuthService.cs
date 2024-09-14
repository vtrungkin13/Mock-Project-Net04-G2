using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Authentication
{
    public class AuthService
    {
        public string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz012345678@!@#$%^&*9";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 12)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
        }
    }
}
