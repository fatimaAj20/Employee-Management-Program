using Courseproject.Common.Dtos.Team;
using FluentValidation;
using System.Drawing;

namespace Courseproject.Business.Validation;

public class TeamCreateValidator :AbstractValidator<TeamCreate>
{
    public TeamCreateValidator()
    {
        RuleFor(teamCreate =>teamCreate.Name).NotEmpty().MaximumLength(50);
    }
}
