using System.Reflection;

namespace Gmsca.Group.GA.Models.Validation.Helpers
{
    public static class PropertyHelper
    {
        public static object? GetPropertyValue(object? src, string propName)
        {
            return src is null
                ? null
                : propName.Contains(".")
                ? GetValueBeforeDot(src, propName)
                : propName.Contains("[") ? GetArrayValue(src, propName) : GetValue(src, propName);
        }

        private static object? GetValue(object src, string propName)
        {
            PropertyInfo? prop = src.GetType().GetProperty(propName);
            return prop is not null ? prop.GetValue(src, null) : null;
        }

        private static object? GetArrayValue(object src, string propName)
        {
            int startindex = propName.IndexOf("[");
            int endindex = propName.IndexOf("]");
            int index = int.Parse(propName.Substring(startindex + 1, endindex - startindex - 1));
            string propNameWithoutIndex = propName[..startindex];
            object? propValue = GetValue(src, propNameWithoutIndex);
            return propValue is null ? null : ((IEnumerable<object>)propValue).ToArray()[index];
        }

        private static object? GetValueBeforeDot(object src, string propName)
        {
            string[] temp = propName.Split(new char[] { '.' }, 2);
            return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
        }
    }
}