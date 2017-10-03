using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ArgumentParser.Internal
{
    internal static class AttributeHelper
    {
        internal static IEnumerable<CommandAttribute> GetCommandAttributes(this Type type)
        {
            return type.GetTypeInfo().GetCustomAttributes<CommandAttribute>(true);
        }

        public static IDictionary<MemberInfo, OptionAttribute> GetOptionAttributes(this Type type)
        {
            var membersWithArgumentAttributes =
            (from memberInfo in type.GetMembers().Where(m => m is FieldInfo | m is PropertyInfo)
                from argumentAttribute in memberInfo.GetCustomAttributes<OptionAttribute>(true)
                select new { Key = memberInfo, Value = argumentAttribute }).ToDictionary(x => x.Key, x => x.Value);
            return membersWithArgumentAttributes;
        }

        public static IDictionary<MemberInfo, ValueAttribute> GetValueAttributes(this Type type)
        {
            var membersWithArgumentAttributes =
            (from memberInfo in type.GetMembers().Where(m => m is FieldInfo | m is PropertyInfo)
                from argumentAttribute in memberInfo.GetCustomAttributes<ValueAttribute>(true)
                select new { Key = memberInfo, Value = argumentAttribute }).ToDictionary(x => x.Key, x => x.Value);
            return membersWithArgumentAttributes;
        }
    }
}