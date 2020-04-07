using System.Linq;

namespace SN.Library.Tools
{
    public class Class
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
