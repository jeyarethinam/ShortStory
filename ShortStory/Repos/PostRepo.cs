using ShortStory.DataContext;
using ShortStory.Entities;
using ShortStory.IRepos;
using ShortStory.Models.ResponseModel;

namespace ShortStory.Repos
{
    public class PostRepo : IPostRepo
    {
        private readonly ShortStoryDbContext _shortStoryDbContext;
        public PostRepo(ShortStoryDbContext shortStoryDbContext)
        {
            _shortStoryDbContext = shortStoryDbContext;
        }



        public async Task<Post> Post(Post postEntity)
        {

            var res = _shortStoryDbContext.Post.Add(postEntity);
            await _shortStoryDbContext.SaveChangesAsync();
            return res.Entity;

        }

        public async Task<List<PostResponseModel>> GetAllPost(Guid userId)
        {
            var data = (from post in _shortStoryDbContext.Post
                         join user in _shortStoryDbContext.User on post.UserId equals user.Id
                         where (userId == null || post.UserId == userId)
                         select new PostResponseModel()
                         {
                             Caption = post.Caption,
                             UserName = user.UserName,
                             Created = post.Created,
                             Id = post.Id,
                             UserId = post.UserId
                         }

                     ).OrderByDescending(o => o.Created).ToList();

            return data;
        }



        public async Task<List<PostResponseModel>> GetAllFollowersPost(Guid UserId)
        {
            var data = (from post in _shortStoryDbContext.Post
                         join folower in _shortStoryDbContext.UserFollowers on post.UserId equals folower.FollowerId
                         join user in _shortStoryDbContext.User on folower.FollowerId equals user.Id
                         where (UserId == null || folower.UserId == UserId)
                         select new PostResponseModel()
                         {
                             Caption = post.Caption,
                             UserName = user.UserName,
                             Created = post.Created,
                             Id = post.Id,
                             UserId = post.UserId
                         }

                     ).OrderByDescending(o=>o.Created).ToList();

            return data;
        }



        public async Task<VowelStats> GetVowelStats()
        {
            var res = _shortStoryDbContext.VowelStats.Where(w => w.Created == DateTime.Now.Date).FirstOrDefault();
            return res;

        }


        public async Task<VowelStats> UpdateVowelStats(VowelStats vowelStats)
        {
            var res = _shortStoryDbContext.VowelStats.Update(vowelStats);
            await _shortStoryDbContext.SaveChangesAsync();
            return res.Entity;

        }
        public async Task<VowelStats> CreateVowelStats(VowelStats vowelStats)
        {
            var res = _shortStoryDbContext.VowelStats.Add(vowelStats);
           await _shortStoryDbContext.SaveChangesAsync();   
            return res.Entity;

        }



    }
}
