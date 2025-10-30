using BE_project.DTOs.Record;

namespace BE_project.Services
{
    public interface IRecordService
    {
        Task<RecordDTO> GetRecordByIdAsync(int recordId);
        Task<RecordDTO> CreateRecordAsync(CreateRecordDTO createRecordDTO);
        Task DeleteRecordAsync(int recordId);
        Task<IEnumerable<RecordDTO>> GetRecordsAsync(int? userId, int? categoryId);
    }
}
