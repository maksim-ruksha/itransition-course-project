using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseProject.BLL.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore] public string PasswordHash { get; set; }
        public string Role { get; set; }
        
    }
}