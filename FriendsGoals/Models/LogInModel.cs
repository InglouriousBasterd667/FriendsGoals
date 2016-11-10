using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FriendsGoals.Models
{    
        public class LogInModel
        {
            [Required(ErrorMessage = " ")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required(ErrorMessage = " ")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [HiddenInput(DisplayValue = false)]
            public string ReturnUrl { get; set; }
        }
}