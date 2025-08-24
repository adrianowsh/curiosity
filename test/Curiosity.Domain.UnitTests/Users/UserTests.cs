using Curiosity.Domain.UnitTests.Infrastructure;
using Curiosity.Domain.Users;
using Curiosity.Domain.Users.Events;
using FluentAssertions;

namespace Curiosity.Domain.UnitTests.Users;

public class UserTests: BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValues()
    {
        // Action
        var user = User.Create(
            UserData.Name,
            UserData.Email,
            UserData.Active);

        // Assert
        Assert.Equal(UserData.Name, user.Name);
        Assert.Equal(UserData.Email, user.Email);
        Assert.Equal(UserData.Active, Status.Active);
    }

    [Fact]
    public void Create_Should_RaiseUserCreatedDomainEvent()
    {
        // Action
        var user = User.Create(
            UserData.Name,
            UserData.Email,
            UserData.Active);

        // Assert
        UserCreatedDomainEvent userCreatedDomainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

        userCreatedDomainEvent.Should().NotBeNull();

        userCreatedDomainEvent.UserId.Should().Be(user.Id);
    }
}
