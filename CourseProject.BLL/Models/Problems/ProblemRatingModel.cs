using System;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemRatingModel
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
    }
}