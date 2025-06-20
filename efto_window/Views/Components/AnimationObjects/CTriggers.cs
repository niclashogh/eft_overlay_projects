namespace efto_window.Views.Components.AnimationObjects
{
    public class CurserOverTrigger : CAnimationTrigger
    {
        public override CTriggerType Trigger => CTriggerType.CursorOver;
    }

    public class CursorLeaveTrigger : CAnimationTrigger
    {
        public override CTriggerType Trigger => CTriggerType.CursorLeave;
    }

    public class SelectedTrigger : CAnimationTrigger
    {
        public override CTriggerType Trigger => CTriggerType.Selected;
    }

    public class UnSelectedTrigger : CAnimationTrigger
    {
        public override CTriggerType Trigger => CTriggerType.UnSelected;
    }
}
