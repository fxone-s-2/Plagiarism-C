using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Plagiarism_C
{
    public class Contactus
    {

        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class ContactusValidator : AbstractValidator<Contactus>
    {
        public ContactusValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Enter Your name:");
            RuleFor(p => p.Name).MaximumLength(50).WithMessage("Name cannout longer than 50 char.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Enter your email address:");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Please enter your a valid email address:");
        }
    }
}