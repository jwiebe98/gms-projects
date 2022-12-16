using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Gmsca.Group.GA.Models.Validation

{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public sealed class EmailAddressAttribute : DataTypeAttribute
    {
        private static bool EnableFullDomainLiterals { get; } =
            AppContext.TryGetSwitch("System.Net.AllowFullDomainLiterals", out bool enable) && enable;

        public EmailAddressAttribute() : base(DataType.EmailAddress) { }

        public override bool IsValid(object? value)
        {
            if (value is null or "")
            {
                return true;
            }

            if (value is not string valueAsString)
            {
                return false;
            }

            if (!EnableFullDomainLiterals && (valueAsString.Contains('\r') || valueAsString.Contains('\n')))
            {
                return false;
            }

            try
            {
                _ = new MailAddress(valueAsString);
            }
            catch
            {
                return false;
            }

            // only return true if there is only 1 '@' character
            // and it is neither the first nor the last character
            int index = valueAsString.IndexOf('@');

            return
                index > 0 &&
                index != valueAsString.Length - 1 &&
                index == valueAsString.LastIndexOf('@');
        }
    }
}

