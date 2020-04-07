using System.Collections.Generic;

namespace MyFitScope.Web.ViewModels.Workouts
{
    public class WorkoutsListingViewModel
    {
        public ICollection<WorkoutViewModel> Workouts { get; set; }

        public string ListedGroupType { get; set; }
    }
}
