using ShortStory.Models.RequestModel;
using ShortStory.Models.ResponseModel;

namespace ShortStory.IServices
{
    public interface IPostService
    {
        Task<PostResponseModel> CreatePost(PostRequestModel requestModel);
        Task<List<PostResponseModel>> GetAllFollowersPost(Guid UserId);
        Task<List<PostResponseModel>> GetAllPost(Guid UserId);
    }
}
