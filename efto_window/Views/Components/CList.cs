using efto_window.Views.Components.AnimationObjects;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using WinRT;

namespace efto_window.Views.Components
{
    [ContentProperty(Name = nameof(AnimationsTriggers))]
    public class CList : Control
    {
        #region [Background] DependencyProperties
        public Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }
        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(CList), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))));
        #endregion

        #region [Border] DependencyProperties
        public Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }
        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register(nameof(BorderBrush), typeof(Brush), typeof(CList), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))));
        
        public Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register(nameof(BorderThickness), typeof(Thickness), typeof(CList), new PropertyMetadata(new Thickness(2)));
        #endregion

        #region [Items] DependencyProperties
        public object ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(CList), new PropertyMetadata(null));

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(CList), new PropertyMetadata(null));

        public ObservableCollection<object> SelectedItems
        {
            get => (ObservableCollection<object>)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(nameof(SelectedItems), typeof(ObservableCollection<object>), typeof(CList), new PropertyMetadata(null));
        #endregion

        #region [Position] DependencyProperties
        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }
        public static DependencyProperty OrientationProperty => DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(CList), new PropertyMetadata(Orientation.Vertical));

        public Thickness Margin
        {
            get => (Thickness)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }
        public static DependencyProperty MarginProperty => DependencyProperty.Register(nameof(Margin), typeof(Thickness), typeof(CList), new PropertyMetadata(new Thickness(0)));
        
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        public static DependencyProperty PaddingProperty => DependencyProperty.Register(nameof(Padding), typeof(Thickness), typeof(CList), new PropertyMetadata(new Thickness(0)));
        
        public VerticalAlignment VerticalAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }
        public static DependencyProperty VerticalAlignmentProperty => DependencyProperty.Register(nameof(VerticalAlignment), typeof(VerticalAlignment), typeof(CList), new PropertyMetadata(VerticalAlignment.Center));
        
        public HorizontalAlignment HorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalAlignmentProperty);
            set => SetValue(HorizontalAlignmentProperty, value);
        }
        public static DependencyProperty HorizontalAlignmentProperty => DependencyProperty.Register(nameof(HorizontalAlignment), typeof(HorizontalAlignment), typeof(CList), new PropertyMetadata(HorizontalAlignment.Center));
        #endregion

        #region [Child Element] DependencyProperties
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(CList), new PropertyMetadata(null));

        public ObservableCollection<CAnimationTrigger> AnimationsTriggers { get; } = new();
        #endregion

        public CList()
        {
            this.DefaultStyleKey = typeof(CList);
        }
    }
}
