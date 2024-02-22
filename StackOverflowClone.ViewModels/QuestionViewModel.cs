using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace StackOverflowClone.ViewModels
{
    public class QuestionViewModel
    {
        [Required]
        public int QuestionID { get; set; }
        [Required]
        public string QuestionName { get; set; }
        [Required]
        public string QuestionContent { get; set; }
        [Required]
        public DateTime QuestionDateAndTime { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int VoteCount { get; set; }
        [Required]
        public int AnswersCount { get; set; }
        [Required]
        public int ViewsCount { get; set; }

        public UserViewModel User { get; set; }
        public CategoryViewModel Category { get; set; }
        public virtual List<AnswerViewModel> Answers { get; set; }
        public virtual List<VotesQuestionViewModel> VotesQuestionsViewModels { get; set; }
        public int CurrentUserVoteType { get; set; }
    }
}
