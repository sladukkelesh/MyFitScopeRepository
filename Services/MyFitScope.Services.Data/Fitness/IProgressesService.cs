namespace MyFitScope.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using MyFitScope.Web.Infrastructure;
    using MyFitScope.Web.ViewModels.Progresses;

    public interface IProgressesService
    {
        Task CreateStatisticAsync(string userId, double weight, double? biceps, double? chest, double? stomach, double? hips, double? thigh, double? calf);

        Task<PaginatedList<StatisticOutputViewModel>> GetAllStatisticsAsync(int? pageIndex = null);

        Task DeleteStatisticAsync(string statisticId);

        EditProgressStatisticViewModel GetById(string statisticId);

        Task UpdateStatisticAsync(string statisticId, double weight, double? biceps, double? chest, double? stomach, double? hips, double? thigh, double? calf);

        bool StatisticExists(DateTime currentDate, string userId);
    }
}
