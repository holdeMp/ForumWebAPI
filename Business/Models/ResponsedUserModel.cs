using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class ResponsedUserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AvatarPath { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool ConfirmedEmail { get; set; }
    }
}
