using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArgumentParser.Internal
{
    internal static class ReflectionHelpers
    {
        internal static Type GetMemberType(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
                throw new ArgumentNullException(nameof(memberInfo));
            if (memberInfo is PropertyInfo)
            {
                var propertyInfo = (PropertyInfo)memberInfo;
                return propertyInfo.PropertyType;
            }
            if (memberInfo is FieldInfo)
            {
                var fieldInfo = (FieldInfo)memberInfo;
                return fieldInfo.FieldType;
            }
            throw new ArgumentOutOfRangeException(nameof(memberInfo), memberInfo, "Only property and field members are supported.");
        }

        internal static void SetValue(this MemberInfo memberInfo, object obj, object value)
        {
            if (memberInfo is PropertyInfo)
            {
                var propertyInfo = (PropertyInfo)memberInfo;
                propertyInfo.SetValue(obj, value);
                return;
            }
            if (memberInfo is FieldInfo)
            {
                var fieldInfo = (FieldInfo)memberInfo;
                fieldInfo.SetValue(obj, value);
                return;
            }
            throw new ArgumentOutOfRangeException(nameof(memberInfo), memberInfo, "Only property and field members are supported.");
        }
    }
}
