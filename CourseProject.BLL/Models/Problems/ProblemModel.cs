using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemModel
    {
        public Guid Id { get; set; }
        public DateTime PublicationDateTime { get; set; }
        public string Title { get; set; }
        public string RawDescription { get; set; }
        public ProblemThemeModel ProblemTheme { get; set; }
        public IList<ProblemImageModel> Images { get; set; }
        public UserModel Author { get; set; }
        public IList<ProblemTagModel> Tags { get; set; }
        public IList<ProblemSolutionVariantModel> SolutionVariants { get; set; }
        public IList<ProblemRatingModel> Ratings { get; set; }
    }
}