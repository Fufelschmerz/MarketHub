namespace MarketHub.Domain.Tests.Services;

using System.Linq.Expressions;
using AutoFixture;
using Domain.Services.Users;
using Entities.Users;
using Exceptions;
using Moq;
using Repositories.Users;
using Specifications;
using Specifications.Users;
using Xunit;

public sealed class UserServiceTest
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock = new();

    public UserServiceTest()
    {
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturn_ObjectWithSameNameAlreadyExistsException()
    {
        //arrange
        User user = new Fixture()
            .Build<User>()
            .Do(x => x.SetName("CutiePenguin"))
            .Create();
        
        //act
        await _userService.CreateAsync(user);
        
        //accept
        await Assert.ThrowsAsync<ObjectWithSameNameAlreadyExistsException>(() => _userService.CreateAsync(user));
    }
}