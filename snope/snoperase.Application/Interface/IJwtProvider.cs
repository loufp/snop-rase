using snoperase.Domain.Entites;

namespace snoperase.Application.Interface;

public interface IJwtProvider
{
    string GenerateJwt(User user);
}