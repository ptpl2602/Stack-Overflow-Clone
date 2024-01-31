using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowClone.ViewModels
{
    public class EditUserDetailsViewModel
    {
        [Required]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]    //Bắt đầu là 1 chuỗi ký tự 'word', theo sau là @, sau đó là 1 phần domain hợp lệ với 1 dấu . giữa tên miền và domain --> Email hợp lệ
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
