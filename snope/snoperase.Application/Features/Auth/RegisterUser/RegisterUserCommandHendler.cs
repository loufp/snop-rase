using MediatR;
using snoperase.Application.Interface;
using snoperase.Domain.Entites;
using snoperase.Domain.Exeptions;

namespace snoperase.Application.Features.Auth.RegisterUser;

public class RegisterUserCommandHendler : IRequestHandler<RegisterUserCommand, Unit>

{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHendler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existing = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (existing is not null)
            throw new UserAlreadyExistsException(request.Email);

        var hashPas = _passwordHasher.Hash(request.Password);

        var user = new User(Guid.NewGuid(),
            request.Username,
            request.Email,
            hashPas);

        await _userRepository.CreateAsync(user, cancellationToken);
        return Unit.Value;
    }
}