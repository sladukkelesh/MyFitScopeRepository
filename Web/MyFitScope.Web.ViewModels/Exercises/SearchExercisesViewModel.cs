namespace MyFitScope.Web.ViewModels.Exercises
{
    using MyFitScope.Web.Infrastructure;

    public class SearchExercisesViewModel
    {
        public PaginatedList<ExerciseViewModel> Exercises { get; set; }

        public string KeyWord { get; set; }

        public string PageTitle { get; set; }

        public string NoResultsMessage { get; set; }
    }
}
