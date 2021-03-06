﻿namespace MyFitScope.Web.ViewModels.Exercises
{
    using MyFitScope.Web.Infrastructure;

    public class ExerciseListingViewModel
    {
        public PaginatedList<ExerciseViewModel> Exercises { get; set; }

        public string ExercisesCategory { get; set; }

        public string PageTitle { get; set; }

        public string NoResultsMessage { get; set; }
    }
}
