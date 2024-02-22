using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using StackOverflowClone.ServiceLayer;
namespace StackOverflowClone.APIController
{
    public class QuestionsController : ApiController
    {
        private IQuestionsService iQuestionService;
        private IAnswersService iAnswersService;

        public QuestionsController(IAnswersService asr, IQuestionsService qs)
        {
            this.iAnswersService = asr;
            this.iQuestionService = qs;
        }

        public void PostVoteAnswer(int answerId, int userId, int value)
        {
            this.iAnswersService.UpdateAnswertVotesCount(answerId, userId, value);
            /*var updateVoteCount = iAnswersService.GetVoteCountByAnswerId(answerId);
            return Json(new { success = true, votecount = updateVoteCount });*/
        }

        public void PostVoteQuestion(int questionId, int userId, int value)
        {
            this.iQuestionService.UpdateQuestionVoteCount(questionId, userId, value);
        }
    }
}
