using BE_project.Models;

namespace BE_project.Data.Repositories
{
    public interface IRecordRepository
    {
        Task<IEnumerable<Record>> GetFilteredAsync(int? userId, int? categoryId);
        Task<Record?> GetByIdAsync(int recordId, int userId);
        Task<Record> AddAsync(Record record);
        Task DeleteAsync(int recordId);
    }
}
