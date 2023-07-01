namespace MarketHub.Domain.Tests.Services.Users.Recoveries;

using AutoFixture;
using Entities.Users;
using Events.Users.Recoveries;
using Infrastructure.Persistence.Repositories;
using MarketHub.Domain.Entities.Users.Recoveries;
using Exceptions.Tokens;
using MarketHub.Domain.Services.Tokens;
using MarketHub.Domain.Services.Users.Recoveries;
using Moq;
using Specifications.Users.Recoveries;
using Xunit;

public sealed class PasswordRecoveryServiceTest
{
    private readonly PasswordRecoveryService _passwordRecoveryService;
    private readonly Mock<IDbRepository<PasswordRecovery>> _passwordRecoveryRepository = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();

    public PasswordRecoveryServiceTest()
    {
        _passwordRecoveryService = new(_passwordRecoveryRepository.Object,
            _tokenServiceMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturn_PasswordRecoveryRequiredEvent()
    {
        //arrange
        const string token = "0af26eca75fa4b9ca095428989c29e48";
        User fakeUser = new Fixture()
            .Build<User>()
            .Create();

        _tokenServiceMock.Setup(x => x.Create())
            .Returns(token);

        //act
        await _passwordRecoveryService.CreateAsync(fakeUser);

        //assert
        Assert.Contains(fakeUser.DomainEvents,
            x => x is PasswordRecoveryRequiredEvent);
    }

    [Fact]
    public async Task RecoverAsync_ShouldReturn_InvalidConfirmationTokenException()
    {
        //arrange
        const string token = "0af26eca75fa4b9ca095428989c29e48";
        const string newPassword = "text_new_password";

        //act
        Task Result() => _passwordRecoveryService.RecoverAsync(token,
            newPassword);

        //assert
        await Assert.ThrowsAsync<InvalidTokenException>(Result);
    }

    [Fact]
    public async Task RecoverAsync_ShouldReturn_NewUserPassword()
    {
        //arrange
        const string token = "0af26eca75fa4b9ca095428989c29e48";
        const string newPassword = "text_new_password";
        User fakeUser = new Fixture()
            .Build<User>()
            .Create();

        _passwordRecoveryRepository.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<PasswordRecoveryByTokenSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PasswordRecovery(fakeUser,
                token));

        //act
        await _passwordRecoveryService.RecoverAsync(token,
            newPassword);

        //assert
        Assert.True(fakeUser.Password.Check(newPassword));
    }
}