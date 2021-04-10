namespace MyFitScope.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "MyFitScope";

        // System Brand Logo Constants:
        public const string BrandPrefix = "My";

        public const string BrandMainFix = "Fit";

        public const string BrandPostFix = "Scope";

        // Roles Constants:
        public const string AdministratorRoleName = "Administrator";

        public const string UserRoleName = "User";

        // Data Seeder Constants:
        public const int UsersEntitiesCount = 3;

        public const int ArticlesEntitiesCount = 3;
        public const string ArticleImageUrl = "https://res.cloudinary.com/myfitscope-cloud/image/upload/v1615231683/data_seeder/Some_public_ID.jpg";
        public const string ArticleImagePublicId = "data_seeder/Some_public_ID.jpg";
        public const string ArticleContent = "It is a long established fact that a reader will be " +
                                                "distracted by the readable content of a page when looking at its layout." +
                                                "The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, " +
                                                "as opposed to using 'Content here, content here', " +
                                                "making it look like readable English." +
                                                "Many desktop publishing packages and web page editors now use Lorem " +
                                                "Ipsum as their default model text, and a search for 'lorem ipsum' " +
                                                "will uncover many web sites still in their infancy. " +
                                                "Various versions have evolved over the years, sometimes by accident, " +
                                                "sometimes on purpose (injected humour and the like).";

        public const string CommentContent = "There are many variations of passages of Lorem Ipsum available, " +
                                                "but the majority have suffered alteration in some form, by injected humour, or randomised words which don't " +
                                                "look even slightly believable.";

        public const string ResponseContent = "All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary";

        public const int ExercisesEntitiesCount = 11; // Count depends from "MuscleGroup" Enum Values - one Exercise for each mucle group!
        public const string ExerciseVideoUrl = "https://www.youtube.com/watch?v=_M2Etme-tfE";
        public const string ExerciseDescription = "It is a long established fact that a reader will be distracted " +
                                                    "by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that " +
                                                    "it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', " +
                                                    "making it look like readable English.";

        public const int WorkoutsEntitiesCount = 4; // Count depends from "WorkoutType" Enum Values - one Workout for each type!
        public const string WorkoutDescription = "There are many variations of passages of Lorem Ipsum available, " +
                                                   "but the majority have suffered alteration in some form, by injected humour, " +
                                                   "or randomised words which don't look even slightly believable.";

        public const int EntitiesCount = 3;

        public const string ArticleCategoryTypeName = "MyFitScope.Data.Models.BlogModels.Enums.ArticleCategory";

        public const string ExerciseCategoryTypeName = "MyFitScope.Data.Models.FitnessModels.Enums.MuscleGroup";

        public const string WorkoutCategoryTypeName = "MyFitScope.Data.Models.FitnessModels.Enums.WorkoutType";

        public const string WorkoutConfirmDeleteMessage = "Confirm that you want to delete workout \"{0}\"?";

        public const string WorkoutDayConfirmDeleteMessage = "Confirm that you want to delete workout day \"{0}\"?";

        public const string ExerciseConfirmDeleteMessage = "Confirm that you want to delete exercise \"{0}\"?";

        public const string ArticleConfirmDeleteMessage = "Confirm that you want to delete article \"{0}\"?";

        public const string CommentConfirmDeleteMessage = "Confirm that you want to delete a comment by \"{0}\"?";

        public const string ResponseConfirmDeleteMessage = "Confirm that you want to delete a response by \"{0}\"?";

        public const string ProgressImageConfirmDeleteMessage = "Confirm that you want to delete an image from \"{0}\"?";

        // Pagination Constants:
        public const int PaginationPageSize = 5;

        public const int PaginationDefaultPageIndex = 1;

        // Cloud Image Folder Constants:
        public const string CloudArticlesImageFolder = "articles_photos";

        public const string CloudUsersImageFolder = "users_photos";

        public const string CloudProgressImageFolder = "progress_photos";

        // Progress Constants:
        public const int ProgressTableHeaderColumnRowsCount = 9;

        // Search Page Constants:
        public const string SearchPageTitle = "Search results for \"{0}\":";
        public const string SearchPageNoResultsMessage = "No results found for \"{0}\"...";
    }
}
