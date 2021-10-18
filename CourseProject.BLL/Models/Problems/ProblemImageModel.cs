using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemImageModel
    {
        [Required] public Guid Id { get; set; }
        [Required] public IFormFile Image { get; set; }
        public string ImageFileName { get; set; }
    }
}