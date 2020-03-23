namespace MyFitScope.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models;
    using MyFitScope.Web.ViewModels.Votes;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public async Task CreateCommentVoteAsync(string commentId, bool isUpVote, string userId)
        {
            var vote = this.votesRepository.All()
                           .Where(v => v.CommentId == commentId && v.UserId == userId)
                           .FirstOrDefault();

            if (vote != null)
            {
                // If vote exists, we can chang it to down or up vote:
                vote.VoteType = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                vote = new Vote
                {
                    Id = Guid.NewGuid().ToString(),
                    CommentId = commentId,
                    UserId = userId,
                    VoteType = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }

        public VoteOutputModel GetCommentVotesCount(string commentId)
                => new VoteOutputModel
                {
                    UpVotes = this.votesRepository.All()
                                 .Count(v => v.CommentId == commentId && v.VoteType == VoteType.UpVote),
                    DownVotes = this.votesRepository.All()
                                 .Count(v => v.CommentId == commentId && v.VoteType == VoteType.DownVote),
                };
    }
}
