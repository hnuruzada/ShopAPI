using FluentValidation;

namespace ShopProjectAPI.Apps.UserApi.AccountDtos
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName).MinimumLength(4).MaximumLength(20).NotEmpty();

            RuleFor(x => x.FullName).NotEmpty().MaximumLength(25).MinimumLength(4);

            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(20).NotEmpty();
            RuleFor(x => x.ConfirmPassword).MinimumLength(8).MaximumLength(20).NotEmpty();


            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                    context.AddFailure("ConfirmPassword", "Password ConfirmPAssword-e beraber olmalidir!");
            });
        }
    }
}
