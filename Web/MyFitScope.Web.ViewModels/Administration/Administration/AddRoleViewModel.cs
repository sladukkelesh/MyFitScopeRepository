namespace MyFitScope.Web.ViewModels.Administration.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class AddRoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
