using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CourseProject.BLL.Models.Problems;
using CourseProject.BLL.Services;
using CourseProject.DAL.EF;
using CourseProject.DAL.Entities.Problems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly Service<ProblemModel, ProblemEntity> _problemService;

        public ProblemsController(
            IRepository<ProblemEntity> problemRepository,
            IRepository<ProblemThemeEntity> themeRepository,
            IMapper mapper,
            IWebHostEnvironment environment)
        {
            _problemService = new Service<ProblemModel, ProblemEntity>(problemRepository, mapper, environment);
        }

        [HttpGet("/searchProblems")]
        public async Task<IEnumerable<ProblemModel>> SearchProblems(string whatToSearch)
        {
            IEnumerable<ProblemModel> problems =
                await _problemService.GetAsync(problem => problem.Title.Contains(whatToSearch));
            return problems;
        }

        [HttpGet("/getProblems")]
        public async Task<IEnumerable<ProblemModel>> GetProblems()
        {
            return await _problemService.GetAsync();
        }

        // TODO: sus shit
        [HttpGet("/getUserProblems")]
        public async Task<IEnumerable<ProblemModel>> GetUserProblems(Guid userId, int start = 0, int size = 25)
        {
            IEnumerable<ProblemModel> userProblems =
                await _problemService.GetAsync(problem => problem.Author.Id.Equals(userId));
            IEnumerable<ProblemModel> pagedUserProblems = userProblems.Skip(start).Take(size);
            return pagedUserProblems;
        }

        [HttpGet("/findProblemById")]
        public async Task<ProblemModel> FindProblemById(Guid id)
        {
            ProblemModel model = await _problemService.FindByIdAsync(id);
            return model;
        }

        [HttpPost("/editProblem")]
        public async Task<ActionResult> EditProblem(ProblemModel theNewModel)
        {
            // TODO: send proper messages?
            bool success = await _problemService.UpdateAsync(theNewModel);
            if (!success)
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            return Ok();
        }

        [HttpPost("/createProblem")]
        public async Task<ActionResult> CreateProblem([FromBody]ProblemModel problem)
        {
            bool success = await _problemService.CreateAsync(problem);
            if (!success)
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            return Ok();
        }

        [HttpPost("/removeProblem")]
        public async Task<ActionResult> RemoveProblem(ProblemModel problem)
        {
            bool success = _problemService.RemoveAsync(problem);
            if (!success)
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            return Ok();
        }
    }
}