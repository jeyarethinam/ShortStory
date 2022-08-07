namespace ShortStory.Entities
{
    public class UserFollowers
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid FollowerId { get; set; }
    }
}
