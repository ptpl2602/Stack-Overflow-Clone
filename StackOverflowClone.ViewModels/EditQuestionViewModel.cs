﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace StackOverflowClone.ViewModels
{
    public class EditQuestionViewModel
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
        public int CategoryID { get; set; }
        public DateTime GetDateTime()
        {
            string dateTimeString = $"{QuestionDateAndTime}";
            return DateTime.ParseExact(dateTimeString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            //return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}
