using System;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemSolutionVariantModel
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Solution { get; set; }
    }
}