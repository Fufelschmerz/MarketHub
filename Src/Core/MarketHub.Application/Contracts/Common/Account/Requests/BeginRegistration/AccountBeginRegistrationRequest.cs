namespace MarketHub.Application.Contracts.Common.Account.Requests.BeginRegistration;

using Domain.Entities.Users.Roles.Enums;
using MediatR;

public sealed record AccountBeginRegistrationRequest(string Name,
    string Email,
    string Password,
    IEnumerable<RoleType> RoleTypes) : IRequest;