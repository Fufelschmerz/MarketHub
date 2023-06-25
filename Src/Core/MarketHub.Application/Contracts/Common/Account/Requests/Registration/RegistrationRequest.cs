namespace MarketHub.Application.Contracts.Common.Account.Requests.Registration;

using Domain.Entities.Users.Roles.Enums;
using MediatR;

public sealed record RegistrationRequest(string Name,
    string Email,
    string Password,
    RoleType[] RoleTypes) : IRequest;