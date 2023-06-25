namespace MarketHub.Domain.Tests.Services.Users;

using AutoFixture;
using MarketHub.Domain.Entities.Users;
using Exceptions;
using Infrastructure.Persistence.Repositories;
using MarketHub.Domain.Exceptions.Users;
using MarketHub.Domain.Services.Users;
using Specifications;
using Moq;
using Specifications.Users;
using Xunit;

public sealed class UserServiceTest
{
    private readonly UserService _userService;
    private readonly Mock<IRepository<User>> _userRepositoryMock = new();

    public UserServiceTest()
    {
        _userService = new UserService(_userRepositoryMock.Object);
    }
    
    [Fact]
    public async Task CreateAsync_ShouldReturn_ObjectWithSameNameAlreadyExistsException()
    {
        //arrange
        User fakeUser = new Fixture()
            .Build<User>()
            .Do(x => x.SetName("CutiePenguin"))
            .Create();

        _userRepositoryMock.Setup(x => x.SingleOrDefaultAsync(It.IsAny<EntityByNameSpecification<User>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeUser);

        //act
        Task Result() => _userService.CreateAsync(fakeUser);

        //assert
        await Assert.ThrowsAsync<ObjectWithSameNameAlreadyExistsException>(Result);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturn_UserWithSameEmailAlreadyExistsException()
    {
        //arrange
        User fakeUser = new Fixture()
            .Build<User>()
            .Do(x => x.SetEmail("CutiePenguin@gmail.com"))
            .Create();
        
        _userRepositoryMock.Setup(x=> x.SingleOrDefaultAsync(It.IsAny<UserByEmailSpecification>(),
            It.IsAny<CancellationToken>()))            
            .ReturnsAsync(fakeUser);
        
        //act
        Task Result() => _userService.CreateAsync(fakeUser);
        
        //assert
        await Assert.ThrowsAsync<UserWithSameEmailAlreadyExistsException>(Result);
    }
}