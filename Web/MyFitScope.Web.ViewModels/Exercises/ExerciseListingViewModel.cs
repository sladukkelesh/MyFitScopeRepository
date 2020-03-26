namespace MyFitScope.Web.ViewModels.Exercises
{
    using System.Collections.Generic;

    public class ExerciseListingViewModel
    {
        public ICollection<ExerciseViewModel> Exercises { get; set; }

        public string ListedGroupType { get; set; }
    }
}
