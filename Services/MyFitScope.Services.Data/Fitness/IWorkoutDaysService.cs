namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.FitnessModels.Enums;
    using MyFitScope.Web.ViewModels.WorkoutDays;

    public interface IWorkoutDaysService
    {
        Task CreateWorkoutDayAsync(string workoutId, WeekDay weekDay);

        EditWorkoutDayViewModel GetWorkoutDayById(string workoutDayId);

        Task DeleteWorkoutDayAsync(string workoutDayId);
    }
}
