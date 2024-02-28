using FindJob.Data.Entities;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace FindJob.Models
{

    public class UserDTO
    {
        public UserDTO()
        {
            IsActive = true;
        }

        public bool IsActive { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public ObjectId Id { get; set; }
        public string Password { get; set; }
        //public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public GenderType Gender { get; set; }
        public int Age { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public bool RememberMe { get; set; }
    }



}