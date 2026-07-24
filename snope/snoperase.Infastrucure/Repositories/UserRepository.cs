using Microsoft.EntityFrameworkCore;
using snoperase.Application.Interface;
using snoperase.Domain.Entites;
using snoperase.Infastrucure.Data;

namespace snoperase.Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

    public async Task CreateAsync(User user, CancellationToken ct)
    {
        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);
    }
}