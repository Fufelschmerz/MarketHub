namespace MarketHub.Domain.Tests.Services.Accounts;

using AutoFixture;
using Domain.Services.Accounts;
using Domain.Services.Accounts.Confirmations;
using Entities.Accounts;
using Entities.Accounts.Confirmations;
using Events.Account.Confirmations;
using Exceptions.Accounts;
using Exceptions.Accounts.Confirmations;
using Moq;
using Repositories.Accounts;
using Repositories.Accounts.Confirmations;
using Specifications.Accounts;
using Specifications.Accounts.Confirmations;
using Xunit;

public sealed class AccountServiceTest
{
    private readonly AccountService _accountService;
    private readonly Mock<IAccountRepository> _accountRepositoryMock = new();
    private readonly Mock<IEmailConfirmationRepository> _emailConfirmationRepositoryMock = new();
    private readonly Mock<IConfirmationTokenGenerator> _confirmationTokenGeneratorMock = new();

    public AccountServiceTest()
    {
        _accountService = new AccountService(_accountRepositoryMock.Object,
            _emailConfirmationRepositoryMock.Object,
            _confirmationTokenGeneratorMock.Object);
    }

    [Fact]
    public async Task BeginRegistrationAsync_ShouldReturn_AccountWithSameUserAlreadyExistsException()
    {
        //arrange
        Account fakeAccount = new Fixture()
            .Build<Account>()
            .Create();

        _accountRepositoryMock.Setup(x => x.SingleOrDefaultAsync(
            It.IsAny<AccountByUserSpecification>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(fakeAccount);

        //act
        Task Result() => _accountService.BeginRegistrationAsync(fakeAccount);

        //assert
        await Assert.ThrowsAsync<AccountWithSameUserAlreadyExistsException>(Result);
    }

    [Fact]
    public async Task BeginRegistrationAsync_ShouldReturn_EmailConfirmationRequiredEvent()
    {
        //arrange
        const string emailConfirmationToken = "0af26eca75fa4b9ca095428989c29e48";
        Account fakeAccount = new Fixture()
            .Build<Account>()
            .Create();

        _confirmationTokenGeneratorMock.Setup(x => x.Create())
            .Returns(emailConfirmationToken);

        //act
        await _accountService.BeginRegistrationAsync(fakeAccount);

        //assert
        Assert.Contains(fakeAccount.DomainEvents,
            x => x is EmailConfirmationRequiredEvent);
    }

    [Fact]
    public async Task CompleteRegistrationAsync_ShouldReturn_ConfirmedAccount()
    {
        //arrange
        const string emailConfirmationToken = "0af26eca75fa4b9ca095428989c29e48";
        Account fakeAccount = new Fixture()
            .Build<Account>()
            .Create();

        _emailConfirmationRepositoryMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<EmailConfirmationByTokenSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new EmailConfirmation(fakeAccount.User.Email,
                emailConfirmationToken));

        //act
        await _accountService.CompleteRegistrationAsync(fakeAccount,
            emailConfirmationToken);

        //assert
        Assert.True(fakeAccount.IsConfirmed);
    }

    [Fact]
    public async Task CompleteRegistrationAsync_ShouldReturn_InvalidConfirmationTokenException()
    {
        //arrange
        const string emailConfirmationToken = "0af26eca75fa4b9ca095428989c29e48";
        Account fakeAccount = new Fixture()
            .Build<Account>()
            .Create();
        
        //act
        Task Result() => _accountService.CompleteRegistrationAsync(fakeAccount,
            emailConfirmationToken);
        
        //assert
        await Assert.ThrowsAsync<InvalidConfirmationTokenException>(Result);
    }
}