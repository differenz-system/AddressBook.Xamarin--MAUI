using System;
using SQLite;

namespace AddressBook.MAUI.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string DeviceOSType { get; set; }
        public string DeviceUDID { get; set; }
        public string DeviceToken { get; set; }
        public string Errors { get; set; }
    }

    public class LoginObject
    {
        public LoginModel LoginData { get; set; }
    }

    public partial class LoginResponse
    {
        public LoginObject Data { get; set; }
    }
}

