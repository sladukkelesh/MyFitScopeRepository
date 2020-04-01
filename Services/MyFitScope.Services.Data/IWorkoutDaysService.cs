namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    using MyFitScope.Data.Models.FitnessModels.Enums;

    public interface IWorkoutDaysService
    {
        Task CreateWorkoutDayAsync(string workoutId, WeekDay weekDay);
    }
}
