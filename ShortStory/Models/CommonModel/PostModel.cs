namespace ShortStory.Models.CommonModel
{
    public class PostModel
    {
        public string Caption { get; set; }
        public Guid UserId { get; set; }
        public DateTime Created { get; set; }
        public string ImageUrl { get; set; }
    }
}
