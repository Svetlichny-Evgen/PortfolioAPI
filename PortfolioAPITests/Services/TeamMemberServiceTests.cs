// Tests/Services/TeamMemberServiceTests.cs
using MongoDB.Driver;
using Moq;
using TeamPortfolio.Models;
using TeamPortfolio.Services;
using Xunit;

namespace PortfolioAPI.Tests.Services
{
    public class TeamMemberServiceTests
    {
        private readonly Mock<IMongoClient> _mockMongoClient;
        private readonly Mock<IMongoDatabase> _mockMongoDatabase;
        private readonly Mock<IMongoCollection<TeamMember>> _mockCollection;
        private readonly TeamMemberService _service;

        public TeamMemberServiceTests()
        {
            // 1. Мокаем зависимости MongoDB
            _mockMongoClient = new Mock<IMongoClient>();
            _mockMongoDatabase = new Mock<IMongoDatabase>();
            _mockCollection = new Mock<IMongoCollection<TeamMember>>();

            // 2. Настраиваем цепочку вызовов
            _mockMongoClient
                .Setup(c => c.GetDatabase(It.IsAny<string>(), null))
                .Returns(_mockMongoDatabase.Object);

            _mockMongoDatabase
                .Setup(db => db.GetCollection<TeamMember>(It.IsAny<string>(), null))
                .Returns(_mockCollection.Object);

            // 3. Мокаем настройки базы данных
            var mockSettings = new Mock<ITeamPortfolioDatabaseSettings>();
            mockSettings.SetupGet(s => s.DatabaseName).Returns("test_db");
            mockSettings.SetupGet(s => s.TeamMembersCollectionName).Returns("test_collection");
            mockSettings.SetupGet(s => s.ConnectionString).Returns("mongodb://localhost:27017");

            // 4. Создаем экземпляр сервиса с моками
            _service = new TeamMemberService(mockSettings.Object, _mockMongoClient.Object);
        }

        [Fact]
        public async Task GetAsync_ReturnsAllMembers()
        {
            // Arrange
            var mockCursor = new Mock<IAsyncCursor<TeamMember>>();
            var testMembers = new List<TeamMember>
            {
                new TeamMember { FullName = "Test User 1" },
                new TeamMember { FullName = "Test User 2" }
            };

            mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                     .ReturnsAsync(true)
                     .ReturnsAsync(false);
            mockCursor.Setup(_ => _.Current).Returns(testMembers);

            _mockCollection.Setup(x => x.FindAsync(
                    It.IsAny<FilterDefinition<TeamMember>>(),
                    It.IsAny<FindOptions<TeamMember, TeamMember>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Test User 1", result[0].FullName);
        }

        [Fact]
        public async Task CreateAsync_InsertsNewMember()
        {
            // Arrange
            var newMember = new TeamMember { FullName = "New User" };

            _mockCollection.Setup(x => x.InsertOneAsync(
                newMember,
                null,
                It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var result = await _service.CreateAsync(newMember);

            // Assert
            _mockCollection.Verify();
            Assert.Equal("New User", result.FullName);
        }
    }
}