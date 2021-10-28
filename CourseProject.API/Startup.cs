using System;
using AutoMapper;
using CourseProject.Auth;
using CourseProject.BLL.Models;
using CourseProject.BLL.Models.Problems;
using CourseProject.BLL.Services;
using CourseProject.DAL.EF;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Entities.Problems;
using CourseProject.Mapping;
using CourseProject.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

// dotnet ef database update  Initial --project CourseProject.DAL -s CourseProject.API --verbose
namespace CourseProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            
            MapperConfiguration mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IRepository<ProblemEntity>, Repository<ProblemEntity>>();
            services.AddScoped<IRepository<ProblemCommentEntity>, Repository<ProblemCommentEntity>>();
            services.AddScoped<IRepository<ProblemRatingEntity>, Repository<ProblemRatingEntity>>();
            services.AddScoped<IRepository<UserEntity>, Repository<UserEntity>>();
            

            services.AddScoped<IService<ProblemModel, ProblemEntity>, Service<ProblemModel, ProblemEntity>>();
            services.AddScoped<IService<ProblemCommentModel, ProblemCommentEntity>, Service<ProblemCommentModel, ProblemCommentEntity>>();
            services.AddScoped<IService<ProblemRatingModel, ProblemRatingEntity>, Service<ProblemRatingModel, ProblemRatingEntity>>();
            
            services.AddScoped<IService<UserModel, UserEntity>, Service<UserModel, UserEntity>>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "CourseProject.API", Version = "v1"});
            });
            
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DatabaseContext>(optionsActions => optionsActions.UseSqlServer(connectionString));
            
           
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JwtAuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = JwtAuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = JwtAuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                    
                    // TODO: check this
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = JwtAuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
                {
                    options.AddPolicy("RequireAdminRights", policy => policy.RequireRole("Admin"));
                    //TODO: ???
                    options.AddPolicy("RequireUserRights", policy => policy.RequireRole("User"));
                }
            );
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CourseProject.API v1"));
            }
            
            app.UseCors(
                options => options.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );

            app.UseMiddleware<JwtMiddleware>();
            app.UseMiddleware<CookieMiddleware>();
            
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}