using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Core;

namespace efto_window.Views.Components.AnimationObjects
{
    public abstract class CAnimationTrigger
    {
        public Brush? SetBackground { get; set; }
        public Brush? SetForeground { get; set; }
        public Brush? SetBorderBrush { get; set; }
        public Thickness SetBorderThickness { get; set; }

        public CoreCursorType? SetCursor { get; set; }

        public abstract CTriggerType Trigger { get; }
    }
}
