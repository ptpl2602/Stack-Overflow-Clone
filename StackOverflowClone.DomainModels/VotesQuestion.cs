using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.DomainModels
{
    public class VotesQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
        public int UserID { get; set; }
        [ForeignKey("QuestionID")]
        public Question Question { get; set; }
        public int QuestionID { get; set; }
        public int VoteValue { get; set; }
    }
}
