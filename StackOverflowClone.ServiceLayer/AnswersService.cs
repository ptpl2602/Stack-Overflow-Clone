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
        void InsertAnswer(AnswerViewModel viewModel);
        void DeleteAnswer(int answerId);
        void UpdateAnswertVotesCount(int answerId, int userId, int value);
        List<AnswerViewModel> GetAnswersByQuestionId(int questionId);
        AnswerViewModel GetAnswersByAnswerId(int answerId);
    }
    public class AnswersService
    {
        IAnswerRepository iAnswerRepo;
        IVotesRepository iVoteRepo;

        public AnswersService()
        {
            iAnswerRepo = new AnswersRepository();
            iVoteRepo = new VotesRepository();
        }
        public void InsertAnswer(AnswerViewModel viewModel)
        {
            var config = new MapperConfiguration(i => { i.CreateMap<NewAnswerViewModel, Answer>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<AnswerViewModel, Answer>(viewModel);
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
            var config = new MapperConfiguration(i => { i.CreateMap<Answer, AnswerViewModel>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> viewModel = mapper.Map < List<Answer>, List<AnswerViewModel>>(answers);

            return viewModel;
        }
        public AnswerViewModel GetAnswersByAnswerId(int answerId)
        {
            Answer answer = iAnswerRepo.GetAnswersByAnswerId(answerId).FirstOrDefault() ;
            AnswerViewModel viewModel = null;
            if (answer != null)
            {
                var config = new MapperConfiguration(i => { i.CreateMap<Answer, AnswerViewModel>(); i.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                viewModel = mapper.Map<Answer, AnswerViewModel>(answer);
            }
            return viewModel;
        }
    }
}
