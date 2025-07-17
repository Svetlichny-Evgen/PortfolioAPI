using Microsoft.AspNetCore.Mvc;
using Moq;
using TeamPortfolio.Controllers;
using TeamPortfolio.DTOs;
using TeamPortfolio.Models;
using TeamPortfolio.Services;

namespace PortfolioAPITests.Controllers
{
    public class TeamMembersControllerTests
    {
        private readonly Mock<ITeamMemberService> _mockService;
        private readonly TeamMembersController _controller;

        public TeamMembersControllerTests()
        {
            _mockService = new Mock<ITeamMemberService>();
            _controller = new TeamMembersController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfMembers()
        {
            // Arrange
            var testMembers = new List<TeamMember>
            {
                new TeamMember { Id = "1", FullName = "John Doe" },
                new TeamMember { Id = "2", FullName = "Jane Smith" }
            };

            _mockService.Setup(x => x.GetAsync())
                       .ReturnsAsync(testMembers);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var members = Assert.IsType<List<TeamMember>>(okResult.Value);
            Assert.Equal(2, members.Count);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenMemberExists()
        {
            // Arrange
            var testMember = new TeamMember { Id = "1", FullName = "John Doe" };
            _mockService.Setup(x => x.GetAsync("1"))
                       .ReturnsAsync(testMember);

            // Act
            var result = await _controller.Get("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var member = Assert.IsType<TeamMember>(okResult.Value);
            Assert.Equal("John Doe", member.FullName);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMemberNotExists()
        {
            // Arrange
            _mockService.Setup(x => x.GetAsync("999"))
                       .ReturnsAsync((TeamMember)null);

            // Act
            var result = await _controller.Get("999");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult_WithNewMember()
        {
            // Arrange
            var newMemberDto = new TeamMemberCreateDTO
            {
                FullName = "New Member",
                Position = "Developer"
            };

            var expectedMember = new TeamMember
            {
                Id = "123",
                FullName = newMemberDto.FullName,
                Position = newMemberDto.Position
            };

            _mockService.Setup(x => x.CreateAsync(It.IsAny<TeamMember>()))
                      .ReturnsAsync(expectedMember)
                      .Callback<TeamMember>(m => m.Id = "123");

            // Act
            var result = await _controller.Create(newMemberDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("Get", createdAtActionResult.ActionName);
            Assert.Equal("123", ((TeamMember)createdAtActionResult.Value).Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenMemberExists()
        {
            // Arrange
            var existingMember = new TeamMember { Id = "1", FullName = "Old Name" };
            var updateDto = new TeamMemberUpdateDTO { FullName = "New Name" };

            _mockService.Setup(x => x.GetAsync("1"))
                      .ReturnsAsync(existingMember);

            // Act
            var result = await _controller.Update("1", updateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(x => x.UpdateAsync("1", It.Is<TeamMember>(m => m.FullName == "New Name")), Times.Once);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenMemberNotExists()
        {
            // Arrange
            _mockService.Setup(x => x.GetAsync("999"))
                      .ReturnsAsync((TeamMember)null);

            // Act
            var result = await _controller.Update("999", new TeamMemberUpdateDTO());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenMemberExists()
        {
            // Arrange
            var existingMember = new TeamMember { Id = "1" };
            _mockService.Setup(x => x.GetAsync("1"))
                      .ReturnsAsync(existingMember);

            // Act
            var result = await _controller.Delete("1");

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(x => x.RemoveAsync("1"), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenMemberNotExists()
        {
            // Arrange
            _mockService.Setup(x => x.GetAsync("999"))
                      .ReturnsAsync((TeamMember)null);

            // Act
            var result = await _controller.Delete("999");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}