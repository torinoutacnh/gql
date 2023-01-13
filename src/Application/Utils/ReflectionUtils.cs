using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gql.Application.Utils;

public static class ReflectionUtils
{
    public static IEnumerable<Type> GetTypeImplementedT<T>()
    {
        var objects = new List<Type>();
        foreach (var type in
            Assembly.GetAssembly(typeof(T)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
        {
            objects.Add(type);
        }
        return objects.AsEnumerable();
    }
}
