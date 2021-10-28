using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CourseProject.Auth;
using CourseProject.BLL.Models;
using CourseProject.BLL.Services;
using CourseProject.DAL.EF;
using CourseProject.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private Service<UserModel, UserEntity> _userService;

        public AccountController(IRepository<UserEntity> repository, IMapper mapper, IWebHostEnvironment environment)
        {
            _userService = new Service<UserModel, UserEntity>(repository, mapper, environment);
        }

        [HttpGet("/register")]
        public async Task<ActionResult> Register(string name, string password, string passwordRepeat)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is empty");
            
            if (string.IsNullOrWhiteSpace(password))
                return BadRequest("Password is empty");
            
            if (string.IsNullOrWhiteSpace(passwordRepeat))
                return BadRequest("Repeated Password is empty");
            
            if (!password.Equals(passwordRepeat))
                return BadRequest("Passwords are not same");

            IEnumerable<UserModel> possibleExistingUser = await _userService.GetAsync(user => user.Name.Equals(name));
            if (possibleExistingUser.Any())
                return BadRequest("User with this name already exists");

            UserModel newUser = new UserModel
            {
                Name = name,
                PasswordHash = Cryptography.HashPassword(password),
                Role = "User"
            };
            bool success = await _userService.CreateAsync(newUser);
            if (!success)
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);

            UserModel dbNewUser = await _userService.FindByIdAsync(newUser.Id);
            ClaimsIdentity identity = await GetIdentity(name, password);
            if (identity == null)
            {
                return BadRequest("What the fuck");
            }
            UserModel userModel = await _userService.FindByIdAsync(
                Guid.Parse(
                    // ReSharper disable once PossibleNullReferenceException
                    identity.Claims.FirstOrDefault().Value
                ));
            return new JsonResult(userModel);
        }

        [HttpGet("/login")]
        public async Task<ActionResult> Login(string name, string password)
        {
            ClaimsIdentity identity = await GetIdentity(name, password);
            if (identity == null)
            {
                return BadRequest("Invalid username or password");
            }

            string encodedJwt = JwtCoder.Encode(identity);
            HttpContext.Response.Cookies.Append("jwt", encodedJwt);

            UserModel userModel = await _userService.FindByIdAsync(
                Guid.Parse(
                    // ReSharper disable once PossibleNullReferenceException
                    identity.Claims.FirstOrDefault().Value
                ));
            return new JsonResult(userModel);
        }

        private async Task<ClaimsIdentity> GetIdentity(string name, string password)
        {
            IEnumerable<UserModel> userEnumerable = await _userService.GetAsync(u =>
                u.Name.Equals(name) && Cryptography.VerifyHashedPassword(u.PasswordHash, password));
            UserModel user = userEnumerable.FirstOrDefault();
            if (user != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                    new(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        // TODO: registration by email & password
        // TODO: registration by facebook
        // TODO: registration by google
    }
}