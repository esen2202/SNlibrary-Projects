 using System.Linq;

namespace SN.Class.Helpers
{
    public class ClassHelper : IHelper
    {
        public static void CopyObjectPropertiesValue<TObject>(TObject source, TObject target)
        {
            typeof(TObject).GetProperties().ToList().ForEach(property =>
            {
                property.SetValue(target, property.GetValue(source));
            });
        }
    }
}
