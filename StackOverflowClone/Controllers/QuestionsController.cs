using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowClone.CustomFilters;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnswer(NewAnswerViewModel viewModel)
        {
            viewModel.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            viewModel.AnswerDateAndTime = DateTime.Now;
            viewModel.VotesCount = 0;
            viewModel.IsAccepted = false;

            if(ModelState.IsValid)
            {
                this.iAnswersService.InsertAnswer(viewModel);
                return RedirectToAction("View", "Questions", new { id = viewModel.QuestionID});
            }
            else
            {
                ModelState.AddModelError("x", "Invalib Data");
                QuestionViewModel questionViewModel = this.iQuestionService.GetQuestionById(viewModel.QuestionID, viewModel.UserID);
                return View("View", questionViewModel);
            }
        }

        /*[HttpPost]
        public ActionResult AcceptAnswer (int answerId, int questionId)
        {
            var userId = Convert.ToInt32(Session["CurrentUserID"]);
            var answer = this.iAnswersService.GetAnswersByAnswerId(answerId);
            var question = iQuestionService.GetQuestionById(questionId, userId);

            if(answer != null && question != null && question.UserID == userId && question.AnswerID == null)
            {
                iAnswersService.AcceptAnswer(answerId);
                iQuestionService.SetAcceptedAnswer(questionId, answerId);
            }
            return RedirectToAction("View", "Question");
        }*/

        [HttpPost]
        public ActionResult EditAnswer(EditAnswerViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                viewModel.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.iAnswersService.UpdateAnswer(viewModel);
                return RedirectToAction("View", new { id = viewModel.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return RedirectToAction("View", new { id = viewModel.QuestionID });
            }
        }

        public ActionResult CreateQuestion()
        {
            List<CategoryViewModel> categories = this.iCategoryService.GetCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult CreateQuestion(NewQuestionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.AnswersCount = 0;
                viewModel.ViewsCount = 0;
                viewModel.VoteCount = 0;
                viewModel.QuestionDateAndTime = DateTime.Now;
                viewModel.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.iQuestionService.InsertQuestion(viewModel);
                return RedirectToAction("Questions", "Home");
            }
            else
            {
                List<CategoryViewModel> categories = this.iCategoryService.GetCategories();
                ViewBag.Categories = categories;
                ModelState.AddModelError("x", "Invalid Data");
                return View();
            }
        }
    }
}