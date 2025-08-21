namespace Shaunebu.Controls.Events
{
    public class TouchCompletedEventArgs : EventArgs
    {
        public TouchCompletedEventArgs()
        {

        }

        internal TouchCompletedEventArgs(object? parameter)
        {
            Parameter = parameter;
        }

        public object? Parameter { get; }
    }
}
