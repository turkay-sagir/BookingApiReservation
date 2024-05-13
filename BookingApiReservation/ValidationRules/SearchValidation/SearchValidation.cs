using BookingApiReservation.Models;
using FluentValidation;

namespace BookingApiReservation.ValidationRules.SearchValidation
{
    public class SearchValidation:AbstractValidator<SearchViewModel>
    {
        public SearchValidation()
        {
            RuleFor(x => x.city_name).NotEmpty().WithMessage("Lütfen bir şehir adı giriniz.");
            RuleFor(x => x.city_name).MinimumLength(3).WithMessage("En az 3 karakter giriniz.");
            RuleFor(x => x.guest).GreaterThan(29).WithMessage("En fazla 29 değerini girebilirsiniz.");
            RuleFor(x => x.guest).LessThan(1).WithMessage("En az 1 tane misafir girişi yapmalısınız.");
            RuleFor(x => x.children).GreaterThan(29).WithMessage("En fazla 29 değerini girebilirsiniz.");
            RuleFor(x => x.children).LessThan(1).WithMessage("En az 1 tane çocuk girişi yapmalısınız.");
        }
    }
}
