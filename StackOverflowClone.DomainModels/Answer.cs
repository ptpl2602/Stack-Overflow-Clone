using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace StackOverflowClone.DomainModels
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public DateTime AnswerDateAndTime { get; set; }
        public int VoteCount { get; set; }
        public bool IsAccepted { get; set; }
        public int UserID { get; set; }
        [ForeignKey("QuestionID")]
        public Question Question { get; set; }
        public int QuestionID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public virtual List<Vote> Votes { get; set; }
    }
}
