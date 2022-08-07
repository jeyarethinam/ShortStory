namespace ShortStory.Models.RequestModel
{
    public class BanRequestModel
    {
        public Guid UserId { get; set; }
        public bool isBanned { get; set; }
    }
}
