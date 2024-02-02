using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StackOverflowClone.DomainModels;
using StackOverflowClone.Repositories;
using StackOverflowClone.ViewModels;

namespace StackOverflowClone.ServiceLayer
{
    public interface ICategoriesService
    {
        void InsertCategory(CategoryViewModel viewModel);
        void UpdateCategory(CategoryViewModel viewModel);
        void DeleteCategory(int categoryID);
        int GetQuestionQuantityByCategory(int categoryId);
        List<CategoryViewModel> GetCategoriesWithQuestionCount();
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryById(int CategoryId);
    }
    public class CategoriesService : ICategoriesService
    {
        ICategoriesRepository iCategoriesRepo;
        public CategoriesService()
        {
            iCategoriesRepo = new CategoriesRepository();
        }
        public void InsertCategory(CategoryViewModel viewModel)
        {
            var config = new MapperConfiguration(i => { i.CreateMap<CategoryViewModel, Category>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(viewModel);
            iCategoriesRepo.InsertCategory(category);
        }
        public void UpdateCategory(CategoryViewModel viewModel)
        {
            var config = new MapperConfiguration(i => { i.CreateMap<CategoryViewModel, Category>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(viewModel);
            iCategoriesRepo.UpdateCategory(category);
        }

        public void DeleteCategory(int categoryID)
        {
            iCategoriesRepo.DeleteCategory(categoryID);
        }
        public List<CategoryViewModel> GetCategories()
        {
            /*List<Category> categories = iCategoriesRepo.GetCategories();
            var config = new MapperConfiguration(i => { i.CreateMap<Category, CategoryViewModel>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> viewModel = mapper.Map<List<Category>, List<CategoryViewModel>>(categories);
            return viewModel;*/
            return iCategoriesRepo.GetCategories();
        }

        public CategoryViewModel GetCategoryById(int CategoryId)
        {
            Category category = iCategoriesRepo.GetCategoryById(CategoryId).FirstOrDefault();
            CategoryViewModel viewModel = null;
            if (category != null)
            {
                var config = new MapperConfiguration(i => { i.CreateMap<Category, CategoryViewModel>(); i.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                viewModel = mapper.Map<Category, CategoryViewModel>(category);
            }
            return viewModel;
        }

        public int GetQuestionQuantityByCategory(int categoryId)
        {
            return iCategoriesRepo.GetQuestionQuantityByCategory(categoryId);
        }

        public List<CategoryViewModel> GetCategoriesWithQuestionCount()
        {
            List<Category> categories = iCategoriesRepo.GetCategoriesWithQuestionCount();
            var config = new MapperConfiguration(i => { i.CreateMap<Category, CategoryViewModel>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();

            List<CategoryViewModel> viewModel = mapper.Map<List<Category>, List<CategoryViewModel>>(categories);

            foreach (var category in viewModel)
            {
                category.QuestionCount = iCategoriesRepo.GetQuestionQuantityByCategory(category.CategoryID);
            }

            return viewModel;
        }
    }
}
