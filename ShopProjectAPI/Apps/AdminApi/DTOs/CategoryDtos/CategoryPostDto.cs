using FluentValidation;
using Microsoft.AspNetCore.Http;
using ShopProjectAPI.Extensions;

namespace ShopProjectAPI.Apps.AdminApi.DTOs.CategoryDtos
{
    public class CategoryPostDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
    public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(20).WithMessage("Name uzunlugu 20-den boyuk ola bilmez!")
                .NotEmpty().WithMessage("Name mecburidir!");

            RuleFor(x => x.Image).Custom((x, content) =>
            {
                if (!x.IsImage())
                {
                    content.AddFailure("Image", "Choose correct image file");
                }
                if (!x.IsSizeOkay(2))
                {
                    content.AddFailure("Image", "File size must be max 2MB");
                }
            }).NotEmpty().WithMessage("File is null choose image file");

        }
    }
}
