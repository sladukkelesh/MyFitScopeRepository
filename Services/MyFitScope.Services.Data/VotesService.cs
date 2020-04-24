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

        public async Task CreateVoteAsync(string votedObjectName, string votedObjectId, bool isUpVote, string userId)
        {

            if (votedObjectName == "Comment")
            {
                var vote = this.votesRepository.All()
                           .Where(v => v.CommentId == votedObjectId && v.UserId == userId)
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
                        UserId = userId,
                        CommentId = votedObjectId,
                        VoteType = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                    };

                    await this.votesRepository.AddAsync(vote);
                }
            }
            else if (votedObjectName == "Response")
            {
                var vote = this.votesRepository.All()
                           .Where(v => v.ResponseId == votedObjectId && v.UserId == userId)
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
                        UserId = userId,
                        ResponseId = votedObjectId,
                        VoteType = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                    };

                    await this.votesRepository.AddAsync(vote);
                }
            }

            await this.votesRepository.SaveChangesAsync();
        }

        public VoteOutputModel GetVotesCount(string votedObjectName, string objectId)
        {
            var result = new VoteOutputModel();

            if (votedObjectName == "Comment")
            {
                result.UpVotes = this.votesRepository.All()
                                               .Count(v => v.CommentId == objectId && v.VoteType == VoteType.UpVote);
                result.DownVotes = this.votesRepository.All()
                                            .Count(v => v.CommentId == objectId && v.VoteType == VoteType.DownVote);
            }

            if (votedObjectName == "Response")
            {
                result.UpVotes = this.votesRepository.All()
                                               .Count(v => v.ResponseId == objectId && v.VoteType == VoteType.UpVote);
                result.DownVotes = this.votesRepository.All()
                                            .Count(v => v.ResponseId == objectId && v.VoteType == VoteType.DownVote);
            }

            return result;
        }
    }
}
