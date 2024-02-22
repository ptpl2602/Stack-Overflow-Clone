using StackOverflowClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.Repositories
{
    public interface IVotesQuestionsRepository
    {
        void UpdateVote(int answerId, int userId, int value);
    }
    public class VotesQuestionsRepository : IVotesQuestionsRepository
    {
        StackOverflowCloneDbContext _dbContext;
        public VotesQuestionsRepository()
        {
            _dbContext = new StackOverflowCloneDbContext();
        }
        public void UpdateVote(int questionId, int userId, int value)
        {
            int updateValue;
            if (value > 0)
            {
                updateValue = 1;
            }
            else if (value < 0)
            {
                updateValue = -1;
            }
            else
            {
                updateValue = 0;
            }

            VotesQuestion vote = _dbContext.VotesQuestions.FirstOrDefault(i => i.QuestionID == questionId && i.UserID == userId);
            if (vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                VotesQuestion voteNull = new VotesQuestion() { QuestionID = questionId, UserID = userId, VoteValue = updateValue };
                _dbContext.VotesQuestions.Add(voteNull);
            }
            _dbContext.SaveChanges();
        }
    }
}
