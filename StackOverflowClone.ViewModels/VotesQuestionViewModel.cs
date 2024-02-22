using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.ViewModels
{
    public class VotesQuestionViewModel
    {
        public int VoteID { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public int VoteValue { get; set; }
    }
}
