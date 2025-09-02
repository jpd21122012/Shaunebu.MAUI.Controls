using Shaunebu.Controls.Abstractions;

namespace Shaunebu.Controls.Validations;

public class LettersOnlyValidation : BindableObject, IValidation
{
    /// <summary>
    /// The message property
    /// </summary>
    public static readonly BindableProperty MessageProperty = BindableProperty.Create(
        nameof(Message),
        typeof(string),
        typeof(LettersOnlyValidation),
        "The field should contain only letters.");

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
    /// The allow spaces property
    /// </summary>
    public static readonly BindableProperty AllowSpacesProperty = BindableProperty.Create(
        nameof(AllowSpaces),
        typeof(bool),
        typeof(LettersOnlyValidation),
        true);

    /// <summary>
    /// Gets or sets a value indicating whether [allow spaces].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [allow spaces]; otherwise, <c>false</c>.
    /// </value>
    public bool AllowSpaces
    {
        get => (bool)GetValue(AllowSpacesProperty);
        set => SetValue(AllowSpacesProperty, value);
    }

    /// <summary>
    /// Validates the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool Validate(object value)
    {
        if (value is string text)
        {
            if (AllowSpaces)
            {
                return text.All(x => char.IsLetter(x) || char.IsWhiteSpace(x));
            }

            return text.All(char.IsLetter);
        }

        return false;
    }
}
