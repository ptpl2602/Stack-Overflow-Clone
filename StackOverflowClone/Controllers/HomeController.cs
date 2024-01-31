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
     

        public HomeController(IQuestionsService iQuestionService)
        {
            this.iQuestionService = iQuestionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            List<QuestionViewModel> listQuestion = this.iQuestionService.GetQuestions().Take(10).ToList();
            return View(listQuestion);

        }
    }
}