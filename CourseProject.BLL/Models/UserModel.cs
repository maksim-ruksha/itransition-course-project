using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseProject.BLL.Models
{
    public class UserModel
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        [JsonIgnore] public string PasswordHash { get; set; }
        [Required] public string Role { get; set; }
        
    }
}