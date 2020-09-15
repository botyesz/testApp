using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace LynxTestApp.Models
{
    public class User : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(15, MinimumLength = 4)]
        [Index("Ix_Username", Order = 1, IsUnique = true)]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password is required."), DataType(DataType.Password),]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required."), DataType(DataType.Password), Compare("Password", ErrorMessage = "Password and Password confirmation must match."), Display(Name = "Password confirmation")]
        public string ConfirmPassword { get; set; }

        [Required,StringLength(20),Display(Name = "First name")]
        public string First_name { get; set; }

        [Required,StringLength(20), Display(Name = "Last name")]
        public string Last_name { get; set; }

        [Required,EmailAddress, StringLength(50)]
        [Index("Ix_Email", Order = 1, IsUnique = true)]
        public string Email { get; set; }

        [Required, RegularExpression(@"^(\d{9})$", ErrorMessage = "Not a valid phone number"), Display(Name = "Phone number")]
        public int Phone_number { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            DataContext db = new DataContext();
            List<ValidationResult> validationResult = new List<ValidationResult>();
            var validateName = db.Users.FirstOrDefault(x => x.Username == Username && x.Id != Id);
            var validateEmail = db.Users.FirstOrDefault(x => x.Email == Email && x.Id != Id);
            if (validateName != null)
            {
                ValidationResult errorMessage = new ValidationResult
                ("The Username Allready Exists.", new[] { "Username" });
                yield return errorMessage;
            }else if(validateEmail != null)
            {
                ValidationResult errorMessage = new ValidationResult
                ("The Email Is Allready In Use.", new[] { "Email" });
                yield return errorMessage;

            }
            else
            {
                yield return ValidationResult.Success;
            }
        }
    }
}