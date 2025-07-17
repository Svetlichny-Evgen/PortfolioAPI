using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeamPortfolio.Models;
using TeamPortfolio.Services;
using Xunit;

namespace PortfolioAPITests.Services
{
    public class TeamMemberServiceTests
    {
        private readonly Mock<IMongoCollection<TeamMember>> _mockCollection;
        private readonly TeamMemberService _service;

        public TeamMemberServiceTests()
        {
            _mockCollection = new Mock<IMongoCollection<TeamMember>>();
            var mockDatabase = new Mock<IMongoDatabase>();
            var mockClient = new Mock<IMongoClient>();

            var mockSettings = new Mock<ITeamPortfolioDatabaseSettings>();
            mockSettings.Setup(x => x.DatabaseName).Returns("test_db");
            mockSettings.Setup(x => x.TeamMembersCollectionName).Returns("test_collection");

            mockClient.Setup(x => x.GetDatabase("test_db", null))
                     .Returns(mockDatabase.Object);

            mockDatabase.Setup(x => x.GetCollection<TeamMember>("test_collection", null))
                       .Returns(_mockCollection.Object);

            _service = new TeamMemberService(mockSettings.Object, mockClient.Object);
        }

        [Fact]
        public async Task GetAsync_ReturnsAllMembers()
        {
            // Arrange
            var testMembers = new List<TeamMember>
            {
                new TeamMember { Id = "1", FullName = "John Doe" },
                new TeamMember { Id = "2", FullName = "Jane Smith" }
            };

            var mockCursor = new Mock<IAsyncCursor<TeamMember>>();
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                     .ReturnsAsync(true)
                     .ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(testMembers);

            _mockCollection.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<TeamMember>>(),
                It.IsAny<FindOptions<TeamMember, TeamMember>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("John Doe", result[0].FullName);
        }

        [Fact]
        public async Task GetAsync_ReturnsMember_WhenIdExists()
        {
            // Arrange
            var testMember = new TeamMember { Id = "1", FullName = "John Doe" };

            var mockCursor = new Mock<IAsyncCursor<TeamMember>>();
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                     .ReturnsAsync(true)
                     .ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(new List<TeamMember> { testMember });

            // Исправленный подход для проверки фильтра
            _mockCollection.Setup(x => x.FindAsync(
                It.Is<FilterDefinition<TeamMember>>(f =>
                    f.ToString().Contains("1")), // Простая проверка строкового представления
                It.IsAny<FindOptions<TeamMember, TeamMember>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _service.GetAsync("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.FullName);
        }

        [Fact]
        public async Task CreateAsync_InsertsMember_AndReturnsIt()
        {
            // Arrange
            var newMember = new TeamMember { Id = "1", FullName = "New Member" };

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
            Assert.Equal("New Member", result.FullName);
            Assert.NotNull(result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ReplacesMember_WhenIdExists()
        {
            // Arrange
            var existingId = "1";
            var updatedMember = new TeamMember { Id = existingId, FullName = "Updated Name" };

            var replaceResult = new ReplaceOneResult.Acknowledged(1, 1, null);

            // Упрощенная проверка фильтра
            _mockCollection.Setup(x => x.ReplaceOneAsync(
                It.Is<FilterDefinition<TeamMember>>(f =>
                    f.ToString().Contains(existingId)),
                updatedMember,
                It.IsAny<ReplaceOptions>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(replaceResult)
                .Verifiable();

            // Act
            await _service.UpdateAsync(existingId, updatedMember);

            // Assert
            _mockCollection.Verify();
        }

        [Fact]
        public async Task RemoveAsync_DeletesMember_WhenIdExists()
        {
            // Arrange
            var memberId = "1";

            var deleteResult = new DeleteResult.Acknowledged(1);

            // Упрощенная проверка фильтра
            _mockCollection.Setup(x => x.DeleteOneAsync(
                It.Is<FilterDefinition<TeamMember>>(f =>
                    f.ToString().Contains(memberId)),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(deleteResult)
                .Verifiable();

            // Act
            await _service.RemoveAsync(memberId);

            // Assert
            _mockCollection.Verify();
        }
    }
}