using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowClone.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@'(\w+)')]
        public string Email { get; set; }

    }
}
