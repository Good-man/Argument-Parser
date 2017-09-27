using System;
using System.ComponentModel;

namespace ArgumentParser.Internal
{
    internal static class StringConversionExtensions
    {
        public static bool Is(this string input, Type targetType)
        {
            try
            {
                TypeDescriptor.GetConverter(targetType).ConvertFromString(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Is<T>(this string input)
        {
            try
            {
                TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static T ConvertTo<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(input);
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
        }

        public static object ConvertTo(this string input, Type targetType)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(targetType);
                return converter.ConvertFromString(input);
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }
    }
}
