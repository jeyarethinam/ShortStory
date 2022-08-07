using ShortStory.Models.CommonModel;

namespace ShortStory.Models.ResponseModel
{
    public class UserResponseModel : UserModel
    {


        public Guid Id { get; set; }
        public string Token { get; set; }
    }
}
