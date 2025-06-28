using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TeamPortfolio.Models;

namespace TeamPortfolio.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMongoCollection<AdminUser> _adminUsers;

        public AdminService(
            ITeamPortfolioDatabaseSettings settings,
            IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _adminUsers = database.GetCollection<AdminUser>("AdminUsers");
        }

        public async Task<AdminUser> Authenticate(string username, string password)
        {
            var admin = await _adminUsers.Find(x => x.Username == username).FirstOrDefaultAsync();
            if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.HashedPassword))
                return null;
            return admin;
        }

        public async Task<AdminUser> CreateAdmin(AdminUser adminUser, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");

            if (await _adminUsers.Find(x => x.Username == adminUser.Username).AnyAsync())
                throw new ArgumentException($"Username {adminUser.Username} is already taken");

            adminUser.HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            await _adminUsers.InsertOneAsync(adminUser);
            return adminUser;
        }

        public async Task UpdateAdmin(string id, AdminUser adminUser, string password = null)
        {
            var existingAdmin = await _adminUsers.Find(x => x.Id == id).FirstOrDefaultAsync()
                ?? throw new ArgumentException("Admin not found");

            if (adminUser.Username != existingAdmin.Username &&
                await _adminUsers.Find(x => x.Username == adminUser.Username).AnyAsync())
                throw new ArgumentException($"Username {adminUser.Username} is already taken");

            existingAdmin.Username = adminUser.Username;
            existingAdmin.Email = adminUser.Email;
            existingAdmin.Role = adminUser.Role;

            if (!string.IsNullOrWhiteSpace(password))
                existingAdmin.HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            await _adminUsers.ReplaceOneAsync(x => x.Id == id, existingAdmin);
        }

        public async Task DeleteAdmin(string id) =>
            await _adminUsers.DeleteOneAsync(x => x.Id == id);
    }
}