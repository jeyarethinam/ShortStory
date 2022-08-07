using ShortStory.Entities;
using ShortStory.Models.ResponseModel;

namespace ShortStory.IRepos
{
    public interface IPostRepo
    {
        Task<VowelStats> CreateVowelStats(VowelStats vowelStats);
        Task<List<PostResponseModel>> GetAllFollowersPost(Guid UserId);
        Task<List<PostResponseModel>> GetAllPost(Guid userId);
        Task<VowelStats> GetVowelStats();
        Task<Post> Post(Post postEntity);
        Task<VowelStats> UpdateVowelStats(VowelStats vowelStats);
    }
}
