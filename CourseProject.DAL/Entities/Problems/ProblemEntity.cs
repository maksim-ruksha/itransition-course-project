using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.DAL.Entities.Problems
{
    public class ProblemEntity
    {
        public Guid Id { get; set; }
        public DateTime PublicationDateTime { get; set; }
        public string Title { get; set; }
        public string RawDescription { get; set; }
        public virtual UserEntity Author { get; set; }

        public virtual IEnumerable<ProblemImageEntity> Images { get; set; }
        public virtual IEnumerable<ProblemTagEntity> Tags { get; set; }
        public virtual IEnumerable<ProblemSolutionVariantEntity> SolutionVariants { get; set; }
        public virtual IEnumerable<ProblemRatingEntity> Ratings { get; set; }
    }
}