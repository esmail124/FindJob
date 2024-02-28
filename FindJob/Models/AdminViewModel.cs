﻿

using MongoDB.Bson;

namespace FindJob.Models
{
    public class AdminViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
    }
}
