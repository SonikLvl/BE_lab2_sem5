using BE_project.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_project.Data.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public RecordRepository(ApplicationDbContext dataStore)
        {
            _context = dataStore;
        }

        public async Task<Record?> GetByIdAsync(int recordId, int userId)
        {
            return await _context.Records.FirstOrDefaultAsync(r => r.Id == recordId && r.UserId == userId);
        }

        public async Task<Record> AddAsync(Record record)
        {
            _context.Records.Add(record);

            await _context.SaveChangesAsync();
            return record;
        }

        public async Task DeleteAsync(int recordId)
        {
            var record = await _context.Records.FindAsync(recordId);
            if (record != null)
            {
                _context.Records.Remove(record);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Record>> GetFilteredAsync(int? userId, int? categoryId)
        {
            IEnumerable<Record> filteredRecords = await _context.Records.ToListAsync();

            if (userId.HasValue)
            {
                filteredRecords = filteredRecords.Where(r => r.UserId == userId.Value);
            }

            if (categoryId.HasValue)
            {
                filteredRecords = filteredRecords.Where(r => r.CategoryId == categoryId.Value);
            }

            return filteredRecords.ToList();
        }
    }
}
