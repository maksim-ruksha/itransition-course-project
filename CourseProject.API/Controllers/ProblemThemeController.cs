using System;
using System.Collections;
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
    [ApiController]
    public class ProblemThemeController : Controller
    {
        private Service<ProblemThemeModel, ProblemThemeEntity> _problemThemeService;

        public ProblemThemeController(IRepository<ProblemThemeEntity> repository, IMapper mapper, IWebHostEnvironment environment)
        {
            _problemThemeService = new Service<ProblemThemeModel, ProblemThemeEntity>(repository, mapper, environment);
        }

        [HttpGet("/getProblemThemes")]
        public async Task<ActionResult> GetProblemThemes()
        {
            IEnumerable<ProblemThemeModel> problems = await _problemThemeService.GetAsync();
            return new JsonResult(problems);
        }

        // TODO: admin only CreateProblemTheme()
        [HttpPost("/createProblemTheme")]
        public async Task<ActionResult> CreateProblemTheme(ProblemThemeModel problemTheme)
        {
            IEnumerable<ProblemThemeModel> possibleExistingTheme =
                await _problemThemeService.GetAsync(theme => theme.Value.Equals(problemTheme.Value));
            if (possibleExistingTheme.Any())
                return BadRequest("This theme already exists");
            bool success = await _problemThemeService.CreateAsync(problemTheme);
            if (!success)
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            return Ok();
        }

        // TODO: admin only UpdateProblemTheme()
        [HttpPost("/updateProblemTheme")]
        public async Task<ActionResult> UpdateProblemTheme(ProblemThemeModel newProblemTheme)
        {
            try
            {
                bool success = await _problemThemeService.UpdateAsync(newProblemTheme);
                if (!success)
                    return new StatusCodeResult((int) HttpStatusCode.InternalServerError);    
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Could not find problem theme");
            }
        }
    }
}