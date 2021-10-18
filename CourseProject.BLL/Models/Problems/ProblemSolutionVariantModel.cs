using System;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemSolutionVariantModel
    {
        public Guid Id { get; set; }
        public string Solution { get; set; }
    }
}