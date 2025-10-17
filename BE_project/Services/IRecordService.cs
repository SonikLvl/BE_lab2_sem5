using BE_project.DTOs.Record;

namespace BE_project.Services
{
    public interface IRecordService
    {
        RecordDTO? GetRecordById(int recordId);
        RecordDTO CreateRecord(CreateRecordDTO createRecordDTO);
        void DeleteRecord(int recordId);
        IEnumerable<RecordDTO> GetRecords(int? userId, int? categoryId);

    }
}
