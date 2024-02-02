using StackOverflowClone.DomainModels;
using StackOverflowClone.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflowClone.Repositories
{
    public interface ICategoriesRepository
    {
        void InsertCategory (Category category);
        void UpdateCategory (Category category);
        void DeleteCategory (int categoryID); 
        int GetQuestionQuantityByCategory (int categoryID);
        List<CategoryViewModel> GetCategories ();
        List<Category> GetCategoryById (int CategoryId);
        List<Category> GetCategoriesWithQuestionCount();
    }

    public class CategoriesRepository : ICategoriesRepository
    {
        StackOverflowCloneDbContext _dbContext;
        public CategoriesRepository()
        {
            _dbContext = new StackOverflowCloneDbContext();
        }

        public List<CategoryViewModel> GetCategories()
        {
            //Use project onto an anonymous type or onto a DTO as CategoryViewModel because cannot (and should not be able to) project onto a mapped entity
            return (from c in _dbContext.Categories
                    join q in _dbContext.Questions on c.CategoryID equals q.CategoryID into g
                    select new CategoryViewModel
                    {
                        CategoryID = c.CategoryID,
                        CategoryName = c.CategoryName,
                        CategoryDescription = c.CategoryDescription,
                        QuestionCount = g.Count()
                    }).ToList();
        }

        public List<Category> GetCategoryById(int CategoryId)
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
        public void DeleteCategory(int categoryID)
        {
            Category deleteCategory = _dbContext.Categories.FirstOrDefault(i => i.CategoryID == categoryID);
            if (deleteCategory != null)
            {
                _dbContext.Categories.Remove(deleteCategory);
                _dbContext.SaveChanges();
            }
        }

        public int GetQuestionQuantityByCategory (int categoryID)
        {
            int questionCount = _dbContext.Questions.Where(i => i.CategoryID == categoryID).Count();
            return questionCount;
        }

        public List<Category> GetCategoriesWithQuestionCount()
        {
            List<Category> categoriesWithCount = _dbContext.Categories.ToList();

            foreach (var category in categoriesWithCount)
            {
                category.QuestionCount = _dbContext.Questions.Count(q => q.CategoryID == category.CategoryID);
            }

            return categoriesWithCount;
        }
    }
}
