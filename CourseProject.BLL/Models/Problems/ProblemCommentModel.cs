using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemCommentModel
    {
        public Guid Id { get; set; }
        public DateTime PublicationDateTime { get; set; }
        public UserModel Author { get; set; }
        public string Value { get; set; }
        public IEnumerable<UserModel> LikedBy { get; set; }
        public IEnumerable<UserModel> DislikedBy { get; set; }
    }
}