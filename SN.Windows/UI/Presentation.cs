using System.Windows;
using System.Windows.Media;

namespace SN.Windows.UI
{
    public class Presentation
    {
        private static ChildItem FindVisualChild<ChildItem>(DependencyObject obj) where ChildItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is ChildItem)
                    return (ChildItem)child;
                else
                {
                    ChildItem childOfChild = FindVisualChild<ChildItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
