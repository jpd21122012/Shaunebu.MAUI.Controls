namespace Shaunebu.Controls.Events
{
    public class RatingChangedEventArgs(double rating) : EventArgs
    {
        /// <summary>Gets the rating for the rating changed event.</summary>
        public double Rating { get; } = rating;
    }
}
