using NSubstitute;
using snoperase.Application.Features.Auth.LoginUser;
using snoperase.Application.Interface;
using snoperase.Domain.Entites;
using snoperase.Domain.Exeptions;

namespace TestSnopeX.Application.Features.Auth;

public class LoginCommandHandlerTest
{
    private readonly IUserRepository _repository = NSubstitute.Substitute.For<IUserRepository>();
    private readonly IJwtProvider _jwt = NSubstitute.Substitute.For<IJwtProvider>();
    private readonly IPasswordHasher _passwordHash = NSubstitute.Substitute.For<IPasswordHasher>();

    private LoginUserCommandHendler NewHandler() => new(_repository, _passwordHash, _jwt);

    [Fact]
    public async Task Handle_ValidUserAndPasswordIsCorrect()
    {
        var existUser = new User(Guid.NewGuid(), "kirill", "f@f.com", "123456789");

        _repository.GetByEmailAsync("f@f.com", Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<User?>(existUser));

        _jwt.GenerateJwt(existUser).Returns("token");

        _passwordHash.Verify("123321$2hash", "123456789").Returns(true);

        var hendler = NewHandler();
        var cmd = new LoginUserCommand("f@f.com", "123321$2hash");

        var token = await hendler.Handle(cmd, CancellationToken.None);

        Assert.Equal("token", token);

        _jwt.Received(1).GenerateJwt(existUser);
    }

    [Fact]
    public async Task Handle_InvalidEmail_UserNotFound()
    {
        var exist = new User(Guid.NewGuid(), "kirill", "f@f.com", "123456789");

        _repository.GetByEmailAsync("f$f.com", Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<User?>(null));

        var handler = NewHandler();
        var cmd = new LoginUserCommand("f$fd.com", "123321");

        await Assert.ThrowsAsync<InvalidCreatedDataException>(() => handler.Handle(cmd, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_InvalidPassword_UserFound()
    {
        var usertdo = new User(Guid.NewGuid(), "kirill", "f@f.com", "123456789");

        _repository.GetByEmailAsync("f@f.com", Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<User?>(usertdo));

        _passwordHash.Verify("123321$2hash", "123456789")
            .Returns(false);

        var handler = NewHandler();
        var cmd = new LoginUserCommand("f$f.com", "123321");
        
        await Assert.ThrowsAsync<InvalidCreatedDataException>(() => handler.Handle(cmd, CancellationToken.None));
        
        //await _jwt.DidNotReceive().GenerateJwt(Arg.Any<User>());
    }
}