using System;

namespace CourseProject.BLL.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public DateTime PublicationDateTime { get; set; }
        public UserModel Author { get; set; }
        public string Value { get; set; }
        public UserModel[] LikedBy { get; set; }
        public UserModel[] DislikedBy { get; set; }
    }
}