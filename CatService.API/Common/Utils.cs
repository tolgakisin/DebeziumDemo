using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CatService.API.Common
{
    public static class Utils
    {
        public static IEnumerable<Type> GetTypesWithCustomAttribute(Assembly assembly, Type attribute)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(attribute).Any())
                {
                    yield return type;
                }
            }
        }
    }
}
