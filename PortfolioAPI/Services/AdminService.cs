using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using TeamPortfolio.Models;

namespace TeamPortfolio.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMongoCollection<AdminUser> _adminUsers;

        public AdminService(ITeamPortfolioDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _adminUsers = database.GetCollection<AdminUser>("AdminUsers");
        }

        public async Task<AdminUser> Authenticate(string username, string password)
        {
            var admin = await _adminUsers.Find(x => x.Username == username).FirstOrDefaultAsync();

            if (admin == null || !VerifyPasswordHash(password, admin.HashedPassword))
                return null;

            return admin;
        }

        public async Task<AdminUser> CreateAdmin(AdminUser adminUser, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");

            if (await _adminUsers.Find(x => x.Username == adminUser.Username).AnyAsync())
                throw new ArgumentException("Username \"" + adminUser.Username + "\" is already taken");

            adminUser.HashedPassword = CreatePasswordHash(password);

            await _adminUsers.InsertOneAsync(adminUser);
            return adminUser;
        }

        public async Task UpdateAdmin(string id, AdminUser adminUser, string password = null)
        {
            var existingAdmin = await _adminUsers.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (existingAdmin == null)
                throw new ArgumentException("Admin not found");

            if (adminUser.Username != existingAdmin.Username)
            {
                if (await _adminUsers.Find(x => x.Username == adminUser.Username).AnyAsync())
                    throw new ArgumentException("Username " + adminUser.Username + " is already taken");
            }

            existingAdmin.Username = adminUser.Username;
            existingAdmin.Email = adminUser.Email;
            existingAdmin.Role = adminUser.Role;

            if (!string.IsNullOrWhiteSpace(password))
            {
                existingAdmin.HashedPassword = CreatePasswordHash(password);
            }

            await _adminUsers.ReplaceOneAsync(x => x.Id == id, existingAdmin);
        }

        public async Task DeleteAdmin(string id)
        {
            await _adminUsers.DeleteOneAsync(x => x.Id == id);
        }

        private static string CreatePasswordHash(string password)
        {
            // Реализация хеширования пароля (используйте BCrypt или аналоги)
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool VerifyPasswordHash(string password, string storedHash)
        {
            // Проверка пароля
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}