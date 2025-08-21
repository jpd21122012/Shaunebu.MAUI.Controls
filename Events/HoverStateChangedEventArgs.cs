using Shaunebu.Controls.Enums;


namespace Shaunebu.Controls.Events
{
    public class HoverStateChangedEventArgs : EventArgs
    {
        public HoverStateChangedEventArgs(HoverState state = HoverState.Normal)
        {
            State = state;
        }

        public HoverState State { get; }
    }
}
