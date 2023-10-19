using Moq;
using C_Rest_chat_server.Repositories;
using C_Rest_chat_server.Entities;
using C_Rest_chat_server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CrestChatServer.UnitTests;

public class UsersControllerTests
{
    [Fact]
    public async Task GetUserAsync_UnexistingItem_ReturnsNotFound()
    {
        // Arrange
        var repositoryStub = new Mock<IUsersRepository>();
        repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<Guid>()))
            .ReturnsAsync((User)null);

        var controller = new UsersController(repositoryStub.Object);

        // Act
        var result = await controller.GetUserAsync(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}