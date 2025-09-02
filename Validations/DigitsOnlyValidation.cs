using Shaunebu.Controls.Abstractions;

namespace Shaunebu.Controls.Validations;

public class DigitsOnlyValidation : BindableObject, IValidation
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
        typeof(DigitsOnlyValidation),
        "The field should contain only digits.");

    /// <summary>
    /// Validates the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool Validate(object value)
    {
        if (value is string text)
        {
            return text.All(char.IsDigit);
        }

        return false;
    }
}