using System.Threading.Tasks;
using TeamPortfolio.Models;

namespace TeamPortfolio.Services
{
    public interface IAdminService
    {
        Task<AdminUser> Authenticate(string username, string password);
        Task<AdminUser> CreateAdmin(AdminUser adminUser, string password);
        Task UpdateAdmin(string id, AdminUser adminUser, string password = null);
        Task DeleteAdmin(string id);
    }
}