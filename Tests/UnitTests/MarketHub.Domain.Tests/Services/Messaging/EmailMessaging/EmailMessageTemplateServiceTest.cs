namespace MarketHub.Domain.Tests.Services.Messaging.EmailMessaging;

using AutoFixture;
using Domain.Services.Messaging.EmailMessaging;
using Entities.Messaging.EmailMessaging;
using Infrastructure.Persistence.Repositories;
using Moq;
using Specifications.Messaging.EmailMessaging;
using Xunit;

public sealed class EmailMessageTemplateServiceTest
{
    private readonly EmailMessageTemplateService _emailMessageTemplateService;
    private readonly Mock<IDbRepository<EmailMessageTemplate>> _emailMessageTemplateRepositoryMock = new();

    public EmailMessageTemplateServiceTest()
    {
        _emailMessageTemplateService = new EmailMessageTemplateService(_emailMessageTemplateRepositoryMock.Object);
    }

    [Fact]
    public async Task ActivateAsync_ShouldReturn_ActiveEmailMessageTemplate()
    {
        //arrange
        EmailMessageTemplate emailMessageTemplate = new Fixture()
            .Build<EmailMessageTemplate>()
            .Do(x => x.SetIsActive(false))
            .Create();
        
        //act
        await _emailMessageTemplateService.ActivateAsync(emailMessageTemplate);
        
        //assert
        Assert.True(emailMessageTemplate.IsActive);
    }

    [Fact]
    public async Task ActiveAsync_ShouldReturn_InactiveCurrentEmailMessageTemplate()
    {
        //arrange
        EmailMessageTemplate emailMessageTemplate = new Fixture()
            .Build<EmailMessageTemplate>()
            .Do(x => x.SetIsActive(false))
            .Create();
        
        EmailMessageTemplate currentEmailMessageTemplate = new Fixture()
            .Build<EmailMessageTemplate>()
            .Do(x => x.SetIsActive(true))
            .Create();
        
        _emailMessageTemplateRepositoryMock.Setup(x => x.SingleOrDefaultAsync(
            It.IsAny<EmailMessageTemplateByTypeActiveSpecification>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(currentEmailMessageTemplate);
        
        //act
        await _emailMessageTemplateService.ActivateAsync(emailMessageTemplate);
        
        //Assert
        Assert.False(currentEmailMessageTemplate.IsActive);
    }
}