namespace MyFitScope.Services.Data
{
    using System.Threading.Tasks;

    public interface IAdministrationService
    {
        Task CreateRoleAsync(string name);
    }
}
