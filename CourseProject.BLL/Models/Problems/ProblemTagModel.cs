using System;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemTagModel
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Tag { get; set; }
    }
}