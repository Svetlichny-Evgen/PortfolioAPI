using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPortfolio.Controllers;
using TeamPortfolio.Models;
using TeamPortfolio.Services;

namespace PortfolioAPITests.Controllers
{
    public class TeamMembersControllerTests
    {
        [Fact]
        public async Task Get_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<ITeamMemberService>();
            mockService.Setup(service => service.GetAsync())
                       .ReturnsAsync(new List<TeamMember>());

            var controller = new TeamMembersController(mockService.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
