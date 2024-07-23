using Courseproject.Common.Dtos.Address;
using FluentValidation;

namespace Courseproject.Business.Validation;

public class AddressCreateValidator : AbstractValidator<AddressCreate>
{
    public AddressCreateValidator()
    {
        RuleFor(addressCreate => addressCreate.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(AddressCreate => AddressCreate.City).NotEmpty().MaximumLength(100);
        RuleFor(AddressCreate => AddressCreate.Street).NotEmpty().MaximumLength(100);
        RuleFor(AddressCreate => AddressCreate.Zip).NotEmpty().MaximumLength(16);
        RuleFor(AddressCreate => AddressCreate.Phone).MaximumLength(32);
    }

}
