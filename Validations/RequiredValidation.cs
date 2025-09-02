using Shaunebu.Controls.Abstractions;

namespace Shaunebu.Controls.Validations;

public class RequiredValidation : BindableObject, IValidation
{
    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>
    /// The message.
    /// </value>
    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    /// <summary>
    /// The message property
    /// </summary>
    public static readonly BindableProperty MessageProperty = BindableProperty.Create(
        nameof(Message),
        typeof(string),
        typeof(RequiredValidation),
        "This field is required");

    /// <summary>
    /// Validates the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool Validate(object value)
    {
        if (value is string text)
        {
            return !string.IsNullOrEmpty(text);
        }

        return value != null;
    }
}