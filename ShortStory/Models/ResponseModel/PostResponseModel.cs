using ShortStory.Models.CommonModel;

namespace ShortStory.Models.ResponseModel
{
    public class PostResponseModel:PostModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

    }
}
