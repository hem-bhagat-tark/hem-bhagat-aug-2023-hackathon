using System.ComponentModel.DataAnnotations;

namespace TaskExecutor.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var displayAttributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), inherit: false);
            if (displayAttributes != null && displayAttributes.Length > 0)
                return displayAttributes[0].Name;

            return enumValue.ToString();
        }
    }
}
