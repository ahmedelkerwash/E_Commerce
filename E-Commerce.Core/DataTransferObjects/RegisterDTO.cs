using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DataTransferObjects
{
	public class RegisterDTO
	{
        public string DisplayName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$" , ErrorMessage ="Invalid Password")]
        public string Password { get; set; }
    }
}
