namespace Shaunebu.Controls.Events;

public class PINCompletedEventArgs : EventArgs
{
    /// <summary>
    /// Gets or sets the pin.
    /// </summary>
    /// <value>
    /// The pin.
    /// </value>
    public string PIN { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PINCompletedEventArgs"/> class.
    /// </summary>
    /// <param name="pin">The pin.</param>
    public PINCompletedEventArgs(string pin)
    {
        this.PIN = pin;
    }
}
