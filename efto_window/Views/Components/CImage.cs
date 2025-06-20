using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efto_window.Views.Components
{
    public class CImage : Control
    {
        private Image image;

        #region [Source] DependencyProperties
        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(CImage), new PropertyMetadata(null));
        #endregion

        #region [Size] DependencyProperties
        public double Height
        {
            get => (double)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }
        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register(nameof(Height), typeof(double), typeof(CImage), new PropertyMetadata(50d));

        public double Width
        {
            get => (double)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }
        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(nameof(Width), typeof(double), typeof(CImage), new PropertyMetadata(50d));
        #endregion

        #region [Position] DependencyProperties
        public HorizontalAlignment HorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalAlignmentProperty);
            set => SetValue(HorizontalAlignmentProperty, value);
        }
        public static readonly DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register(nameof(HorizontalAlignment), typeof(HorizontalAlignment), typeof(CImage), new PropertyMetadata(HorizontalAlignment.Center));

        public VerticalAlignment VerticalAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }
        public static readonly DependencyProperty VerticalAlignmentProperty = DependencyProperty.Register(nameof(VerticalAlignment), typeof(VerticalAlignment), typeof(CImage), new PropertyMetadata(VerticalAlignment.Center));

        public Thickness Margin
        {
            get => (Thickness)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }
        public static readonly DependencyProperty MarginProperty = DependencyProperty.Register(nameof(Margin), typeof(Thickness), typeof(CImage), new PropertyMetadata(null));

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(nameof(Padding), typeof(Thickness), typeof(CImage), new PropertyMetadata(null));
        #endregion

        public CImage()
        {
            this.DefaultStyleKey = typeof(CImage);


        }
    }
}
