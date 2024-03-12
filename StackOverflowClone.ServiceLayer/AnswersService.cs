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
    public interface IAnswersService
    {
        void InsertAnswer(NewAnswerViewModel viewModel);
        void UpdateAnswer(EditAnswerViewModel viewModel);
        void DeleteAnswer(int answerId);
        void UpdateAnswertVotesCount(int answerId, int userId, int value);
        int GetVoteCountByAnswerId (int answerId);
        void AcceptAnswer (int answerId);
        int GetQuestionIdForAnswer(int answerId);
        List<AnswerViewModel> GetAnswersByQuestionId(int questionId);
        AnswerViewModel GetAnswersByAnswerId(int answerId);
    }
    public class AnswersService : IAnswersService
    {
        IAnswerRepository iAnswerRepo;
        IVotesRepository iVoteRepo;

        public AnswersService()
        {
            iAnswerRepo = new AnswersRepository();
            iVoteRepo = new VotesRepository();
        }
        public void InsertAnswer(NewAnswerViewModel viewModel)
        {
            var config = new MapperConfiguration(i => { i.CreateMap<NewAnswerViewModel, Answer>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<NewAnswerViewModel, Answer>(viewModel);
            iAnswerRepo.InsertAnswer(answer);
        }
        public void UpdateAnswer(EditAnswerViewModel viewModel)
        {
            var config = new MapperConfiguration(i => { i.CreateMap<EditAnswerViewModel, Answer>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<EditAnswerViewModel, Answer>(viewModel);
            iAnswerRepo.UpdateAnswer(answer);
        }
        public void DeleteAnswer(int answerId)
        {
            iAnswerRepo.DeleteAnswer(answerId);
        }
        public void UpdateAnswertVotesCount(int answerId, int userId, int value)
        {
            iAnswerRepo.UpdateAnswertVotesCount(answerId, userId, value);
        }
        public List<AnswerViewModel> GetAnswersByQuestionId(int questionId)
        {
            List<Answer> answers = iAnswerRepo.GetAnswersByQuestionId(questionId);
            var config = new MapperConfiguration(i => { i.CreateMap<Answer, AnswerViewModel>(); i.IgnoreUnmapped();
                                                        i.CreateMap<User, UserViewModel>(); i.IgnoreUnmapped();
                                                        i.CreateMap<Question, QuestionViewModel>(); i.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> viewModel = mapper.Map<List<Answer>, List<AnswerViewModel>>(answers);

            return viewModel;
        }
        public AnswerViewModel GetAnswersByAnswerId(int answerId)
        {
            Answer answer = iAnswerRepo.GetAnswersByAnswerId(answerId).FirstOrDefault() ;
            AnswerViewModel viewModel = null;
            if (answer != null)
            {
                var config = new MapperConfiguration(i => { i.CreateMap<Answer, AnswerViewModel>(); i.IgnoreUnmapped();
                                                            i.CreateMap<User, UserViewModel>(); i.IgnoreUnmapped();
                                                            i.CreateMap<Question, QuestionViewModel>(); i.IgnoreUnmapped();
                                                        });
                IMapper mapper = config.CreateMapper();
                viewModel = mapper.Map<Answer, AnswerViewModel>(answer);
            }
            return viewModel;
        }

        public int GetVoteCountByAnswerId (int answerId)
        {
            return iAnswerRepo.GetVoteCountByAnswerId(answerId) ;
        }

        public int GetQuestionIdForAnswer(int answerId)
        {
            return iAnswerRepo.GetQuestionIdForAnswer(answerId) ;
        }

        public void AcceptAnswer(int answerId)
        {
            iAnswerRepo.AcceptAnswer(answerId);
        }
    }
}
