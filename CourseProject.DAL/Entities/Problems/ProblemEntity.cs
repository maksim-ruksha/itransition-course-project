using System;
using System.Collections.Generic;

namespace CourseProject.DAL.Entities.Problems
{
    public class ProblemEntity
    {
        public Guid Id { get; set; }
        public DateTime PublicationDateTime { get; set; }
        public string Title { get; set; }
        public string RawDescription { get; set; }
        public ProblemThemeEntity ProblemTheme { get; set; }
        public UserEntity Author { get; set; }
        public IEnumerable<ProblemTagEntity> Tags { get; set; }
        public IEnumerable<ProblemSolutionVariantEntity> SolutionVariants { get; set; }
        public IEnumerable<ProblemRatingEntity> Ratings { get; set; }
    }
}