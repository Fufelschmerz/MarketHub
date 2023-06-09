namespace MarketHub.Application.Contracts.Common.Account.Requests.Registration.BeginRegistration;

using Domain.Entities.Users.Roles.Enums;
using MediatR;

public sealed record BeginRegistrationRequest(string Name,
    string Email,
    string Password,
    IEnumerable<RoleType> RoleTypes) : IRequest;