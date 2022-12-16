using Gmsca.Group.GA.Models.Validation.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Gmsca.Group.GA.Models.Validation
{
    [AttributeUsage(AttributeTargets.Class,
    AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' and '{1}' do not match.";
        private readonly object _typeId = new();

        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty) : base(_defaultErrorMessage)
        {
            OriginalProperty = originalProperty;
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty { get; private set; }
        public string OriginalProperty { get; private set; }
        public override object TypeId => _typeId;

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentUICulture, ErrorMessageString, OriginalProperty, ConfirmProperty);
        }

        public override bool IsValid(object? value)
        {
            object? original = PropertyHelper.GetPropertyValue(value, OriginalProperty);
            object? confirm = PropertyHelper.GetPropertyValue(value, ConfirmProperty);
            return original is "" || confirm is "" || Equals(original, confirm);
        }
    }
}
