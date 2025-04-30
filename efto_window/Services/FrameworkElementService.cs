using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace efto_window.Services
{
    public static class FrameworkElementService
    {
        public static T? FindParentByType<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null && parent is not T)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as T;
        }

        public static FrameworkElement? FindChildByTag(DependencyObject parent, object tag)
        {
            int childrens = VisualTreeHelper.GetChildrenCount(parent);

            if (childrens > 0)
            {
                for (int i = 0; i < childrens; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                    if (child is FrameworkElement element && Equals(element.Tag, tag))
                    {
                        return element;
                    }

                    FrameworkElement? result = FindChildByTag(child, tag);
                    if (result != null)
                    {
                        return result;
                    }
                }

                return null;
            }

            return null;
        }
    }
}
