
using Microsoft.IdentityModel.Tokens;
using ShortStory.Entities;
using ShortStory.Enums;
using ShortStory.Helper;
using ShortStory.IRepos;
using ShortStory.IServices;
using ShortStory.Models.RequestModel;
using ShortStory.Models.ResponseModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShortStory.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IJWTHelper _jWTHelper;

        public IConfiguration _configuration;
        public UserService(IUserRepo userRepo, IConfiguration config, IJWTHelper jWTHelper)
        {
            _userRepo = userRepo;
            _configuration = config;
            _jWTHelper = jWTHelper;
        }




        public async Task<UserResponseModel> SignUp(UserRequestModel userRequestModel)
        {
            if (!IsValidEmail(userRequestModel.Email))
            {
                throw new BadHttpRequestException("EMail is not valid");
            }

            var existingUser = await _userRepo.GetUSerByUSerName(userRequestModel.UserName, userRequestModel.Email).ConfigureAwait(true);

            if (existingUser != null)
            {
                throw new BadHttpRequestException("Username is already taken");
            }
            var entity = MapUserModelToEntity(userRequestModel);

            var res = await _userRepo.SignUp(entity).ConfigureAwait(true);


            return MapEntityToResponseModel(res);

        }



        public async Task<UserResponseModel> Login(LoginRequestModel loginRequestModel)
        {
            var existingUser = await _userRepo.GetUserByEmail(loginRequestModel.Email).ConfigureAwait(true);

            if (existingUser == null)
            {
                throw new BadHttpRequestException("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginRequestModel.Password, existingUser.PasswordHash))
            {
                throw new BadHttpRequestException("Incorrect Password");
            }

            return MapEntityToResponseModel(existingUser);

        }


        public async Task<UserResponseModel> BanUser(BanRequestModel banRequest)
        {
            var existingUser = await _userRepo.GetUserById(banRequest.UserId).ConfigureAwait(true);
            if (existingUser == null)
            {
                throw new BadHttpRequestException("User not found.");
            }
            existingUser.IsBanned = banRequest.isBanned;

            var res = await _userRepo.UpdateUser(existingUser).ConfigureAwait(true);
            return MapEntityToResponseModel(existingUser);

        }

        public async Task<UserResponseModel> PromoteAsEdittor(PromoteRequestModel promoteRequest)
        {
            var existingUser = await _userRepo.GetUserById(promoteRequest.UserId).ConfigureAwait(true);
            if (existingUser == null)
            {
                throw new BadHttpRequestException("User not found.");
            }
            existingUser.IsBanned = promoteRequest.isEditor;

            var res = await _userRepo.UpdateUser(existingUser).ConfigureAwait(true);
            return MapEntityToResponseModel(existingUser);

        }

        public async Task<List<FollowingUserModel>> GetFollowingList(Guid UserId)
        {

            var res = await _userRepo.GetFollowingList(UserId).ConfigureAwait(true);
            return res;

        }


        public async Task<UserFollowers> Follow(Guid UserId, Guid FolloweId)
        {

            var res = await _userRepo.Follow(UserId, FolloweId).ConfigureAwait(true);
            return res;

        }
        public async Task<User> GetUserById(Guid Id)
        {
            var existingUser = await _userRepo.GetUserById(Id).ConfigureAwait(true);
            return existingUser;

        }
        public async Task<List<FollowingUserModel>> GetallUsers(string? UserName)
        {
            var res = await _userRepo.GetallUsers(UserName).ConfigureAwait(true);
            return res;
        }





        private User MapUserModelToEntity(UserRequestModel userRequestModel)
        {

            User entity = new User();
            entity.Id = new Guid();
            entity.UserName = userRequestModel.UserName;
            entity.Email = userRequestModel.Email;
            entity.FirstName = userRequestModel.FirstName;
            entity.LastName = userRequestModel.LastName;
            entity.UserRole = UserRole.Writer;
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRequestModel.Password);
            entity.IsEditor = false;
            entity.IsBanned = false;


            return entity;

        }

        private UserResponseModel MapEntityToResponseModel(User user)
        {
            UserResponseModel responseModel = new UserResponseModel();
            responseModel.Id = user.Id;
            responseModel.UserName = user.UserName;
            responseModel.Email = user.Email;
            responseModel.FirstName = user.FirstName;
            responseModel.LastName = user.LastName;
            responseModel.Token = _jWTHelper.GenerateToken(user);

            return responseModel;
        }


        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

    }
}
