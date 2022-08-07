using ShortStory.Entities;
using ShortStory.IRepos;
using ShortStory.IServices;
using ShortStory.Models.RequestModel;
using ShortStory.Models.ResponseModel;
using System;

namespace ShortStory.Services
{
    public class PostService : IPostService
    {
        public readonly IPostRepo _postRepo;
        public PostService(IPostRepo postRepo)
        {
            _postRepo = postRepo;
        }


        public async Task<PostResponseModel> CreatePost(PostRequestModel requestModel)
        {

            if (requestModel != null)
            {
                var words = requestModel.Caption.ToLower().Split(' ');
                if (words.Length < 10)
                {
                    throw new BadHttpRequestException("post must be contain minimum ten words");
                }
                if (words.Length > 500)
                {
                    throw new BadHttpRequestException("post words count eceeded.");
                }
                await UpdateVowelsSats(words);
                var entity = MapPostRequestToEntity(requestModel);
                var res = await _postRepo.Post(entity).ConfigureAwait(true);


                return MapPostEntityToResponse(res);


            }

            return null;
        }

        public async Task<List<PostResponseModel>> GetAllPost(Guid UserId)
        {
            var res = await _postRepo.GetAllPost(UserId).ConfigureAwait(true);
            return res;
        }

        public async Task<List<PostResponseModel>> GetAllFollowersPost(Guid UserId)
        {
            var res = await _postRepo.GetAllFollowersPost(UserId).ConfigureAwait(true);
            return res;
        }


        private Post MapPostRequestToEntity(PostRequestModel requestModel)
        {

            var post = new Post();
            post.Caption = requestModel.Caption;
            post.Id = new Guid();
            post.UserId = requestModel.UserId;
            post.Created = DateTime.UtcNow;
            post.ImageUrl = "";
            return post;
        }

        private PostResponseModel MapPostEntityToResponse(Post post)
        {
            var response = new PostResponseModel();
            response.Id = post.Id;
            post.UserId = post.UserId;
            response.Created = post.Created;
            response.ImageUrl = post.ImageUrl;
            response.Caption = post.Caption;

            return response;

        }

        private async Task<bool> UpdateVowelsSats(string[]? words)
        {
            bool isUpsated = false;
            int twoVowelsCount = 0;
            int oneVowelCount = 0;
            var vowels = new List<char>() { 'a', 'e', 'i', 'o', 'u' };
            var existingStats = await _postRepo.GetVowelStats().ConfigureAwait(true);
            if (existingStats != null)
            {
                twoVowelsCount = existingStats.PairVowelCount;
                oneVowelCount = existingStats.SingleVowelCount;
            }



            if (words?.Length > 0)
            {
                foreach (var word in words)
                {


                    var pairCountInWord = word.Where((x, i) =>

                (
                    i < word.Length - 1
                    && (
                        vowels.Contains(word[i])
                        &&
                        vowels.Contains(word[i + 1])
                    )

                )

            ).Count();

                    twoVowelsCount = twoVowelsCount + pairCountInWord;


                    var singleCountInWord = word.Count(x => vowels.Contains(x)) - (pairCountInWord * 2);
                    oneVowelCount = oneVowelCount+ singleCountInWord;


                }

                if (existingStats == null)
                {
                    var entity = new VowelStats();
                    entity.PairVowelCount = twoVowelsCount;
                    entity.SingleVowelCount = oneVowelCount;
                    entity.Created = DateTime.Now.Date;
                    entity.TotalCount = twoVowelsCount + oneVowelCount;
                    var res = await _postRepo.CreateVowelStats(entity).ConfigureAwait(true);
                    isUpsated = true;

                }
                else
                {
                    existingStats.PairVowelCount = twoVowelsCount;
                    existingStats.SingleVowelCount = oneVowelCount;
                    existingStats.TotalCount = twoVowelsCount + oneVowelCount;
                    var res = await _postRepo.UpdateVowelStats(existingStats).ConfigureAwait(true);
                    isUpsated = true;
                }

            }

            return isUpsated;

        }


    }
}
