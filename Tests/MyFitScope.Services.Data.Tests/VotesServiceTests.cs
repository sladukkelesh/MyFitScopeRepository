namespace MyFitScope.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Data.Repositories;
    using MyFitScope.Services.Data.Tests.Common;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public async Task TestCreateExerciseAsync_WithValidData_ShouldCreateExerciseCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfRepository<Vote>(context);
            var service = new VotesService(repository);

            await service.CreateVoteAsync("Response", "111", false, "222");

            var expected = VoteType.DownVote;
            var targetVote = repository.All().Where(v => v.ResponseId == "111").FirstOrDefault();
            Assert.Equal(expected, targetVote.VoteType);
        }

        [Fact]
        public void TestGetVotesCount_ShouldReturnCorrectCounts()
        {
            var repository = new Mock<IRepository<Vote>>();
            repository.Setup(r => r.All()).Returns(new List<Vote>()
            {
                new Vote { CommentId = "1111", VoteType = VoteType.UpVote },
                new Vote { CommentId = "1111", VoteType = VoteType.UpVote },
                new Vote { CommentId = "1111", VoteType = VoteType.UpVote },
                new Vote { CommentId = "1111", VoteType = VoteType.DownVote },
            }.AsQueryable());

            var service = new VotesService(repository.Object);
            var votes = service.GetVotesCount("Comment", "1111");

            Assert.Equal(3, votes.UpVotes);
            Assert.Equal(1, votes.DownVotes);


        }
    }
}
