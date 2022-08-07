using ShortStory.Entities;
using ShortStory.Models.RequestModel;
using ShortStory.Models.ResponseModel;

namespace ShortStory.IServices
{
    public interface IUserService
    {
        Task<UserResponseModel> BanUser(BanRequestModel banRequest);
        Task<UserFollowers> Follow(Guid UserId, Guid FolloweId);
        Task<List<FollowingUserModel>> GetallUsers(string? UserName);
        Task<List<FollowingUserModel>> GetFollowingList(Guid UserId);
        Task<User> GetUserById(Guid Id);
        Task<UserResponseModel> Login(LoginRequestModel loginRequestModel);
        Task<UserResponseModel> PromoteAsEdittor(PromoteRequestModel promoteRequest);
        Task<UserResponseModel> SignUp(UserRequestModel userRequestModel);
    }


}
