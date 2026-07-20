using MediatR;

namespace snoperase.Application.Features.Auth.RegisterUser;

public record RegisterUserCommand(string Email, string Password) : IRequest<Unit>;