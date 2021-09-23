using System;

namespace CourseProject.BLL.Models
{
    public class UserModel
    {
        public Guid Id;
        public string Name { get; set; }
        public string Role { get; set; }

        //TODO: user image
    }
}