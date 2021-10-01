using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemCommentModel
    {
        [Required] public Guid Id { get; set; }
        [Required] public DateTime PublicationDateTime { get; set; }
        [Required] public UserModel Author { get; set; }
        [Required] public string Value { get; set; }
        public IEnumerable<UserModel> LikedBy { get; set; }
        public IEnumerable<UserModel> DislikedBy { get; set; }
    }
}