using snoperase.Domain.Entites;

namespace snoperase.Application.Interface;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email,CancellationToken ct);
    //Task<User?> GetByIdAsync(int id, CancellationToken ct);
    Task CreateAsync(User user, CancellationToken ct);
    //Task UpdateAsync(User user, CancellationToken ct);
}
