using ShortStory.Enums;
using System.Text.Json.Serialization;

namespace ShortStory.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
        public bool IsEditor { get; set; }
        public bool IsBanned { get; set; }

        [JsonIgnore]
        public string PasswordHash{ get; set; }


    }
}
