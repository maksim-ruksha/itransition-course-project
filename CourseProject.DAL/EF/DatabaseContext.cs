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
        public DbSet<ProblemRatingEntity> Ratings { get; set; }
        public DbSet<ProblemTagEntity> Tags { get; set; }
        public DbSet<ProblemSolutionVariantEntity> SolutionVariants { get; set; }
        public DbSet<ProblemImageEntity> Images { get; set; }
        
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
            //modelBuilder.Entity<ProblemEntity>().HasOne<ProblemThemeEntity>().WithMany();
            /*modelBuilder.Entity<UserEntity>()
                .ToTable("Users");
            modelBuilder.Entity<ProblemEntity>().HasOne<ProblemThemeEntity>();
            
            modelBuilder.Entity<ProblemCommentEntity>()
                .ToTable("ProblemComments");
            modelBuilder.Entity<ProblemThemeEntity>().HasMany<ProblemEntity>();
            modelBuilder.Entity<ProblemRatingEntity>()
                .ToTable("ProblemRatings");
            modelBuilder.Entity<ProblemTagEntity>()
                .ToTable("ProblemTags");
            modelBuilder.Entity<ProblemSolutionVariantEntity>()
                .ToTable("ProblemSolutionVariants");
            modelBuilder.Entity<ProblemImageEntity>()
                .ToTable("ProblemImages");*/
        }
    }
}