namespace MarketHub.Domain.Tests.Services.Accounts.Confirmations;

using AutoFixture;
using Domain.Services.Accounts.Confirmations;
using Domain.Services.Tokens;
using Entities.Accounts;
using Entities.Accounts.Confirmations;
using Events.Accounts.Confirmations;
using Exceptions.Tokens;
using Infrastructure.Persistence.Repositories;
using Moq;
using Specifications.Accounts.Confirmations;
using Xunit;

public sealed class EmailConfirmationServiceTest
{
    private readonly EmailConfirmationService _emailConfirmationService;
    private readonly Mock<IRepository<EmailConfirmation>> _emailConfirmationRepositoryMock = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();

    public EmailConfirmationServiceTest()
    {
        _emailConfirmationService = new EmailConfirmationService(_emailConfirmationRepositoryMock.Object,
            _tokenServiceMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturn_EmailConfirmationRequiredEvent()
    {
        //arrange
        const string token = "0af26eca75fa4b9ca095428989c29e48";
        Account fakeAccount = new Fixture()
            .Build<Account>()
            .Create();

        _tokenServiceMock.Setup(x => x.Create())
            .Returns(token);

        //act
        await _emailConfirmationService.CreateAsync(fakeAccount);

        //assert
        Assert.Contains(fakeAccount.DomainEvents,
            x => x is EmailConfirmationRequiredEvent);
    }

    [Fact]
    public async Task ConfirmAsync_ShouldReturn_InvalidConfirmationTokenException()
    {
        //arrange
        const string token = "0af26eca75fa4b9ca095428989c29e48";

        //act
        Task Result() => _emailConfirmationService.ConfirmAsync(token);

        //assert
        await Assert.ThrowsAsync<InvalidTokenException>(Result);
    }

    [Fact]
    public async Task ConfirmAsync_ShouldReturn_ConfirmedAccount()
    {
        //arrange
        const string token = "0af26eca75fa4b9ca095428989c29e48";
        Account fakeAccount = new Fixture()
            .Build<Account>()
            .Create();

        _emailConfirmationRepositoryMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<EmailConfirmationByTokenSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new EmailConfirmation(fakeAccount,
                token));

        //act
        await _emailConfirmationService.ConfirmAsync(token);

        //assert
        Assert.True(fakeAccount.IsEmailConfirmed);
    }
}