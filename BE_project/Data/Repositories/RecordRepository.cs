using BE_project.Models;

namespace BE_project.Data.Repositories
{
    public class RecordRepository
    {
        private readonly InMemoryDataStore _dataStore;

        public RecordRepository(InMemoryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Record? GetById(int recordId)
        {
            return _dataStore.Records.FirstOrDefault(r => r.Id == recordId);
        }

        public Record Add(Record record)
        {
            record.Id = _dataStore.GetNextUserId();
            _dataStore.Records.Add(record);
            return record;
        }

        public void Delete(int recordId)
        {
            var record = GetById(recordId);
            if (record != null)
            {
                _dataStore.Records.Remove(record);
            }
        }

        public IEnumerable<Record> GetFiltered(int? userId, int? categoryId)
        {
            IEnumerable<Record> filteredRecords = _dataStore.Records;

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
