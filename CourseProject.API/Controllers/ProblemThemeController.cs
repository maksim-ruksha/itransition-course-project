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
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Controllers
{
    [ApiController]
    public class ProblemThemeController : Controller
    {
        private Service<ProblemThemeModel, ProblemThemeEntity> _problemThemeService;

        public ProblemThemeController(IRepository<ProblemThemeEntity> repository, IMapper mapper)
        {
            _problemThemeService = new Service<ProblemThemeModel, ProblemThemeEntity>(repository, mapper);
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
                return BadRequest("This theme already exist");
            bool success = await _problemThemeService.CreateAsync(problemTheme);
            if (!success)
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            return Ok();
        }

        // TODO: admin only UpdateProblemTheme()
        [HttpGet("/updateProblemTheme")]
        public async Task<ActionResult> UpdateProblemTheme(ProblemThemeModel problemTheme)
        {
            try
            {
                ProblemThemeModel theme = await _problemThemeService.FindByIdAsync(problemTheme.Id);
                bool success = _problemThemeService.UpdateAsync(problemTheme);
                if (!success)
                    return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Could not find problem theme");
            }
        }

        // TODO: remove RemoveProblemTheme? Some shit would happen if some problems will point to non existing problem
        [HttpGet("/removeProblemTheme")]
        public async Task<ActionResult> RemoveProblemTheme(Guid id)
        {
            try
            {
                ProblemThemeModel theme = await _problemThemeService.FindByIdAsync(id);
                bool success = _problemThemeService.RemoveAsync(theme);
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