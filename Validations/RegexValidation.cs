using Shaunebu.Controls.Abstractions;
using System.Text.RegularExpressions;

namespace Shaunebu.Controls.Validations;

public class RegexValidation : BindableObject, IValidation
{
    /// <summary>
    /// Gets or sets the pattern.
    /// </summary>
    /// <value>
    /// The pattern.
    /// </value>
    public string Pattern
    {
        get => (string)GetValue(PatternProperty);
        set => SetValue(PatternProperty, value);
    }

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
    /// The pattern property
    /// </summary>
    public static readonly BindableProperty PatternProperty = BindableProperty.Create(
        nameof(Pattern),
        typeof(string),
        typeof(RegexValidation),
        string.Empty);

    /// <summary>
    /// The message property
    /// </summary>
    public static readonly BindableProperty MessageProperty = BindableProperty.Create(
        nameof(Message),
        typeof(string),
        typeof(RegexValidation),
        "The field isn't valid.");

    /// <summary>
    /// Validates the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool Validate(object value)
    {
        if (value is string text)
        {
            var result = Regex.Match(text, Pattern);

            return result.Success;
        }

        return true;
    }
}