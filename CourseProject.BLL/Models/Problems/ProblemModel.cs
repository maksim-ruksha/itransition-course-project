using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemModel
    {
        [Required] public Guid Id { get; set; }
        [Required] public DateTime PublicationDateTime { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string RawDescription { get; set; }
        [Required] public ProblemThemeModel ProblemTheme { get; set; }
        [Required] public UserModel Author { get; set; }
        public IEnumerable<ProblemTagModel> Tags { get; set; }
        public IEnumerable<ProblemSolutionVariantModel> SolutionVariants { get; set; }
        public IEnumerable<ProblemRatingModel> Ratings { get; set; }
        
        // TODO: add images
        
    }
}