using StudentManagementSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace StudentManagementSystem.Application.Validators
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email must not exceed 255 characters");

            RuleFor(x => x.Age)
                .InclusiveBetween(1, 120).WithMessage("Age must be between 1 and 120");

            RuleFor(x => x.Course)
                .NotEmpty().WithMessage("Course is required")
                .MaximumLength(50).WithMessage("Course must not exceed 50 characters");
        }
    }

    public class UpdateStudentValidator : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email must not exceed 255 characters");

            RuleFor(x => x.Age)
                .InclusiveBetween(1, 120).WithMessage("Age must be between 1 and 120");

            RuleFor(x => x.Course)
                .NotEmpty().WithMessage("Course is required")
                .MaximumLength(50).WithMessage("Course must not exceed 50 characters");
        }
    }
}
