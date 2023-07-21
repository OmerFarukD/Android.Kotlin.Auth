using Android.Kotlin.Auth.Authorization.Models;
using FluentValidation;

namespace Android.Kotlin.Auth.Authorization.Validation
{
    public class SignUpViewModelValidator : AbstractValidator<SignUpViewModel>
    {
        public SignUpViewModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı gereklidir.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş olamaz.").EmailAddress().WithMessage("Email Doğru formatta değil.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı gereklidir.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir alanı gereklidir.");
        }
    }
}