using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowClone.ViewModels
{
    public class AnswerViewModel
    {
        [Required]
        public int AnswerID { get; set; }
        [Required]
        public string AnswerText { get; set; }
        [Required]
        public DateTime AnswerDataAndTime { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int VotesCount { get; set; }
        [Required]
        public int QuestionID { get; set; }

        public virtual UserViewModel User { get; set; }
        public virtual List<VoteViewModel> Votes { get; set; }
        public int CurrentUserVoteType { get; set; }
    }
}
