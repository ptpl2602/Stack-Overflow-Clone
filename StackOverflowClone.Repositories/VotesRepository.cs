using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowClone.DomainModels;

namespace StackOverflowClone.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int answerId, int userId, int value);
    }
    public class VotesRepository : IVotesRepository
    {
        StackOverflowCloneDbContext _dbContext;
        public VotesRepository()
        {
            _dbContext = new StackOverflowCloneDbContext();
        }
        public void UpdateVote(int answerId, int userId, int value)
        {
            int updateValue;
            if(value > 0)
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

            Vote vote = _dbContext.Votes.FirstOrDefault(i => i.AnswerID == answerId && i.UserID == userId);
            if(vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Vote voteNull = new Vote() { AnswerID = answerId, UserID = userId, VoteValue = updateValue };
                _dbContext.Votes.Add(voteNull);
            }
            _dbContext.SaveChanges();
        }
    }
}
