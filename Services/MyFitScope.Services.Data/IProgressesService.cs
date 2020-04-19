namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFitScope.Web.ViewModels.Progresses;

    public interface IProgressesService
    {
        Task CreateStatisticAsync(string userId, double weight, double? biceps, double? chest, double? stomach, double? hips, double? thigh, double? calf);

        List<StatisticOutputViewModel> GetAllStatistics();

        Task DeleteStatisticAsync(string statisticId);
    }
}
