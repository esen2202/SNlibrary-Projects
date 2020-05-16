using FluentValidation;
using SN.NetSet.Entities.Concrete.Network;
using SN.Network.Helpers;

namespace SN.NetSet.Business.Data.Validation
{
    public class NetConfigBaseValidator : AbstractValidator<NetConfigBase>
    {
        public NetConfigBaseValidator()
        {
            RuleFor(x => x.ConfigName)
                .NotEmpty().WithMessage("Don't leave blank the Config Name")
                .Length(1, 30).WithMessage("Config Name min.1 and max.30 character");

            RuleFor(x => x.IpAddress)
                .NotEmpty().WithMessage("Don't leave blank the Ip Address")
                .IpIsValid();

            RuleFor(x => x.SubnetMask)
                .NotEmpty().WithMessage("Don't leave blank the SubnetMask")
                .IpIsValid();

            RuleFor(x => x.Gateway)
                .IpIsValid()
                .When(x => !string.IsNullOrEmpty(x.Gateway));

            RuleFor(x => x.DnsServer1)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.DnsServer2)).WithMessage("Dns Server 1 must be write")
                .IpIsValid()
                .When(x => !string.IsNullOrEmpty(x.DnsServer1), ApplyConditionTo.CurrentValidator);

            RuleFor(x => x.DnsServer2)
                .IpIsValid()
                .When(x => !string.IsNullOrEmpty(x.DnsServer2));
        }
    }

    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string> IpIsValid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(m => m != null && NetworkHelper.ValidateIpV4(m)).WithMessage("'{PropertyName}' is Not Valid an Address");
        }
    }
}
