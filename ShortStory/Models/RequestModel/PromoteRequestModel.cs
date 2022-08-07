namespace ShortStory.Models.RequestModel
{
    public class PromoteRequestModel
    {
        public Guid UserId { get; set; }
        public bool isEditor { get; set; }
    }
}
