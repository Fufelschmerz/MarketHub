namespace MarketHub.Application.Infrastructure.Exceptions.Constants;

using Domain.Exceptions;
using Domain.Exceptions.Accounts;
using Domain.Exceptions.Accounts.Confirmations;
using Domain.Exceptions.Tokens;
using Domain.Exceptions.Users;
using Domain.Exceptions.Users.Recoveries;
using global::Infrastructure.Application.Exceptions.Attributes;

public static class ApiExceptionExtensionCodes
{
    public const int Unauthorized = 401;
    public const int ObjectNotFound = 404;
    public const int InternalServerError = 500;

    [MapDomainException(typeof(ObjectWithSameNameAlreadyExistsException))]
    public const int ObjectWithSameNameAlreadyExists = 1001;

    [MapDomainException(typeof(UserWithSameEmailAlreadyExistsException))]
    public const int UserWithSameEmailAlreadyExists = 1002;

    [MapDomainException(typeof(InvalidTokenException))]
    public const int InvalidTokenException = 1003;

    [MapDomainException(typeof(EmailAlreadyConfirmedException))]
    public const int EmailAlreadyConfirmedException = 1004;

    [MapDomainException(typeof(AccountWithSameUserAlreadyExistsException))]
    public const int AccountWithSameUserAlreadyExists = 1005;

    [MapDomainException(typeof(PasswordRecoveryExpiredException))]
    public const int PasswordRecoveryExpiredException = 1006;
    
    public const int WrongCredentials = 1100;
}