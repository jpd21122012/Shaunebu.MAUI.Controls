using Shaunebu.Controls.Enums;

namespace Shaunebu.Controls.Events
{
    public class HoverStatusChangedEventArgs : EventArgs
    {
        public HoverStatusChangedEventArgs()
        {

        }
        internal HoverStatusChangedEventArgs(HoverStatus status)
        {
            State = status;
        }
        public HoverStatus State { get; }
    }
}
