namespace MyFitScope.Services.Data.Tests.Common
{
    using System.Reflection;

    using MyFitScope.Services.Mapping;
    using MyFitScope.Web.ViewModels.Exercises;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ExerciseViewModel).GetTypeInfo().Assembly);
        }
    }
}
