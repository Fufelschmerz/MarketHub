namespace MarketHub.Domain.Services.Common.UniqueName.Extensions;

using Abstractions;
using Exceptions;

internal static class UniqueNameServiceExtensions
{
    internal static async Task CheckNameIsPossibleAsync<T>(this UniqueNameService<T> uniqueNameService,
        string newName,
        CancellationToken cancellationToken = default) where T : IHasUniqueName
    {
        T? existingObject = await uniqueNameService.FindWithSameNameAsync(
            newName,
            cancellationToken);

        if (existingObject != null)
            throw new ObjectWithSameNameAlreadyExistsException(
                $"{typeof(T).Name} with name {newName} already exists");
    }
}