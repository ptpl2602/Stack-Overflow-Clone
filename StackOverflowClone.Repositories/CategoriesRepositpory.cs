using StackOverflowClone.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflowClone.Repositories
{
    public interface ICategoriesRepositpory 
    {
        void InsertCategory (Category category);
        void UpdateCategory (Category category);
        void DeleteCategory (Category category);
        List<Category> GetCategories ();
        List<Category> GetCategoriesById (int CategoryId);
    }

    public class CategoriesRepository : ICategoriesRepositpory
    {
        StackOverflowCloneDbContext _dbContext;
        public CategoriesRepository()
        {
            _dbContext = new StackOverflowCloneDbContext();
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = _dbContext.Categories.ToList();
            return categories;
        }

        public List<Category> GetCategoriesById(int CategoryId)
        {
            List<Category> categories= _dbContext.Categories.Where(i => i.CategoryID == CategoryId).ToList();
            return categories;
        }

        public void InsertCategory (Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            Category updateCategory = _dbContext.Categories.FirstOrDefault(i => i.CategoryID == category.CategoryID);
            if(updateCategory != null)
            {
                updateCategory.CategoryName = category.CategoryName;
                _dbContext.SaveChanges();
            }
        }
        public void DeleteCategory(Category category)
        {
            Category deleteCategory = _dbContext.Categories.FirstOrDefault(i => i.CategoryID == category.CategoryID);
            if (deleteCategory != null)
            {
                _dbContext.Categories.Remove(deleteCategory);
                _dbContext.SaveChanges();
            }
        }
    }
}
