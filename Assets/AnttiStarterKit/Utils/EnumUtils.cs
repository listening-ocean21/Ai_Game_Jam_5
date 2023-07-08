using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarterKit.Extensions;

namespace AnttiStarterKit.Utils
{
    public static class EnumUtils
    {
        // 返回枚举类型的所有值
        public static IEnumerable<T> ToList<T>()
        {
            var enumType = typeof (T);

            if (enumType.BaseType != typeof(Enum))
            {
                throw new ArgumentException("T must be of type System.Enum");
            }

            var enumValArray = Enum.GetValues(enumType);

            var enumValList = new List<T>(enumValArray.Length);
            enumValList.AddRange(from int val in enumValArray select (T)Enum.Parse(enumType, val.ToString()));

            return enumValList;
        }
        
        // 返回枚举类型的随机值
        public static T Random<T>()
        {
            return ToList<T>().ToList().Random();
        }
    }
}