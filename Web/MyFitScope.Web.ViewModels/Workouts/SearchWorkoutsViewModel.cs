namespace MyFitScope.Web.ViewModels.Workouts
{
    using MyFitScope.Web.Infrastructure;

    public class SearchWorkoutsViewModel
    {
        public PaginatedList<WorkoutViewModel> Workouts { get; set; }

        public string KeyWord { get; set; }

        public string PageTitle { get; set; }

        public string NoResultsMessage { get; set; }
    }
}
