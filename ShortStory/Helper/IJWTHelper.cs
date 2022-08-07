using ShortStory.Entities;

namespace ShortStory.Helper
{
    public interface IJWTHelper
    {
        string GenerateToken(User user);
    }
}
