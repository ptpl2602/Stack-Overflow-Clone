using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowClone.ServiceLayer;
using StackOverflowClone.ViewModels;

namespace StackOverflowClone.Controllers
{
    public class HomeController : Controller
    {
        private IQuestionsService iQuestionService;
        private ICategoriesService iCategoryService;
     

        public HomeController(IQuestionsService iQuestionService, ICategoriesService iCategoryService)
        {
            this.iQuestionService = iQuestionService;
            this.iCategoryService = iCategoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        // AFTER SIGN UP/ LOG IN
        public ActionResult Index2()
        {
            List<QuestionViewModel> listQuestion = this.iQuestionService.GetQuestions().Take(10).ToList();
            return View(listQuestion);

        }

        [Route("tags")]
        public ActionResult Tags()
        {
            List<CategoryViewModel> listTags = this.iCategoryService.GetCategories();
            return View(listTags);
        }

        [Route("allquestions")]
        public ActionResult Questions()
        {
            List<QuestionViewModel> listQuestions = this.iQuestionService.GetQuestions();
            return View(listQuestions);
        }

        public ActionResult Search(string str)
        {
            List<QuestionViewModel> listQuestions = this.iQuestionService.GetQuestions().Where(i => i.QuestionName.ToLower().Contains(str.ToLower()) || i.Category.CategoryName.ToLower().Contains(str.ToLower())).ToList();
            ViewBag.str = str;
            return View(listQuestions);
        }
    }
}