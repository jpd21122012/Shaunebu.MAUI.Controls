namespace Shaunebu.Controls.Abstractions;

public interface IValidation
{
    /// <summary>
    /// Validates the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    bool Validate(object value);

    /// <summary>
    /// Gets the message.
    /// </summary>
    /// <value>
    /// The message.
    /// </value>
    string Message { get; }
}
