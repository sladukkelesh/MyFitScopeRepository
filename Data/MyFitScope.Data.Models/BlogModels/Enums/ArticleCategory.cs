namespace MyFitScope.Data.Models.BlogModels.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ArticleCategory
    {
        // If category name contains more than 1 word, separate them with "_"!!!
        Fitness = 1,
        Food = 2,
        [Display(Name = "Life Style")]
        Life_Style = 3,
    }
}
