using BE_project.Models;

namespace BE_project.Data
{
    public class InMemoryDataStore
    {
        public List<User> Users { get; } = new List<User>();
        public List<Category> Categories { get; } = new List<Category>();
        public List<Record> Records { get; } = new List<Record>();

        private int _nextUserId = 1;
        private int _nextCategoryId = 1;
        private int _nextRecordId = 1;

        public int GetNextUserId() => _nextUserId++;
        public int GetNextCategoryId() => _nextCategoryId++;
        public int GetNextRecordId() => _nextRecordId++;
    }
}
