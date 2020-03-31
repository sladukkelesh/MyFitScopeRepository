namespace MyFitScope.Web.ViewModels.Administration.Administration
{
    using System.Collections.Generic;

    public class EditUsersInRoleViewModel
    {
        public string RoleName { get; set; }

        public IEnumerable<UsersInRoleViewModel> Users { get; set; }
    }
}
