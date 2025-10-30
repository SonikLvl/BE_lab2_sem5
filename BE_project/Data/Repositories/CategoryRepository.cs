using BE_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BE_project.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext dataStore)
        {
            _context = dataStore;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetCategoriesForUserAsync(int userId)
        {
            return await _context.Categories
                .Where(c => c.UserId == null || c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);

            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Category?> GetByIdAsync(int categoryId, int? userId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && (c.UserId == null || c.UserId == userId));
        }

        public async Task<Category?> GetByNameAsync(string categoryName, int? userId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName && (c.UserId == null || c.UserId == userId));
        }
    }
}
