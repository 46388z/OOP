using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace GalaxyProject.Utils
{
    public static class Extensions
    {
        // Взима нормално за четене от потребителя име от енумерацията
        public static string GetFriendlyName(this Enum aEnum)
        {
            Type enumType = aEnum.GetType();
            MemberInfo[] memberInfo = enumType.GetMember(aEnum.ToString());
            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Any())
                {
                    return ((DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }

            return aEnum.ToString();
        }
    }
}
