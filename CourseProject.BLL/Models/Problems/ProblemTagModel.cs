using System;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemTagModel
    {
        public Guid Id { get; set; }
        public string Tag { get; set; }
    }
}