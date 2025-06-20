using efto_window.Views.Components.AnimationObjects;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;

namespace efto_window.Views.Components
{
    [ContentProperty(Name = nameof(AnimationTriggers))]
    public class CTextBlock : Control
    {
        private TextBlock textElement;

        #region [Text] DependencyProperties
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(CTextBlock), new PropertyMetadata(string.Empty));
        #endregion

        #region [Font] DependencyProperties
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(nameof(FontSize), typeof(double), typeof(CTextBlock), new PropertyMetadata(14d));

        public FontWeights FontWeight
        {
            get => (FontWeights)GetValue(FontWeightProperty);
            set => SetValue(FontWeightProperty, value);
        }
        public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register(nameof(FontSize), typeof(FontWeights), typeof(CTextBlock), new PropertyMetadata(FontWeights.Normal));
        
        public Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(CTextBlock), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255,255,255,255))));
        #endregion

        #region [Border] DependencyProperties
        public Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }
        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(CTextBlock), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255,50,50,50))));
        #endregion

        public ObservableCollection<CAnimationTrigger> AnimationTriggers { get; } = new();

        public CTextBlock()
        {
            this.DefaultStyleKey = typeof(CTextBlock);

            this.PointerEntered += (sender, e) =>
            {

            };
        }
    }
}
