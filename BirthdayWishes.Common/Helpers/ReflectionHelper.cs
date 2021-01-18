using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BirthdayWishes.Common.Helpers
{
    public static class ReflectionHelper
    {
        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(Type type)
            where TAttribute : Attribute
        {
            return type.GetCustomAttributes<TAttribute>();
        }
        public static IEnumerable<Type> GetTypes<T>()
        {
            var typeToFind = typeof(T);

            var assembliesToScan = AppDomain.CurrentDomain.GetAssemblies();

            return assembliesToScan
                .SelectMany(s => s.GetTypes())
                .Where(p => typeToFind.IsAssignableFrom(p) && !p.IsInterface && p.ContainsGenericParameters == false);
        }
    }
}
