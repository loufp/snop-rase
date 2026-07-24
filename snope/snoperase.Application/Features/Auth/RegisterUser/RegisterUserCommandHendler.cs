using MediatR;
using snoperase.Application.Interface;

namespace snoperase.Application.Features.Auth.RegisterUser;

public class RegisterUserCommandHendler : IRequestHandler<RegisterUserCommand, Unit>

{
    private readonly IUserRepository  _userRepository;
    
}