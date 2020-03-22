namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyFitScope.Web.ViewModels.NavBarDropdownLinks;

    public class NavBarDropdownLinksServices : INavbarDropdownLinksServices
    {
        public static Type GetEnumType(string enumName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(enumName);
                if (type == null)
                {
                    continue;
                }

                if (type.IsEnum)
                {
                    return type;
                }
            }

            return null;
        }

        public IEnumerable<LinkViewModel> GetLinksCategories(string enumName)
        {
            var enumType = GetEnumType(enumName);

            return Enum.GetNames(enumType)
                        .Select(ac => new LinkViewModel
                        {
                            CategoryType = ac,
                        })
                        .ToList();
        }
    }
}
