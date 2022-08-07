using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortStory.Enums;
using ShortStory.IServices;
using ShortStory.Models.RequestModel;
using System.Security.Claims;

namespace ShortStory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp(UserRequestModel userRequestModel)
        {
            try
            {
                var res = await _userService.SignUp(userRequestModel).ConfigureAwait(true);
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestModel requestModel)
        {
            try
            {
                var res = await _userService.Login(requestModel).ConfigureAwait(true);
                return Ok(res);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [Authorize]
        [HttpPost("BanUser")]
        public async Task<IActionResult> BanUser(BanRequestModel banRequest)
        {
            try
            {
                var userRole = (UserRole)int.Parse(User.FindFirst(ClaimTypes.Role).Value);
                if (userRole == UserRole.Writer)
                {
                    throw new BadHttpRequestException("You canot able to acces");
                }

                var res = await _userService.BanUser(banRequest).ConfigureAwait(true);
                return Ok(res);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpPost("PromoteAsEdittor")]
        public async Task<IActionResult> PromoteAsEdittor(PromoteRequestModel promoteRequest)
        {
            try
            {
                var Id = Guid.Parse(User.Identity?.Name);
                var userRole = (UserRole)int.Parse(User.FindFirst(ClaimTypes.Role).Value);

                var existingUser = await _userService.GetUserById(Id).ConfigureAwait(true);

                if (!existingUser.IsEditor && userRole == UserRole.Writer)
                {
                    throw new BadHttpRequestException("You canot able to acces");
                }

                var res = await _userService.PromoteAsEdittor(promoteRequest).ConfigureAwait(true);
                return Ok(res);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetFolowers")]
        public async Task<IActionResult> GetFollowingList()
        {


            try
            {
                var Id = Guid.Parse(User.Identity?.Name);

                var res = await _userService.GetFollowingList(Id).ConfigureAwait(true);
                return Ok(res);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("Follow/{followerId}")]
        public async Task<IActionResult> Follow(Guid followerId)
        {
            try
            {
                var userId = Guid.Parse(User.Identity?.Name);
                var res = await _userService.Follow(userId, followerId).ConfigureAwait(true);
                return Ok(res);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(string? UserName)
        {

            try
            {
                var res = await _userService.GetallUsers(UserName).ConfigureAwait(true);
                return Ok(res);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
