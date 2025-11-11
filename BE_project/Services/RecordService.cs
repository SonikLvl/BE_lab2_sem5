using BE_project.Data.Repositories;
using BE_project.DTOs.Record;
using BE_project.Exceptions;
using BE_project.Models;

namespace BE_project.Services
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public RecordService(IRecordRepository recordRepository,
                               IUserRepository userRepository,
                               ICategoryRepository categoryRepository)
        {
            _recordRepository = recordRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<RecordDTO> CreateRecordAsync(CreateRecordDTO createRecordDTO)
        {
            if (createRecordDTO.Amount <= 0)
            {
                throw new ValidationException("Amount must be greater than zero.");
            }

            var userExists = await _userRepository.GetByIdAsync(createRecordDTO.UserId);
            if (userExists == null)
            {
                throw new NotFoundException($"User with ID {createRecordDTO.UserId} not found. Cannot create record.");
            }

            var categoryExists = await _categoryRepository.GetByIdAsync(createRecordDTO.CategoryId, createRecordDTO.UserId);
            if (categoryExists == null)
            {
                throw new NotFoundException($"Category with ID {createRecordDTO.CategoryId} not found. Cannot create record.");
            }

            var newRecord = new Record
            {
                UserId = createRecordDTO.UserId,
                CategoryId = createRecordDTO.CategoryId,
                DateTime = DateTime.UtcNow, // Логіка сервісу - встановлюємо поточний час
                Amount = createRecordDTO.Amount,
                User = userExists,
                Category = categoryExists,
            };

            var savedRecord = await _recordRepository.AddAsync(newRecord);

            return MapToRecordDTO(savedRecord);
        }

        public async Task DeleteRecordAsync(int recordId, int userId)
        {
            var recordToDelete = await _recordRepository.GetByIdAsync(recordId, userId);
            if (recordToDelete == null)
            {
                throw new NotFoundException($"Record with ID {recordId} not found.");
            }

            await _recordRepository.DeleteAsync(recordId); 
        }

        public async Task<RecordDTO> GetRecordByIdAsync(int recordId, int userId)
        {
            var record = await _recordRepository.GetByIdAsync(recordId, userId);

            if (record == null)
            {
                throw new NotFoundException($"Record with ID {recordId} not found.");
            }

            return MapToRecordDTO(record);
        }

        public async Task<IEnumerable<RecordDTO>> GetRecordsAsync(int? userId, int? categoryId)
        {
            var recordModels = await _recordRepository.GetFilteredAsync(userId, categoryId);
            return recordModels.Select(MapToRecordDTO);
        }

        private RecordDTO MapToRecordDTO(Record record)
        {
            return new RecordDTO
            {
                Id = record.Id,
                UserId = record.UserId,
                CategoryId = record.CategoryId,
                CreatedDate = record.DateTime,
                Amount = record.Amount
            };
        }
    }
}
