namespace MyFitScope.Web.ViewModels.Administration.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class AddRoleInputViewModel
    {
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
