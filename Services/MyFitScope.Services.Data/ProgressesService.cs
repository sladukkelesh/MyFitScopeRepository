namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFitScope.Data.Common.Repositories;
    using MyFitScope.Data.Models.FitnessModels;
    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Progresses;

    public class ProgressesService : IProgressesService
    {
        private readonly IDeletableEntityRepository<Progress> progressesRepository;

        public ProgressesService(IDeletableEntityRepository<Progress> progressesRepository)
        {
            this.progressesRepository = progressesRepository;
        }

        public async Task CreateStatisticAsync(double weight, double? biceps = null, double? chest = null, double? stomach = null, double? hips = null, double? thigh = null, double? calf = null)
        {
            var statistic = new Progress
            {
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

            statisticToDelete.IsDeleted = true;

            await this.progressesRepository.SaveChangesAsync();
        }

        public List<StatisticOutputViewModel> GetAllStatistics()
            => this.progressesRepository.All()
                   .OrderBy(p => p.CreatedOn)
                   .To<StatisticOutputViewModel>()
                   .ToList();
    }
}
