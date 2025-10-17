using BE_project.Data.Repositories;
using BE_project.DTOs.Record;
using BE_project.DTOs.User;
using BE_project.Models;

namespace BE_project.Services
{
    public class RecordService : IRecordService
    {
        private readonly RecordRepository _recordRepository;

        public RecordService(RecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }
        public RecordDTO CreateRecord(CreateRecordDTO createRecordDTO)
        {
            var newRecord = new Record
            {
                UserId = createRecordDTO.UserId,
                CategoryId = createRecordDTO.CategoryId,
                DateTime = DateTime.Now,
                Amount = createRecordDTO.Amount,
            };

            var savedRecord = _recordRepository.Add(newRecord);

            return new RecordDTO
            {
                Id = savedRecord.Id,
                UserId = savedRecord.UserId,
                CategoryId = savedRecord.CategoryId,
                CreatedDate = savedRecord.DateTime,
                Amount = savedRecord.Amount,
            };
        }

        public void DeleteRecord(int recordId)
        {
            _recordRepository.Delete(recordId);
        }

        public RecordDTO? GetRecordById(int recordId)
        {
            var user = _recordRepository.GetById(recordId);
            if (user == null) return null;

            return new RecordDTO
            {
                Id=user.Id,
                UserId = user.UserId,
                CategoryId = user.CategoryId,
                CreatedDate = user.DateTime,
                Amount = user.Amount,
            };
        }

        public IEnumerable<RecordDTO> GetRecords(int? userId, int? categoryId)
        {
            var recordModels = _recordRepository.GetFiltered(userId, categoryId);

            var recordDtos = recordModels.Select(record => new RecordDTO
            {
                Id = record.Id,
                UserId = record.UserId,
                CategoryId = record.CategoryId,
                CreatedDate = record.DateTime,
                Amount = record.Amount
            });

            return recordDtos;
        }
    }
}
