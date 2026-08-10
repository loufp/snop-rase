using NSubstitute;
using snoperase.Application.Features.Auth.RegisterUser;
using snoperase.Application.Interface;
using snoperase.Domain.Entites;
using snoperase.Domain.Exeptions;

namespace TestSnopeX.Application.Features.Auth;

public class RegisterCommandHandlerTests
{
    private readonly IUserRepository _repository = Substitute.For<IUserRepository>();
    private readonly IPasswordHasher _passwordHasher = Substitute.For<IPasswordHasher>();

    private RegisterUserCommandHendler NewHendler() => new(_repository, _passwordHasher);

    [Fact]
    public async Task Handle_NewUser_HashPasswordAndCreated()
    {
        _repository.GetByEmailAsync("filseo", Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<User?>(null));
        _passwordHasher.Hash("1234")
            .Returns("hashed_password");

        var handle = NewHendler();
        var cmd = new RegisterUserCommand("kirill", "filseo", "1234");

        await handle.Handle(cmd, CancellationToken.None);

        _passwordHasher.Received().Hash("1234");

        await _repository.Received(1).CreateAsync(
            Arg.Is<User>(u => u.Email == "filseo" && u.Password == "hashed_password"),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_UsersAlreadyExists_ThrowsAndDoesNotCreated()
    {
        _repository.GetByEmailAsync("filseo", Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<User?>(new User(Guid.NewGuid(), "x", "k@k.com", "h")));

        var handle = NewHendler();
        var cmd = new RegisterUserCommand("filseo", "filseo", "1234");

        await Assert.ThrowsAsync<UserAlreadyExistsException>(() => handle.Handle(cmd, CancellationToken.None));

        await _repository.DidNotReceive().CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }
}