namespace Shaunebu.Controls.Events
{
    public class LongPressCompletedEventArgs : EventArgs
    {
        public LongPressCompletedEventArgs()
        {

        }

        internal LongPressCompletedEventArgs(object? parameter)
        {
            Parameter = parameter;
        }

        public object? Parameter { get; }
    }
}
