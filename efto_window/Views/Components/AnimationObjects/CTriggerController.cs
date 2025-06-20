using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efto_window.Views.Components.AnimationObjects
{
    public class CTriggerController
    {
        public static readonly DependencyProperty TriggerProperty = DependencyProperty.RegisterAttached("Trigger", typeof(ObservableCollection<CAnimationTrigger>), typeof(CTriggerController), new PropertyMetadata(null, OnTriggerChanged));

        public static void SetTrigger(DependencyObject element, ObservableCollection<CAnimationTrigger> value) => element.SetValue(TriggerProperty, value);
        public static void GetTrigger(DependencyObject element) => element.GetValue(TriggerProperty);

        private static void OnTriggerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj is FrameworkElement element && args.NewValue is ObservableCollection<CAnimationTrigger> triggers)
            {
                element.Loaded += (_, __) =>
                {
                    foreach (CAnimationTrigger trigger in triggers)
                    {
                        switch (trigger.Trigger)
                        {
                            case CTriggerType.CursorOver:
                                element.PointerEntered += (_, __) => ApplyTrigger(element, trigger);
                                break;
                            case CTriggerType.CursorLeave:
                                element.PointerEntered += (_, __) => ApplyTrigger(element, trigger);
                                break; //...
                        }
                    }
                };
            }
        }

        private static void ApplyTrigger(FrameworkElement element, CAnimationTrigger trigger)
        {

        }
    }
}
