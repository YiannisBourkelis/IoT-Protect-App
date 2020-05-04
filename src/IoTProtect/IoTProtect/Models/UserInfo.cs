using System;

namespace IoTProtect.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
