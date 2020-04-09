namespace MyFitScope.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFitScope.Web.ViewModels.Progresses;

    public interface IProgressesService
    {
        Task CreateStatisticAsync(double weight, double? biceps, double? chest, double? stomach, double? hips, double? thigh, double? calf);

        List<StatisticOutputViewModel> GetAllStatistics();

        Task DeleteStatisticAsync(string statisticId);
    }
}
