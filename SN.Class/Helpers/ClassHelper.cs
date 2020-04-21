using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SN.Class.Helpers
{
    public class ClassHelper
    {
        public static void CopyObjectPropertiesValue<TObject>(TObject source, TObject target)
        {
            typeof(TObject).GetProperties().ToList().ForEach(property =>
            {
                property.SetValue(target, property.GetValue(source));
            });
        }

        public static List<TClass> GetClassListFromAssembly<TClass>(string assemblyName)
        {
            //Assembly.GetExecutingAssembly().GetTypes()
            var result = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == assemblyName)
                .GetTypes()
                .Where(p => typeof(TClass).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract)
                .Select(p => (TClass)Activator.CreateInstance(p)).ToList();
            return result;
        }
    }
}
