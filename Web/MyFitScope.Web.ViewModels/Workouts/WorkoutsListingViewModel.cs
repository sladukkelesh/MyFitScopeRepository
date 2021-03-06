﻿namespace MyFitScope.Web.ViewModels.Workouts
{
    using System.Collections.Generic;

    using MyFitScope.Web.Infrastructure;

    public class WorkoutsListingViewModel
    {
        public PaginatedList<WorkoutViewModel> Workouts { get; set; }

        public string WorkoutsCategory { get; set; }

        public string PageTitle { get; set; }

        public string NoResultsMessage { get; set; }
    }
}
