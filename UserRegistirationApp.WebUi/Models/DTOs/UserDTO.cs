using FluentValidation;

namespace UserRegistirationApp.WebUi.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty()
                                   .WithMessage("Name cannot be empty")
                                   .Length(0, 50)
                                   .WithMessage("Minimum length of Name must be 1 and maximum length must be 50");

            RuleFor(u => u.Surname).NotEmpty()
                                     .WithMessage("Surname cannot be empty")
                                     .Length(0, 50)
                                     .WithMessage("Minimum length of Surname must be 1 and maximum length must be 50");

            RuleFor(u => u.Username).NotEmpty()
                                   .WithMessage("Surname cannot be empty")
                                   .Length(0, 50)
                                   .WithMessage("Minimum length of Username must be 1 and maximum length must be 50");

            RuleFor(u => u.Email).NotEmpty()
                                 .WithMessage("Email cannot be empty")
                                 .Length(0, 50)
                                 .WithMessage("Minimum length of Email must be 1 and maximum length must be 50");
        }
    }
}
