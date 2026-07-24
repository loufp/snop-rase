using MediatR;

namespace snoperase.Application.Features.Auth.RegisterUser;

public record RegisterUserCommand(string Username, string Email, string Password) : IRequest<Unit>;