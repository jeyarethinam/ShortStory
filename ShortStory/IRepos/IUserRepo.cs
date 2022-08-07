using ShortStory.Entities;
using ShortStory.Models.ResponseModel;

namespace ShortStory.IRepos
{
    public interface IUserRepo
    {
        Task<UserFollowers> Follow(Guid UserId, Guid FolowerId);
        Task<List<FollowingUserModel>> GetallUsers(string? UserName);
        Task<List<FollowingUserModel>> GetFollowingList(Guid UserId);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid id);
        Task<User> GetUSerByUSerName(string userName,string email);
        Task<User> SignUp(User user);
        Task<User> UpdateUser(User user);
    }
}
