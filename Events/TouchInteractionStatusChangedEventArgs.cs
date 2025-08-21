using Shaunebu.Controls.Enums;

namespace Shaunebu.Controls.Events
{
    public class TouchInteractionStatusChangedEventArgs : EventArgs
    {
        public TouchInteractionStatusChangedEventArgs()
        {

        }

        internal TouchInteractionStatusChangedEventArgs(TouchInteractionStatus touchInteractionStatus)
        {
            TouchInteractionStatus = touchInteractionStatus;
        }

        public TouchInteractionStatus TouchInteractionStatus { get; }
    }
}
