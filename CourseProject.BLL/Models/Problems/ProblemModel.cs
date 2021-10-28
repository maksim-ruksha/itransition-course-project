using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemModel
    {
        public Guid Id { get; set; }
        public DateTime PublicationDateTime { get; set; }
        public string Title { get; set; }
        public string RawDescription { get; set; }
        
        
        public UserModel Author { get; set; }
        
        public IEnumerable<ProblemImageModel> Images { get; set; }
        public IEnumerable<ProblemTagModel> Tags { get; set; }
        public IEnumerable<ProblemSolutionVariantModel> SolutionVariants { get; set; }
        public IEnumerable<ProblemRatingModel> Ratings { get; set; }
    }
}