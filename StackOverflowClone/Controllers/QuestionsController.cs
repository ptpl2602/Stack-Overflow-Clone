using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowClone.ServiceLayer;
using StackOverflowClone.ViewModels;

namespace StackOverflowClone.Controllers
{
    public class QuestionsController : Controller
    {
        private IQuestionsService iQuestionService;
        private ICategoriesService iCategoryService;
        private IAnswersService iAnswersService;

        public QuestionsController(IQuestionsService iQuestionService, ICategoriesService iCategoryService, IAnswersService iAnswersService)
        {
            this.iQuestionService = iQuestionService;
            this.iCategoryService = iCategoryService;
            this.iAnswersService = iAnswersService;
        }

        public ActionResult View(int id)
        {
            this.iQuestionService.UpdateQuestionViewsCount(id, 1);
            int userID = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel questionViewModel = this.iQuestionService.GetQuestionById(id, userID);
            return View(questionViewModel);
        }
    }
}