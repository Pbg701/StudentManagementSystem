using FluentValidation;
using StudentManagementSystem.Application.DTOs;

namespace StudentManagementSystem.Application.Validators
{
    public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
                .Matches(@"^[a-zA-Z\s\-']+$").WithMessage("Name contains invalid characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be valid")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters");

            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("Age is required")
                .InclusiveBetween(1, 120).WithMessage("Age must be between 1 and 120");

            RuleFor(x => x.Course)
                .NotEmpty().WithMessage("Course is required")
                .MaximumLength(50).WithMessage("Course must not exceed 50 characters")
                .Matches(@"^[a-zA-Z0-9\s,\-]+$").WithMessage("Course contains invalid characters");
        }
    }
}