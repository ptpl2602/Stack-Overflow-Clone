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
        void UpdateQuestionVoteCount (int id, int value);
        void UpdateQuestionAnswersCount(int id, int value);
        void UpdateQuestionViewsCount (int id, int value);
        void DeleteQuestion (int questionId);
        List<Question> GetQuestions ();
        List<Question> GetQuestionsById (int questionId);
    }
    public class QuestionsRepository : IQuestionsRepository
    {
        StackOverflowCloneDbContext _dbContext;
        public QuestionsRepository()
        {
            _dbContext = new StackOverflowCloneDbContext();
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
                .OrderByDescending(i => i.QuestionDateAndTime)
                .ToList();
            return questions;
        }

        public List<Question> GetQuestionsById(int questionId)
        {
            List<Question> questions = _dbContext.Questions.Where(i => i.QuestionID == questionId).ToList();
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
                updateQuestion.AnswerCount += value;
                _dbContext.SaveChanges();
            }
        }

        public void UpdateQuestionVoteCount(int id, int value)
        {
            Question updateQuestion = _dbContext.Questions.Where(i => i.QuestionID == id).FirstOrDefault();
            if(updateQuestion != null)
            {
                updateQuestion.VoteCount += value;
                _dbContext.SaveChanges();
            }
        }
    }
}
