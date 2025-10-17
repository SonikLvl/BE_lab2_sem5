using BE_project.Models;

namespace BE_project.Data.Repositories
{
    public class CategoryRepository
    {
        private readonly InMemoryDataStore _dataStore;

        public CategoryRepository(InMemoryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IEnumerable<Category> GetAll()
        {
            return _dataStore.Categories;
        }

        public Category Add(Category category)
        {
            category.Id = _dataStore.GetNextCategoryId();
            _dataStore.Categories.Add(category);
            return category;
        }

        public void Delete(int categoryId)
        {
            var category = _dataStore.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category != null)
            {
                _dataStore.Categories.Remove(category);
            }
        }
    }
}
