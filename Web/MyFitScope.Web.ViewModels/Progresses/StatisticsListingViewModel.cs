namespace MyFitScope.Web.ViewModels.Progresses
{
    using MyFitScope.Web.Infrastructure;

    public class StatisticsListingViewModel
    {
        public PaginatedList<StatisticOutputViewModel> Statistics { get; set; }
    }
}
