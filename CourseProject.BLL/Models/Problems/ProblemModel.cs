using System;

namespace CourseProject.BLL.Models.Problems
{
    public class ProblemModel
    {
        public Guid Id { get; set; }
        public DateTime PublicationDateTime { get; set; }
        public string Title { get; set; }
        public string RawDescription { get; set; }
        public ProblemThemeModel ProblemTheme { get; set; }
        public UserModel Author { get; set; }
        public string[] Tags { get; set; }
        public string[] SolutionVariants { get; set; }
        
        // TODO: add images
        
    }
}