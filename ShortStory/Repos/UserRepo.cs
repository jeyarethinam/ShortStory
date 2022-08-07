using Microsoft.EntityFrameworkCore;
using ShortStory.DataContext;
using ShortStory.Entities;
using ShortStory.IRepos;
using ShortStory.Models.ResponseModel;


namespace ShortStory.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly ShortStoryDbContext _shortStoryDbContext;
        public UserRepo(ShortStoryDbContext shortStoryDbContext)
        {
            _shortStoryDbContext = shortStoryDbContext;
        }




        public async Task<User> SignUp(User user)
        {

            try
            {
                var res = _shortStoryDbContext.User.Add(user);
                await _shortStoryDbContext.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public async Task<User> GetUSerByUSerName(string userName, string email)
        {
            try
            {
                var res = _shortStoryDbContext.User.Where(w => w.UserName == userName || w.Email == email).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var res = _shortStoryDbContext.User.Where(w => w.Email == email).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var res = _shortStoryDbContext.User.Where(w => w.Id == id).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public async Task<User> UpdateUser(User user)
        {
            try
            {
                var res = _shortStoryDbContext.Update(user);
                await _shortStoryDbContext.SaveChangesAsync();
                return res.Entity;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task<List<FollowingUserModel>> GetFollowingList(Guid UserId)
        {

            var data = await (from userFolowers in _shortStoryDbContext.UserFollowers
                              join users in _shortStoryDbContext.User on userFolowers.FollowerId equals users.Id
                              where userFolowers.UserId == UserId
                              select new FollowingUserModel()
                              {
                                  Id = users.Id,
                                  Email = users.Email,
                                  FirstName = users.FirstName,
                                  LastName = users.LastName,
                                  UserName = users.UserName,
                              }).ToListAsync();

            return data;

        }



        public async Task<UserFollowers> Follow(Guid UserId, Guid FolowerId)
        {
            var folowerEntity = new UserFollowers();

            folowerEntity.Id = new Guid();
            folowerEntity.UserId = UserId;
            folowerEntity.FollowerId = FolowerId;

            var res = _shortStoryDbContext.UserFollowers.Add(folowerEntity);
            await _shortStoryDbContext.SaveChangesAsync();


            return res.Entity;

        }


        public async Task<List<FollowingUserModel>> GetallUsers(string? UserName)
        {

            var data = await (from users in _shortStoryDbContext.User

                              where (UserName == null || users.UserName.Contains(UserName))
                              select new FollowingUserModel()
                              {
                                  Id = users.Id,
                                  Email = users.Email,
                                  FirstName = users.FirstName,
                                  LastName = users.LastName,
                                  UserName = users.UserName,
                              }
                        ).ToListAsync();

            return data;
        }



    }
}
