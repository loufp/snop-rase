using MediatR;

namespace snoperase.Application.Features.Auth.LoginUser;

public record LoginUserCommand(string Email, string Password) : IRequest<string>;
