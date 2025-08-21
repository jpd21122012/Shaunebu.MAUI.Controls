using Shaunebu.Controls.Enums;

namespace Shaunebu.Controls.Events
{
    public class TouchStateChangedEventArgs : EventArgs
    {
        public TouchStateChangedEventArgs()
        {

        }
        internal TouchStateChangedEventArgs(TouchState state)
        {
            State = state;
        }

        public TouchState State { get; }
    }
}
