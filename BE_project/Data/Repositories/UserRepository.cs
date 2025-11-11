using BE_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BE_project.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.AnyAsync(predicate);
        }

        public async Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate) 
        {
            return await _context.Users.FirstOrDefaultAsync(predicate);
        }
    }
}
