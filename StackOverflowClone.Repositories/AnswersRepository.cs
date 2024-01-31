using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowClone.DomainModels;

namespace StackOverflowClone.Repositories
{
    public interface IAnswerRepository
    {
        void InsertAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void DeleteAnswer(int answerId);
        void UpdateAnswertVotesCount(int answerId, int userId, int value);
        List<Answer> GetAnswersByQuestionId(int questionId);
        List<Answer> GetAnswersByAnswerId (int answerId);
    }
    public class AnswersRepository : IAnswerRepository
    {
        private readonly StackOverflowCloneDbContext _dbContext;
        private readonly IQuestionsRepository iQuestionRepo;
        private readonly IVotesRepository iVoteRepo;
        public AnswersRepository()
        {
            _dbContext = new StackOverflowCloneDbContext();
            iQuestionRepo = new QuestionsRepository();
            iVoteRepo = new VotesRepository();
            
        }
        public void DeleteAnswer(int answerId)
        {
            Answer deleteAnswer = _dbContext.Answers.FirstOrDefault(i => i.AnswerID == answerId);
            if(deleteAnswer != null)
            {
                _dbContext.Answers.Remove(deleteAnswer);
                _dbContext.SaveChanges();
            }
        }

        public List<Answer> GetAnswersByAnswerId(int answerId)
        {
            List<Answer> answers = _dbContext.Answers.Where(i => i.AnswerID == answerId).ToList();
            return answers;
        }

        public List<Answer> GetAnswersByQuestionId(int questionId)
        {
            List<Answer> answers = _dbContext.Answers.Where(i => i.QuestionID == questionId).ToList();
            return answers;
        }

        public void InsertAnswer(Answer answer)
        {
            _dbContext.Answers.Add(answer);
            _dbContext.SaveChanges();

            iQuestionRepo.UpdateQuestionAnswersCount(answer.AnswerID, 1);
        }

        public void UpdateAnswertVotesCount(int answerId, int userId, int value)
        {
            Answer updateAnswer = _dbContext.Answers.Where(i => i.AnswerID == answerId).FirstOrDefault();
            if(updateAnswer != null)
            {
                updateAnswer.VoteCount += value;
                _dbContext.SaveChanges();

                iQuestionRepo.UpdateQuestionVoteCount(answerId, value);
                iVoteRepo.UpdateVote(answerId, userId, value);
            }
        }

        public void UpdateAnswer(Answer answer)
        {
            Answer updateAnswer = _dbContext.Answers.Where(i => i.AnswerID == answer.AnswerID).FirstOrDefault();
            if(updateAnswer != null)
            {
                updateAnswer.AnswerText = answer.AnswerText;
                _dbContext.SaveChanges();

                iQuestionRepo.UpdateQuestionAnswersCount(answer.AnswerID, -1);
            }
        }
    }
}
