using System;

namespace IoTProtect.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
            Email = "aaa@aa.sa";
            Password = "8888";
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccessToken { get; set; }
    }
}
