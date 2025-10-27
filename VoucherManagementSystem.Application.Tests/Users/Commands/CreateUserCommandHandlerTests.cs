using Microsoft.Extensions.Logging;
using Moq;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Users.Commands;
using VoucherManagementSystem.Application.Users.DTOs;
using VoucherManagementSystem.Domain.Entities;
using VoucherManagementSystem.Domain.Interfaces;
using VoucherManagementSystem.Domain.ValueObjects;

namespace VoucherManagementSystem.Application.Tests.Users.Commands;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILogger<CreateUserCommandHandler>> _loggerMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _loggerMock = new Mock<ILogger<CreateUserCommandHandler>>();
        _handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ValidUser_ReturnsSuccessResult()
    {
        // Arrange
        var command = new CreateUserCommand("John", "Doe", "john.doe@example.com");
        var expectedUser = new User(
            Guid.NewGuid().ToString(),
            "John",
            "Doe",
            new Email("john.doe@example.com"),
            DateTime.UtcNow
        );

        _userRepositoryMock
            .Setup(x => x.GetByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync((User?)null);

        _userRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync(expectedUser);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("John", result.Value.FirstName);
        Assert.Equal("Doe", result.Value.LastName);
        Assert.Equal("john.doe@example.com", result.Value.Email);

        _userRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<Email>()), Times.Once);
        _userRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DuplicateEmail_ReturnsFailureResult()
    {
        // Arrange
        var command = new CreateUserCommand("John", "Doe", "john.doe@example.com");
        var existingUser = new User(
            Guid.NewGuid().ToString(),
            "Jane",
            "Smith",
            new Email("john.doe@example.com"),
            DateTime.UtcNow
        );

        _userRepositoryMock
            .Setup(x => x.GetByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("already exists", result.Error);

        _userRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<Email>()), Times.Once);
        _userRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task Handle_InvalidEmail_ReturnsFailureResult()
    {
        // Arrange
        var command = new CreateUserCommand("John", "Doe", "invalid-email");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_RepositoryException_ReturnsFailureResult()
    {
        // Arrange
        var command = new CreateUserCommand("John", "Doe", "john.doe@example.com");

        _userRepositoryMock
            .Setup(x => x.GetByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync((User?)null);

        _userRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<User>()))
            .ThrowsAsync(new Exception("Database connection failed"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Failed to create user", result.Error);
    }
}
