using System;
using SQLite;

namespace AddressBook.MAUI.Models
{
    public class UserData
    {
        public UserData()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string LoginID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public bool Active { get; set; }
    }
}

