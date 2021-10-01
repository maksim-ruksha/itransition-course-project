using AutoMapper;
using CourseProject.BLL.Models;
using CourseProject.BLL.Models.Problems;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Entities.Problems;

namespace CourseProject.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserEntity>();
            CreateMap<UserEntity, UserModel>();
            
            CreateMap<ProblemModel, ProblemEntity>();
            CreateMap<ProblemEntity, ProblemModel>();
            
            CreateMap<ProblemThemeModel, ProblemThemeEntity>();
            CreateMap<ProblemThemeEntity, ProblemThemeModel>();
            
            CreateMap<ProblemCommentModel, ProblemCommentEntity>();
            CreateMap<ProblemCommentEntity, ProblemCommentModel>();
            
            CreateMap<UserModel, UserEntity>();
            CreateMap<UserEntity, UserModel>();

            CreateMap<ProblemTagModel, ProblemTagEntity>();
            CreateMap<ProblemTagEntity, ProblemTagModel>();

            CreateMap<ProblemSolutionVariantModel, ProblemSolutionVariantEntity>();
            CreateMap<ProblemSolutionVariantEntity, ProblemSolutionVariantModel>();
            
            CreateMap<ProblemRatingModel, ProblemRatingEntity>();
            CreateMap<ProblemRatingEntity, ProblemRatingModel>();
        }
    }
}