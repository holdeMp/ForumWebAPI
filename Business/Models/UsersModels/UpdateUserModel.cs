using Microsoft.AspNetCore.Http;
using System;

namespace Business.Models.UsersModels
{
    public class UpdateUserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AvatarName { get; set; }
        public IFormFile ImageFile { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
