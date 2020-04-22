namespace MyFitScope.Web.ViewModels.Exercises
{
    using Ganss.XSS;
    using MyFitScope.Web.Infrastructure;

    public class ExerciseListingViewModel
    {
        public PaginatedList<ExerciseViewModel> Exercises { get; set; }

        public string ExerciseCategory { get; set; }
    }
}
