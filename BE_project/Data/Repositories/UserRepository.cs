using BE_project.Models;

namespace BE_project.Data.Repositories
{
    public class UserRepository
    {
        private readonly InMemoryDataStore _dataStore;

        public UserRepository(InMemoryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IEnumerable<User> GetAll()
        {
            return _dataStore.Users;
        }

        public User? GetById(int userId)
        {
            return _dataStore.Users.FirstOrDefault(u => u.Id == userId);
        }

        public User Add(User user)
        {
            user.Id = _dataStore.GetNextUserId();
            _dataStore.Users.Add(user);
            return user;
        }

        public void Delete(int userId)
        {
            var user = GetById(userId);
            if (user != null)
            {
                _dataStore.Users.Remove(user);
            }
        }
    }
}
