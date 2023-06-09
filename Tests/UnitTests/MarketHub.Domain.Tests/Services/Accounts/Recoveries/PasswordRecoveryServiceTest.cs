namespace MarketHub.Domain.Tests.Services.Accounts.Recoveries;

using AutoFixture;
using Domain.Services.Accounts.Recoveries;
using Domain.Services.Tokens;
using Entities.Accounts;
using Entities.Accounts.Recoveries;
using Events.Account.Recoveries;
using Exceptions.Tokens;
using Moq;
using Repositories.Accounts.Recoveries;
using Specifications.Accounts.Recoveries;
using Xunit;

public sealed class PasswordRecoveryServiceTest
{
    private readonly PasswordRecoveryService _passwordRecoveryService;
    private readonly Mock<IPasswordRecoveryRepository> _passwordRecoveryRepository = new();
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
        Account fakeAccount = new Fixture()
            .Build<Account>()
            .Create();

        _tokenServiceMock.Setup(x => x.Create())
            .Returns(token);

        //act
        await _passwordRecoveryService.CreateAsync(fakeAccount);

        //assert
        Assert.Contains(fakeAccount.DomainEvents,
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
        Account fakeAccount = new Fixture()
            .Build<Account>()
            .Create();

        _passwordRecoveryRepository.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<PasswordRecoveryByTokenSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PasswordRecovery(fakeAccount,
                token));
        
        //act
        await _passwordRecoveryService.RecoverAsync(token,
            newPassword);
        
        //assert
        Assert.True(fakeAccount.User.Password.Check(newPassword));
    }
}