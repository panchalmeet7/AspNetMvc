using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[EmailAddress(ErrorMessage = "Password is Required")]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        //[DataType(DataType.Password)]
        public string PasswordHash { get; set; } 

        //[Required(ErrorMessage = "Confirm password is required")]
        //[DataType(DataType.Password)]
        //[Compare("PasswordHash", ErrorMessage ="Password and Confirm Password should Match")]
        public string ConfirmPasswordHash { get; set; }
    }
}
