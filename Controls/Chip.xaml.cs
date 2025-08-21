using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;

namespace Shaunebu.Controls.Controls;

public partial class Chip : ContentView
{

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(Chip), string.Empty, propertyChanged: OnTextChanged);

    /// <summary>
    /// The text color property
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Chip), Colors.Black, propertyChanged: OnTextColorChanged);

    /// <summary>
    /// The chip background color property
    /// </summary>
    public static readonly BindableProperty ChipBackgroundColorProperty = BindableProperty.Create(nameof(ChipBackgroundColor), typeof(Color), typeof(Chip), Colors.LightGray, propertyChanged: OnBackgroundColorChanged);

    /// <summary>
    /// The border color property
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(Chip), Colors.Gray, propertyChanged: OnBorderColorChanged);

    /// <summary>
    /// The corner radius property
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(Chip), 16.0, propertyChanged: OnCornerRadiusChanged);

    /// <summary>
    /// The icon property
    /// </summary>
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(Chip), null, propertyChanged: OnIconChanged);

    /// <summary>
    /// The is closable property
    /// </summary>
    public static readonly BindableProperty IsClosableProperty = BindableProperty.Create(nameof(IsClosable), typeof(bool), typeof(Chip), false, propertyChanged: OnIsClosableChanged);

    /// <summary>
    /// The command property
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(Chip));

    /// <summary>
    /// The command parameter property
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(Chip));

    /// <summary>
    /// The close command property
    /// </summary>
    public static readonly BindableProperty CloseCommandProperty = BindableProperty.Create(nameof(CloseCommand), typeof(ICommand), typeof(Chip));

    /// <summary>
    /// The is selected property
    /// </summary>
    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(Chip), false, BindingMode.TwoWay, propertyChanged: OnIsSelectedChanged);

    /// <summary>
    /// The badge text property
    /// </summary>
    public static readonly BindableProperty BadgeTextProperty = BindableProperty.Create(nameof(BadgeText), typeof(string), typeof(Chip), string.Empty, propertyChanged: OnBadgeTextChanged);

    /// <summary>
    /// The badge color property
    /// </summary>
    public static readonly BindableProperty BadgeColorProperty = BindableProperty.Create(nameof(BadgeColor), typeof(Color), typeof(Chip), Colors.Red, propertyChanged: OnBadgeColorChanged);

    /// <summary>
    /// Occurs when [clicked].
    /// </summary>
    public event EventHandler Clicked;

    /// <summary>
    /// Occurs when [closed].
    /// </summary>
    public event EventHandler Closed;

    /// <summary>
    /// Initializes a new instance of the <see cref="Chip"/> class.
    /// </summary>
    public Chip()
    {
        InitializeComponent();
        SetupGestures();

        this.MinimumHeightRequest = 24;
        this.MinimumWidthRequest = 48;
    }

    #region Properties    
    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    /// <value>
    /// The text.
    /// </value>
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

    /// <summary>
    /// Gets or sets the color of the text.
    /// </summary>
    /// <value>
    /// The color of the text.
    /// </value>
    public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }

    /// <summary>
    /// Gets or sets the color of the chip background.
    /// </summary>
    /// <value>
    /// The color of the chip background.
    /// </value>
    public Color ChipBackgroundColor { get => (Color)GetValue(ChipBackgroundColorProperty); set => SetValue(ChipBackgroundColorProperty, value); }

    /// <summary>
    /// Gets or sets the color of the border.
    /// </summary>
    /// <value>
    /// The color of the border.
    /// </value>
    public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }

    /// <summary>
    /// Gets or sets the corner radius.
    /// </summary>
    /// <value>
    /// The corner radius.
    /// </value>
    public double CornerRadius { get => (double)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }

    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    /// <value>
    /// The icon.
    /// </value>
    public ImageSource Icon { get => (ImageSource)GetValue(IconProperty); set => SetValue(IconProperty, value); }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is closable.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is closable; otherwise, <c>false</c>.
    /// </value>
    public bool IsClosable { get => (bool)GetValue(IsClosableProperty); set => SetValue(IsClosableProperty, value); }

    /// <summary>
    /// Gets or sets the command.
    /// </summary>
    /// <value>
    /// The command.
    /// </value>
    public ICommand Command { get => (ICommand)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }

    /// <summary>
    /// Gets or sets the command parameter.
    /// </summary>
    /// <value>
    /// The command parameter.
    /// </value>
    public object CommandParameter { get => GetValue(CommandParameterProperty); set => SetValue(CommandParameterProperty, value); }

    /// <summary>
    /// Gets or sets the close command.
    /// </summary>
    /// <value>
    /// The close command.
    /// </value>
    public ICommand CloseCommand { get => (ICommand)GetValue(CloseCommandProperty); set => SetValue(CloseCommandProperty, value); }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is selected.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
    /// </value>
    public bool IsSelected { get => (bool)GetValue(IsSelectedProperty); set => SetValue(IsSelectedProperty, value); }

    /// <summary>
    /// Gets or sets the badge text.
    /// </summary>
    /// <value>
    /// The badge text.
    /// </value>
    public string BadgeText { get => (string)GetValue(BadgeTextProperty); set => SetValue(BadgeTextProperty, value); }

    /// <summary>
    /// Gets or sets the color of the badge.
    /// </summary>
    /// <value>
    /// The color of the badge.
    /// </value>
    public Color BadgeColor { get => (Color)GetValue(BadgeColorProperty); set => SetValue(BadgeColorProperty, value); }
    #endregion

    #region PropertyChanged    
    /// <summary>
    /// Called when [text changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) => ((Chip)bindable).textLabel.Text = newValue?.ToString();

    /// <summary>
    /// Called when [text color changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnTextColorChanged(BindableObject bindable, object oldValue, object newValue) => ((Chip)bindable).textLabel.TextColor = (Color)newValue;

    /// <summary>
    /// Called when [background color changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue) => ((Chip)bindable).chipBorder.BackgroundColor = (Color)newValue;

    /// <summary>
    /// Called when [border color changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnBorderColorChanged(BindableObject bindable, object oldValue, object newValue) => ((Chip)bindable).chipBorder.Stroke = (Color)newValue;

    /// <summary>
    /// Called when [corner radius changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (((Chip)bindable).chipBorder.StrokeShape is RoundRectangle rr)
            rr.CornerRadius = (double)newValue;
    }

    /// <summary>
    /// Called when [icon changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnIconChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var chip = (Chip)bindable;
        chip.iconImage.Source = newValue as ImageSource;
        chip.iconImage.IsVisible = newValue != null;
    }

    /// <summary>
    /// Called when [is closable changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnIsClosableChanged(BindableObject bindable, object oldValue, object newValue) => ((Chip)bindable).closeButton.IsVisible = (bool)newValue;

    /// <summary>
    /// Called when [is selected changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnIsSelectedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var chip = (Chip)bindable;
        chip.chipBorder.BackgroundColor = (bool)newValue ? Colors.Blue : chip.ChipBackgroundColor;
        chip.textLabel.TextColor = (bool)newValue ? Colors.White : chip.TextColor;
    }

    /// <summary>
    /// Called when [badge text changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnBadgeTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var chip = (Chip)bindable;
        chip.badgeLabel.Text = newValue?.ToString();
        chip.badgeBorder.IsVisible = !string.IsNullOrEmpty(newValue?.ToString());
    }

    /// <summary>
    /// Called when [badge color changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnBadgeColorChanged(BindableObject bindable, object oldValue, object newValue) => ((Chip)bindable).badgeBorder.BackgroundColor = (Color)newValue;


    /// <summary>
    /// Setups the gestures.
    /// </summary>
    private void SetupGestures()
    {
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (s, e) =>
        {
            Clicked?.Invoke(this, EventArgs.Empty);
            Command?.Execute(CommandParameter);
        };
        chipBorder.GestureRecognizers.Add(tapGesture);

        closeButton.Clicked += (s, e) =>
        {
            Closed?.Invoke(this, EventArgs.Empty);
            CloseCommand?.Execute(null);
        };
    }
    #endregion   

}