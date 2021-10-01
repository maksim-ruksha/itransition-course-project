using System;
using System.Collections.Generic;

namespace CourseProject.DAL.Entities.Problems
{
    public class ProblemCommentEntity
    {
        public Guid Id { get; set; }
        public DateTime PublicationDateTime { get; set; }
        public UserEntity Author { get; set; }
        public string Value { get; set; }
        public IEnumerable<UserEntity> LikedBy { get; set; }
        public IEnumerable<UserEntity> DislikedBy { get; set; }
    }
}