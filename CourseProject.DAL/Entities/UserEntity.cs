using System;

namespace CourseProject.DAL.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}