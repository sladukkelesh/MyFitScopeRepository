namespace MyFitScope.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyFitScope.Web.ViewModels.NavBarDropdownLinks;

    public class NavBarDropdownLinksServices : INavbarDropdownLinksServices
    {
        private const string MissingUrlErrorMessage = "Url for Navbar dropdown link is missing!";
        private const string MissingEnumNameErrorMessage = "Enum name for Navbar dropdown link is missing!";

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

        public IEnumerable<LinkViewModel> GetLinksCategories(string url, string enumName)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException(MissingUrlErrorMessage);
            }

            if (string.IsNullOrEmpty(enumName))
            {
                throw new ArgumentException(MissingEnumNameErrorMessage);
            }

            var enumType = GetEnumType(enumName);

            return Enum.GetNames(enumType)
                        .Select(ac => new LinkViewModel
                        {
                            CategoryType = ac,
                            Url = url,
                        })
                        .ToList();
        }
    }
}
