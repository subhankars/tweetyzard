using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using TweetinviCore.Enum;

namespace TweetinviCore.Extensions
{
    public static class LanguageExtension
    {
        public static string GetDescriptionAttribute(this Language language)
        {
            var field = language.GetType().GetField(language.ToString());
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return descriptionAttribute != null ? descriptionAttribute.Description : language.ToString();
        }

        public static Language GetLangFromDescription(string descriptionValue)
        {
            try
            {
                if (!String.IsNullOrEmpty(descriptionValue))
                {
                    descriptionValue = descriptionValue.Substring(0, 2);
                }

                var language = typeof(Language).GetFields().First(field => IsValidDescriptionField(descriptionValue, field));
                return (Language)language.GetValue(null);
            }
            catch (Exception)
            {
                return Language.Undefined;
            }
            
        }

        private static bool IsValidDescriptionField(string descriptionValue, FieldInfo field)
        {
            var descriptionAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            if (descriptionAttribute == null)
            {
                return false;
            }

            return ((DescriptionAttribute) descriptionAttribute).Description == descriptionValue;
        }
    }
}