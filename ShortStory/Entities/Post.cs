namespace ShortStory.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public Guid UserId { get; set; }
        public DateTime Created { get; set; }
        public string ImageUrl { get; set; }

    }
}
