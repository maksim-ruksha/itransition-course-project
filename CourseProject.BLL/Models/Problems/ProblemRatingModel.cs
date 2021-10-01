using System;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemRatingModel
    {
        [Required] public Guid Id { get; set; }
        [Required] public int Rating { get; set; }
    }
}