namespace MarketHub.Application.Infrastructure.Exceptions.Constants;

using Domain.Exceptions;
using Domain.Exceptions.Accounts;
using Domain.Exceptions.Accounts.Confirmations;
using Domain.Exceptions.Users;
using global::Infrastructure.Application.Exceptions.Attributes;

public static class ApiExceptionExtensionCodes
{
    public const int InvalidToken = 401;
    public const int ObjectNotFound = 404;
    public const int InternalServerError = 500;

    [MapDomainException(typeof(ObjectWithSameNameAlreadyExistsException))]
    public const int ObjectWithSameNameAlreadyExists = 1001;

    [MapDomainException(typeof(UserWithSameEmailAlreadyExistsException))]
    public const int UserWithSameEmailAlreadyExists = 1002;

    [MapDomainException(typeof(InvalidConfirmationTokenException))]
    public const int InvalidConfirmationToken = 1003;

    [MapDomainException(typeof(AccountAlreadyConfirmedException))]
    public const int AccountAlreadyConfirmed = 1004;

    [MapDomainException(typeof(AccountWithSameUserAlreadyExistsException))]
    public const int AccountWithSameUserAlreadyExists = 1005;
    
    public const int WrongCredentials = 1008;
}