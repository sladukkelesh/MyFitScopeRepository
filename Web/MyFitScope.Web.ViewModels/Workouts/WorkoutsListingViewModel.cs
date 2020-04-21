namespace MyFitScope.Web.ViewModels.Workouts
{
    using System.Collections.Generic;

    using MyFitScope.Web.Infrastructure;

    public class WorkoutsListingViewModel
    {
        public PaginatedList<WorkoutViewModel> Workouts { get; set; }

        public string WorkoutCategory { get; set; }
    }
}
