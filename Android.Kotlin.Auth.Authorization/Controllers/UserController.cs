using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Android.Kotlin.Auth.Authorization.Dtos;
using Android.Kotlin.Auth.Authorization.Models;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Android.Kotlin.Auth.Authorization.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class UserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        //.well-known/openid-configuration

      
        public IActionResult test()
        {
            return Ok("Ok test");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            var user = new ApplicationUser
            {
                UserName = signUpViewModel.UserName,
                Email = signUpViewModel.Email,
                City = signUpViewModel.City
            };
            var result = await _userManager.CreateAsync(user,signUpViewModel.Password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(b => b.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null) return BadRequest();
            
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);

            if (user is null) return BadRequest();

            var userDto = new ApplicationUserDto
            {
                UserName = user.UserName,
                City = user.City,
                Email = user.Email
            };
            return Ok(userDto);
        }
    }
}
