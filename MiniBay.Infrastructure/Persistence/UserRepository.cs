using Microsoft.EntityFrameworkCore;
using MiniBay.Application.Interfaces;
using MiniBay.Domain.Entities;
using MiniBay.Infrastructure.Data;

namespace MiniBay.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly MiniBayDbContext _context;

        public UserRepository(MiniBayDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}