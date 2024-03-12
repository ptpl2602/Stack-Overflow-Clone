using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
        int GetVoteCountByAnswerId(int answerId);
        void AcceptAnswer(int answerId);
        int GetQuestionIdForAnswer (int answerId);
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
            List<Answer> answers = _dbContext.Answers.Where(i => i.AnswerID == answerId)
                                                            .Include(i => i.User)
                                                            .Include(i => i.Question)
                                                            .ToList();
            return answers;
        }

        public List<Answer> GetAnswersByQuestionId(int questionId)
        {
            List<Answer> answers = _dbContext.Answers.Where(i => i.QuestionID == questionId)
                                                    .Include(i => i.Question)
                                                    .Include(i => i.User)
                                                    .OrderByDescending(i => i.AnswerDateAndTime)
                                                    .ToList();
            return answers;
        }

        public int GetVoteCountByAnswerId(int answerId)
        {
            var answer = _dbContext.Answers.FirstOrDefault(a => a.AnswerID == answerId);
            return answer?.VoteCount ?? 0;
        }

        public void InsertAnswer(Answer answer)
        {
            _dbContext.Answers.Add(answer);
            _dbContext.SaveChanges();

            iQuestionRepo.UpdateQuestionAnswersCount(answer.QuestionID, 1);
        }

        public void UpdateAnswertVotesCount(int answerId, int userId, int value)
        {
            Answer updateAnswer = _dbContext.Answers.Where(i => i.AnswerID == answerId).FirstOrDefault();

            var existingVote = _dbContext.Votes.FirstOrDefault(i => i.AnswerID == answerId && i.UserID == userId);

            if(existingVote != null)
            {
                if(existingVote.VoteValue == value)
                {
                    updateAnswer.VoteCount -= value;
                }
                else
                {
                    updateAnswer.VoteCount += value - existingVote.VoteValue;
                }
                existingVote.VoteValue = value;
            }
            else
            {
                // User hasn't voted before
                updateAnswer.VoteCount += value;
                iVoteRepo.UpdateVote(answerId, userId, value);
            }
            _dbContext.SaveChanges();
        }

        public void UpdateAnswer(Answer answer)
        {
            Answer updateAnswer = _dbContext.Answers.Where(i => i.AnswerID == answer.AnswerID).FirstOrDefault();
            if(updateAnswer != null)
            {
                updateAnswer.AnswerText = answer.AnswerText;
                updateAnswer.AnswerDateAndTime = DateTime.Now;
                _dbContext.SaveChanges();

                iQuestionRepo.UpdateQuestionAnswersCount(answer.AnswerID, -1);
            }
        }

        public int GetQuestionIdForAnswer (int answerId)
        {
            var questionId = _dbContext.Answers.Where(i => i.AnswerID == answerId)
                                                .Select(i => i.QuestionID)
                                                .FirstOrDefault();
            return questionId;
        }

        public void AcceptAnswer(int answerId)
        {
            var answer = _dbContext.Answers.FirstOrDefault(i => i.AnswerID == answerId);
            if(answer != null)
            {
                answer.IsAccepted = true;
                _dbContext.SaveChanges();   
            }        
        }
    }
}
