using CourseProject.DAL.Entities;
using CourseProject.DAL.Entities.Problems;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.EF
{
    public sealed class DatabaseContext: DbContext
    {
        public DbSet<UserEntity> Users { get; set; } 
        public DbSet<ProblemEntity> Problems { get; set; }
        public DbSet<ProblemCommentEntity> Comments { get; set; }
        public DbSet<ProblemThemeEntity> Themes { get; set; }
        public DbSet<ProblemRatingEntity> Ratings { get; set; }
        public DbSet<ProblemTagEntity> Tags { get; set; }
        public DbSet<ProblemSolutionVariantEntity> SolutionVariants { get; set; }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<ProblemEntity>().ToTable("Problems");
            modelBuilder.Entity<ProblemCommentEntity>().ToTable("ProblemComments");
            modelBuilder.Entity<ProblemThemeEntity>().ToTable("ProblemThemes");
            modelBuilder.Entity<ProblemRatingEntity>().ToTable("ProblemRatings");
            modelBuilder.Entity<ProblemTagEntity>().ToTable("ProblemTags");
            modelBuilder.Entity<ProblemSolutionVariantEntity>().ToTable("ProblemSolutionVariants");
        }
    }
}