using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
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

    public static IEnumerable<Type> GetTypeImplementedT(Type implementedType)
    {
        var objects = new List<Type>();
        foreach (var type in
            Assembly.GetAssembly(implementedType).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(implementedType)))
        {
            objects.Add(type);
        }
        return objects.AsEnumerable();
    }
}
