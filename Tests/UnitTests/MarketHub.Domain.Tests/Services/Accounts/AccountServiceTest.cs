namespace MarketHub.Domain.Tests.Services.Accounts;

using AutoFixture;
using Domain.Services.Accounts;
using Domain.Services.Accounts.Confirmations;
using Entities.Accounts;
using Exceptions.Accounts;
using Infrastructure.Persistence.Repositories;
using Moq;
using Specifications.Accounts;
using Xunit;

public sealed class AccountServiceTest
{
    private readonly AccountService _accountService;
    private readonly Mock<IRepository<Account>> _accountRepositoryMock = new();
    private readonly Mock<IEmailConfirmationService> _emailConfirmationServiceMock = new(); 

    public AccountServiceTest()
    {
        _accountService = new AccountService(_accountRepositoryMock.Object,
            _emailConfirmationServiceMock.Object);
    }

    [Fact]
    public async Task RegistrationAsync_ShouldReturn_AccountWithSameUserAlreadyExistsException()
    {
        //arrange
        Account fakeAccount = new Fixture()
            .Build<Account>()
            .Create();
        
        _accountRepositoryMock.Setup(x => x.SingleOrDefaultAsync(
            It.IsAny<AccountByUserSpecification>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(fakeAccount);
        
        //act
        Task Result() => _accountService.RegistrationAsync(fakeAccount);
        
        //assert
        await Assert.ThrowsAsync<AccountWithSameUserAlreadyExistsException>(Result);
    }
}