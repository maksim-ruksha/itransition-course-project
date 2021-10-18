using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemImageModel
    {
        public Guid Id { get; set; }
        public IFormFile Image { get; set; }
        public string ImageFileName { get; set; }
    }
}