using snoperase.Application.Interface;
using snoperase.Domain.Exeptions;

namespace snoperase.Application.Features.Auth.LoginUser;

public class LoginUserCommandHendler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserCommandHendler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Hash(LoginUserCommand request, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, ct) ?? throw new InvalidCreatedDataException();

        if (!_passwordHasher.Verify(request.Password, user.Password))
            throw new InvalidCreatedDataException();

        return _jwtProvider.GenerateJwt(user);
    }
}