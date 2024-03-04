using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowClone.DomainModels;

namespace StackOverflowClone.Repositories
{
    public interface IQuestionsRepository
    {
        void InsertQuestion(Question question);
        void UpdateQuestionDetails (Question question);
        void UpdateQuestionVoteCount (int questionId, int userId, int value);
        void UpdateQuestionAnswersCount(int id, int value);
        void UpdateQuestionViewsCount (int id, int value);
        void DeleteQuestion (int questionId);
        /*void SetAcceptedAnswer(int questionId, int answerId);*/
        List<Question> GetQuestions ();
        List<Question> GetQuestionsById (int questionId);
    }
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly StackOverflowCloneDbContext _dbContext;
        private readonly IVotesQuestionsRepository iVotesQuestionsRepo;
        private readonly IQuestionsRepository iQuestionsRepo;
        public QuestionsRepository()
        {
            _dbContext = new StackOverflowCloneDbContext();
            iVotesQuestionsRepo = new VotesQuestionsRepository();
        }
        public void DeleteQuestion(int questionId)
        {
            Question deleteQuestion = _dbContext.Questions.FirstOrDefault(i => i.QuestionID == questionId);
            if(deleteQuestion != null)
            {
                _dbContext.Questions.Remove(deleteQuestion);
                _dbContext.SaveChanges();
            }
        }

        public List<Question> GetQuestions()
        {
            List<Question> questions = _dbContext.Questions
                                        .Include(i => i.Category)
                                        .Include(i => i.User)
                                        .Include(i => i.Answers)
                                        .OrderByDescending(i => i.QuestionDateAndTime)
                                        .ToList();
            return questions;
        }

        public List<Question> GetQuestionsById(int questionId)
        {
            List<Question> questions = _dbContext.Questions.Include(i => i.Answers).Where(i => i.QuestionID == questionId)
                                                        .Include(i => i.Category)
                                                        .Include(i => i.User)
                                                        .ToList();
            return questions;
        }

        public void InsertQuestion(Question question)
        {
            _dbContext.Questions.Add(question);
            _dbContext.SaveChanges();
        }

        public void UpdateQuestionAnswersCount(int id, int value)
        {
            Question updateQuestion = _dbContext.Questions.Where(i => i.QuestionID == id).FirstOrDefault();
            if (updateQuestion != null)
            {
                updateQuestion.AnswerCount += value;
                _dbContext.SaveChanges();
            }
        }

        public void UpdateQuestionDetails(Question question)
        {
            Question updateQuestion = _dbContext.Questions.FirstOrDefault(i => i.QuestionID == question.QuestionID);
            if(updateQuestion != null)
            {
                updateQuestion.QuestionName = question.QuestionName;
                updateQuestion.QuestionContent = question.QuestionContent;
                updateQuestion.QuestionDateAndTime = question.QuestionDateAndTime;
                updateQuestion.CategoryID = question.CategoryID;
                _dbContext.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int id, int value)
        {
            Question updateQuestion = _dbContext.Questions.Where(i => i.QuestionID == id).FirstOrDefault();
            if (updateQuestion != null)
            {
                updateQuestion.ViewsCount += value;
                _dbContext.SaveChanges();
            }
        }

        public void UpdateQuestionVoteCount(int questionId, int userId, int value)
        {
            Question updateQuestion = _dbContext.Questions.Where(i => i.QuestionID == questionId).FirstOrDefault();

            var existingVote = _dbContext.VotesQuestions.FirstOrDefault(i => i.QuestionID == questionId && i.UserID == userId);

            if (existingVote != null)
            {
                if (existingVote.VoteValue == value)
                {
                    updateQuestion.VoteCount -= value;
                }
                else
                {
                    updateQuestion.VoteCount += value - existingVote.VoteValue;
                }
                existingVote.VoteValue = value;
            }
            else
            {
                // User hasn't voted before
                updateQuestion.VoteCount += value;
                iVotesQuestionsRepo.UpdateVote(questionId, userId, value);
            }
            _dbContext.SaveChanges();
        }

        /*public void SetAcceptedAnswer (int questionId, int answerId)
        {
            Question question = _dbContext.Questions.FirstOrDefault(i => i.QuestionID == questionId);
            if(question != null)
            {
                question.AnswerID = answerId;
                _dbContext.SaveChanges();
            }
        }*/
    }
}
