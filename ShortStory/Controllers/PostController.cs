using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortStory.IServices;
using ShortStory.Models.RequestModel;

namespace ShortStory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _iPostService;
        public PostController(IPostService iPostService)
        {
            _iPostService = iPostService;
        }


        [Authorize]
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(PostRequestModel postRequestModel)
        {
            try
            {


                var UserId = Guid.Parse(User.Identity?.Name);

                postRequestModel.UserId = UserId;
                var res = await _iPostService.CreatePost(postRequestModel).ConfigureAwait(true);
                return Ok(res);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        [Authorize]
        [HttpGet("GetAllPost")]
        public async Task<IActionResult> GetAllPost(Guid? UserId)
        {
            try
            {
                if (UserId == null)
                {
                    UserId = Guid.Parse(User.Identity?.Name);
                }


                var res = await _iPostService.GetAllPost(UserId ?? default).ConfigureAwait(true);
                return Ok(res);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [Authorize]
        [HttpGet("GetFollowersPost")]
        public async Task<IActionResult> GetFollowersPost()
        {
            try
            {
                var Id = Guid.Parse(User.Identity?.Name);
                var res = await _iPostService.GetAllFollowersPost(Id).ConfigureAwait(true);
                return Ok(res);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
