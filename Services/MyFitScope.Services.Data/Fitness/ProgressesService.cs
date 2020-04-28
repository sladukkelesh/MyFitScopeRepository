namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFitScope.Common;
    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.Infrastructure;
    using MyFitScope.Web.ViewModels.Progresses;
    using shortid;

    public class ProgressesService : IProgressesService
    {
        private const string InvalidStatisticIdErrorMessage = "Progress Statistic with ID: {0} does not exist.";
        private readonly IDeletableEntityRepository<Progress> progressesRepository;

        public ProgressesService(IDeletableEntityRepository<Progress> progressesRepository)
        {
            this.progressesRepository = progressesRepository;
        }

        public async Task CreateStatisticAsync(string userId, double weight, double? biceps = null, double? chest = null, double? stomach = null, double? hips = null, double? thigh = null, double? calf = null)
        {
            var statistic = new Progress
            {
                UserId = userId,
                Weight = weight,
                Biceps = biceps,
                Chest = chest,
                Stomach = stomach,
                Hips = hips,
                Thigh = thigh,
                Calf = calf,
            };

            await this.progressesRepository.AddAsync(statistic);
            await this.progressesRepository.SaveChangesAsync();
        }

        public async Task DeleteStatisticAsync(string statisticId)
        {
            var statisticToDelete = await this.progressesRepository.GetByIdWithDeletedAsync(statisticId);

            if (statisticToDelete == null)
            {
                throw new ArgumentNullException(
                string.Format(InvalidStatisticIdErrorMessage, statisticId));
            }

            statisticToDelete.IsDeleted = true;

            await this.progressesRepository.SaveChangesAsync();
        }

        public async Task<PaginatedList<StatisticOutputViewModel>> GetAllStatisticsAsync(int? pageIndex = null)
        {
            var result = this.progressesRepository.All()
                   .OrderByDescending(p => p.CreatedOn);

            return await PaginatedList<StatisticOutputViewModel>.CreateAsync(result.To<StatisticOutputViewModel>(), pageIndex ?? GlobalConstants.PaginationDefaultPageIndex, GlobalConstants.PaginationPageSize);
        }

        public EditProgressStatisticViewModel GetById(string statisticId)
        {
            var statistic = this.progressesRepository.All()
                                .Where(p => p.Id == statisticId)
                                .To<EditProgressStatisticViewModel>()
                                .FirstOrDefault();

            if (statistic == null)
            {
                throw new ArgumentNullException(
                string.Format(InvalidStatisticIdErrorMessage, statisticId));
            }

            return statistic;
        }

        public bool StatisticExists(DateTime currentDate, string userId)
        {
            return this.progressesRepository.All().Any(p => p.UserId == userId && DateTime.Equals(p.CreatedOn.Date, currentDate.Date));
        }

        public async Task UpdateStatisticAsync(string statisticId, double weight, double? biceps, double? chest, double? stomach, double? hips, double? thigh, double? calf)
        {
            var statisticToUpdate = await this.progressesRepository.GetByIdWithDeletedAsync(statisticId);

            if (statisticToUpdate == null)
            {
                throw new ArgumentNullException(
                string.Format(InvalidStatisticIdErrorMessage, statisticId));
            }

            statisticToUpdate.Weight = weight;
            statisticToUpdate.Biceps = biceps;
            statisticToUpdate.Chest = chest;
            statisticToUpdate.Stomach = stomach;
            statisticToUpdate.Hips = hips;
            statisticToUpdate.Thigh = thigh;
            statisticToUpdate.Calf = calf;

            this.progressesRepository.Update(statisticToUpdate);
            await this.progressesRepository.SaveChangesAsync();
        }
    }
}
