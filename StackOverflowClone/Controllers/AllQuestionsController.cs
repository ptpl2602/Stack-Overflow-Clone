using StackOverflowClone.ServiceLayer;
using StackOverflowClone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflowClone.Controllers
{
    public class AllQuestionsController : Controller
    {
        private IQuestionsService IquestionService;
        private List<QuestionViewModel> listQuestion;

        public AllQuestionsController(IQuestionsService questionService)
        {
            this.IquestionService = questionService;
        }

        // GET: AllQuestions
        public ActionResult AllQuestions()
        {
            listQuestion = this.IquestionService.GetQuestions().Take(10).ToList();
            return View();
        }
    }
}