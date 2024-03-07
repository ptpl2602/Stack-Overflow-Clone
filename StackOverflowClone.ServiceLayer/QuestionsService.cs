using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowClone.DomainModels;
using StackOverflowClone.Repositories;
using StackOverflowClone.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowClone.ServiceLayer
{
    public interface IQuestionsService
    {
        void InsertQuestion(NewQuestionViewModel viewModel);
        void UpdateQuestionDetails(EditQuestionViewModel viewModel);
        void UpdateQuestionVoteCount(int questionId, int userId, int value);
        void UpdateQuestionAnswersCount(int id, int value);
        void UpdateQuestionViewsCount(int id, int value);
        void DeleteQuestion(int questionId);
        /*void SetAcceptedAnswer (int questionId, int answerId);*/
        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionById(int questionId, int UserId);
        int GetUserIdByQuestionId(int questionId);
    }
    public class QuestionsService : IQuestionsService
    {
        private IQuestionsRepository iQuestionsRepo;

        public QuestionsService()
        {
            iQuestionsRepo = new QuestionsRepository();

        }
        public void InsertQuestion(NewQuestionViewModel viewModel)
        {
            var config = new MapperConfiguration(i => { i.CreateMap<NewQuestionViewModel, Question>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<NewQuestionViewModel, Question>(viewModel);
            iQuestionsRepo.InsertQuestion(question);
        }

        public void UpdateQuestionDetails(EditQuestionViewModel viewModel)
        {
            var config = new MapperConfiguration(i => { i.CreateMap<EditQuestionViewModel, Question>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<EditQuestionViewModel, Question>(viewModel);
            iQuestionsRepo.UpdateQuestionDetails(question);
        }
        public void UpdateQuestionVoteCount(int questionId, int userId, int value)
        {
            iQuestionsRepo.UpdateQuestionVoteCount(questionId, userId, value);
        }
        public void UpdateQuestionAnswersCount(int id, int value)
        {
            iQuestionsRepo.UpdateQuestionAnswersCount(id, value);
        }
        public void UpdateQuestionViewsCount(int id, int value)
        {
            iQuestionsRepo.UpdateQuestionViewsCount(id, value);
        }
        public void DeleteQuestion(int questionId)
        {
            iQuestionsRepo.DeleteQuestion(questionId);
        }
/*        public void SetAcceptedAnswer(int questionId, int answerId)
        {
            iQuestionsRepo.SetAcceptedAnswer(questionId, answerId);
        }*/
        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> questions = iQuestionsRepo.GetQuestions();
            var config = new MapperConfiguration(i => { i.CreateMap<Question, QuestionViewModel>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> viewModel = mapper.Map<List<Question>, List<QuestionViewModel>>(questions);
            return viewModel;
        }

        public QuestionViewModel GetQuestionById(int questionId, int UserId = 0)
        {
            Question question = iQuestionsRepo.GetQuestionsById(questionId).FirstOrDefault();
            QuestionViewModel viewModel = null;
            if (question != null)
            {
                var config = new MapperConfiguration(i => { i.CreateMap<Question, QuestionViewModel>(); i.IgnoreUnmapped();
                                                            i.CreateMap<User, UserViewModel>(); i.IgnoreUnmapped();
                                                            i.CreateMap<Category, CategoryViewModel>(); i.IgnoreUnmapped();
                                                            i.CreateMap<Answer, AnswerViewModel>(); i.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                viewModel = mapper.Map<Question, QuestionViewModel>(question);
                viewModel.CurrentUserVoteType = 0;
                var votequestion = question.VotesQuestions.FirstOrDefault(i => i.UserID == UserId);
                if(votequestion != null)
                {
                    viewModel.CurrentUserVoteType = votequestion.VoteValue;
                }

                foreach(var i in viewModel.Answers)
                {
                    i.CurrentUserVoteType = 0;
                    VoteViewModel vote = i.Votes.FirstOrDefault(t => t.UserID == UserId);
                    if(vote != null)
                    {
                        i.CurrentUserVoteType = vote.VoteValue;
                    }
                }
            }
            return viewModel;
        }
        public int GetUserIdByQuestionId(int questionId)
        {
            return iQuestionsRepo.GetUserIdByQuestionId(questionId);
        }
    }
}
