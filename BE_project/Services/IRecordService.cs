using BE_project.DTOs.Record;

namespace BE_project.Services
{
    public interface IRecordService
    {
        Task<RecordDTO> GetRecordByIdAsync(int recordId, int userId);
        Task<RecordDTO> CreateRecordAsync(CreateRecordDTO createRecordDTO, int userId);
        Task DeleteRecordAsync(int recordId, int userId);
        Task<IEnumerable<RecordDTO>> GetRecordsAsync(int? userId, int? categoryId);
    }
}
