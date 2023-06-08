namespace MarketHub.Application.Infrastructure.Exceptions.Factories;

using System.Reflection;
using Constants;
using global::Infrastructure.Application.Exceptions;
using global::Infrastructure.Application.Exceptions.Attributes;

public static class ApiExceptionFactory
{
    private static readonly Dictionary<Type, int> ExtensionCodes = new();

    static ApiExceptionFactory()
    {
        FieldInfo[] fields = typeof(ApiExceptionExtensionCodes)
            .GetFields(BindingFlags.Static | BindingFlags.Public);

        foreach (FieldInfo fieldInfo in fields)
        {
            MapDomainExceptionAttribute? attribute = fieldInfo.GetCustomAttribute<MapDomainExceptionAttribute>();

            if (attribute is not null)
            {
                object? value = fieldInfo.GetValue(null);

                if (value is int extensionCode)
                {
                    ExtensionCodes.Add(attribute.DomainExceptionType,
                        extensionCode);
                }
            }
        }
    }

    public static ApiException ObjectNotFound => new(ApiExceptionExtensionCodes.ObjectNotFound,
        "Object not found");

    public static ApiException WrongCredentials => new(ApiExceptionExtensionCodes.WrongCredentials,
        "Wrong credentials");
    
    public static ApiException InvalidToken(string tokenType) => new(ApiExceptionExtensionCodes.InvalidToken,
        $"Invalid token. Type: {tokenType}");
    
    public static Exception TryMap(Exception exception)
    {
        return ExtensionCodes.TryGetValue(exception.GetType(),
            out int code)
            ? new ApiException(code,
                exception.Message,
                exception)
            : exception;
    }
}