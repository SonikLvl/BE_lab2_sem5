using BE_project.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Category?> GetByIdAsync(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<Category?> GetByNameAsync(string categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryId);
        }
    }
}
