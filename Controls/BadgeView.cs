using Microsoft.Maui.Controls.Shapes;
using Shaunebu.Controls.Enums;
using System.Windows.Input;

namespace Shaunebu.Controls.Controls;

public class BadgeView : ContentView
{

    #region Fields
    private Border _badgeContainer;
    private Label _badgeLabel;
    private ContentView _contentContainer;
    private TapGestureRecognizer _tapGesture;
    #endregion


    #region Events
    public event EventHandler Tapped;
    public event EventHandler BadgeTapped;
    #endregion

    #region Bindable Properties
    public static readonly BindableProperty BadgeShapeProperty =
   BindableProperty.Create(nameof(BadgeShape), typeof(BadgeShape), typeof(BadgeView), BadgeShape.Circle,
       propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty StrokeColorProperty =
        BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(BadgeView), null,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty StrokeThicknessProperty =
        BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(BadgeView), 0.0,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty MaxValueProperty =
        BindableProperty.Create(nameof(MaxValue), typeof(int), typeof(BadgeView), 99,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty ShowMaxValueProperty =
        BindableProperty.Create(nameof(ShowMaxValue), typeof(bool), typeof(BadgeView), true,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty BadgeAnimationProperty =
        BindableProperty.Create(nameof(BadgeAnimation), typeof(BadgeAnimation), typeof(BadgeView), BadgeAnimation.None);

    public static readonly BindableProperty AnimationDurationProperty =
        BindableProperty.Create(nameof(AnimationDuration), typeof(int), typeof(BadgeView), 300);

    public static readonly BindableProperty IsInteractiveProperty =
        BindableProperty.Create(nameof(IsInteractive), typeof(bool), typeof(BadgeView), true);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(BadgeView), null);

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(BadgeView), null);

    public static readonly BindableProperty BadgeTemplateProperty =
        BindableProperty.Create(nameof(BadgeTemplate), typeof(DataTemplate), typeof(BadgeView), null,
            propertyChanged: OnBadgeTemplateChanged);

    public static readonly BindableProperty BadgeTextProperty =
        BindableProperty.Create(nameof(BadgeText), typeof(string), typeof(BadgeView), "0",
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty BadgeTypeProperty =
        BindableProperty.Create(nameof(BadgeType), typeof(BadgeType), typeof(BadgeView), BadgeType.Primary,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty BadgePositionProperty =
        BindableProperty.Create(nameof(BadgePosition), typeof(BadgePosition), typeof(BadgeView), BadgePosition.TopRight,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty AnimationTypeProperty =
        BindableProperty.Create(nameof(AnimationType), typeof(AnimationType), typeof(BadgeView), AnimationType.None);

    public static readonly BindableProperty AutoHideProperty =
        BindableProperty.Create(nameof(AutoHide), typeof(bool), typeof(BadgeView), false,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty BadgeSizeProperty =
        BindableProperty.Create(nameof(BadgeSize), typeof(double), typeof(BadgeView), 20.0,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(BadgeView), 12.0,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(BadgeView), 10.0,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(BadgeView), null,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BadgeView), null,
            propertyChanged: OnBadgePropertyChanged);

    public static readonly BindableProperty ContentProperty =
        BindableProperty.Create(nameof(Content), typeof(View), typeof(BadgeView), null,
            propertyChanged: OnContentChanged);
    #endregion

    #region Properties
    public BadgeShape BadgeShape
    {
        get => (BadgeShape)GetValue(BadgeShapeProperty);
        set => SetValue(BadgeShapeProperty, value);
    }

    public Color StrokeColor
    {
        get => (Color)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);
    }

    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    public int MaxValue
    {
        get => (int)GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    public bool ShowMaxValue
    {
        get => (bool)GetValue(ShowMaxValueProperty);
        set => SetValue(ShowMaxValueProperty, value);
    }

    public BadgeAnimation BadgeAnimation
    {
        get => (BadgeAnimation)GetValue(BadgeAnimationProperty);
        set => SetValue(BadgeAnimationProperty, value);
    }

    public int AnimationDuration
    {
        get => (int)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    public bool IsInteractive
    {
        get => (bool)GetValue(IsInteractiveProperty);
        set => SetValue(IsInteractiveProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public DataTemplate BadgeTemplate
    {
        get => (DataTemplate)GetValue(BadgeTemplateProperty);
        set => SetValue(BadgeTemplateProperty, value);
    }
    public string BadgeText
    {
        get => (string)GetValue(BadgeTextProperty);
        set => SetValue(BadgeTextProperty, value);
    }

    public BadgeType BadgeType
    {
        get => (BadgeType)GetValue(BadgeTypeProperty);
        set => SetValue(BadgeTypeProperty, value);
    }

    public BadgePosition BadgePosition
    {
        get => (BadgePosition)GetValue(BadgePositionProperty);
        set => SetValue(BadgePositionProperty, value);
    }

    public AnimationType AnimationType
    {
        get => (AnimationType)GetValue(AnimationTypeProperty);
        set => SetValue(AnimationTypeProperty, value);
    }

    public bool AutoHide
    {
        get => (bool)GetValue(AutoHideProperty);
        set => SetValue(AutoHideProperty, value);
    }

    public double BadgeSize
    {
        get => (double)GetValue(BadgeSizeProperty);
        set => SetValue(BadgeSizeProperty, value);
    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public new View Content
    {
        get => (View)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
    #endregion

    #region Constructor
    public BadgeView()
    {
        InitializeLayout();
        UpdateBadge();
    }
    #endregion

    #region Methods
    private void InitializeLayout()
    {
        // Create the badge label
        _badgeLabel = new Label
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };

        // Create the badge container
        _badgeContainer = new Border
        {
            Padding = new Thickness(4),
            Stroke = Colors.Transparent,
            StrokeThickness = 0,
            Content = _badgeLabel,
            ZIndex = 1,
        };

        // Create container for the content
        _contentContainer = new ContentView();

        // Create the main grid
        var grid = new Grid();
        grid.Children.Add(_contentContainer);
        grid.Children.Add(_badgeContainer);


        // Create tap gesture recognizer
        _tapGesture = new TapGestureRecognizer();
        _tapGesture.Tapped += OnBadgeTapped;


        base.Content = grid;
    }

    private void UpdateBadge()
    {
        // Handle max value display
        string displayText = BadgeText;
        if (ShowMaxValue && int.TryParse(BadgeText, out int numericValue) && numericValue > MaxValue)
        {
            displayText = $"{MaxValue}+";
        }

        _badgeLabel.Text = displayText;

        // Update badge visibility based on AutoHide
        bool shouldHide = AutoHide && (string.IsNullOrEmpty(BadgeText) || BadgeText == "0");
        _badgeContainer.IsVisible = !shouldHide;

        // Update badge colors
        UpdateBadgeColors();

        // Update font size first
        _badgeLabel.FontSize = FontSize;

        // Update badge shape
        UpdateBadgeShape();

        // Update stroke
        _badgeContainer.Stroke = StrokeColor ?? Colors.Transparent;
        _badgeContainer.StrokeThickness = StrokeThickness;
        _badgeContainer.StrokeShape = GetStrokeShape();

        // Calculate dynamic sizing
        double dynamicHeight = CalculateDynamicHeight();
        _badgeContainer.HeightRequest = Math.Max(BadgeSize, dynamicHeight);
        _badgeContainer.WidthRequest = CalculateDynamicWidth(displayText);


        // Update badge position
        UpdateBadgePosition();

        // Auto-animate if configured
        if (BadgeAnimation != BadgeAnimation.None && !shouldHide)
        {
            AnimateBadge();
        }


        // Update interactivity
        UpdateInteractivity();
    }

    // Helper method to calculate dynamic height based on font size
    private double CalculateDynamicHeight()
    {
        // Base height calculation: font size + padding
        // You can adjust the multiplier as needed
        return FontSize * 2.2; // This provides good padding for most font sizes
    }

    // Helper method to calculate dynamic width based on text length
    private double CalculateDynamicWidth(string text)
    {
        if (string.IsNullOrEmpty(text))
            return CalculateDynamicHeight(); // Make it square for empty text

        // Calculate width based on text length and font size
        double baseWidth = CalculateDynamicHeight(); // Start with square size
        double additionalWidth = (text.Length - 1) * (FontSize * 0.7);

        // Ensure minimum width is at least the height (for single characters)
        return Math.Max(baseWidth, baseWidth + additionalWidth);
    }

    private void UpdateBadgeColors()
    {
        Color backgroundColor, textColor;

        if (BackgroundColor != null)
        {
            backgroundColor = BackgroundColor;
            textColor = TextColor ?? Colors.White;
        }
        else
        {
            (backgroundColor, textColor) = BadgeType switch
            {
                BadgeType.Primary => (Color.FromArgb("#0078D4"), Colors.White),
                BadgeType.Secondary => (Color.FromArgb("#6C757D"), Colors.White),
                BadgeType.Success => (Color.FromArgb("#198754"), Colors.White),
                BadgeType.Error => (Color.FromArgb("#DC3545"), Colors.White),
                BadgeType.Warning => (Color.FromArgb("#FFC107"), Colors.Black),
                BadgeType.Info => (Color.FromArgb("#0DCAF0"), Colors.Black),
                _ => (Colors.Red, Colors.White)
            };
        }

        _badgeContainer.BackgroundColor = backgroundColor;
        _badgeLabel.TextColor = textColor;
    }

    private void UpdateBadgeShape()
    {
        var strokeShape = GetStrokeShape();
        _badgeContainer.StrokeShape = strokeShape;
    }

    private IShape GetStrokeShape()
    {
        return BadgeShape switch
        {
            BadgeShape.Circle => new RoundRectangle { CornerRadius = new CornerRadius(_badgeContainer.HeightRequest / 2) },
            BadgeShape.Pill => new RoundRectangle { CornerRadius = new CornerRadius(20) },
            BadgeShape.Rectangle => new RoundRectangle { CornerRadius = new CornerRadius(CornerRadius) },
            _ => new RoundRectangle { CornerRadius = new CornerRadius(CornerRadius) }
        };
    }

    private void OnBadgeTapped(object sender, EventArgs e)
    {
        // Raise the event
        Tapped?.Invoke(this, EventArgs.Empty);

        // Execute the command
        if (Command?.CanExecute(CommandParameter) == true)
        {
            Command.Execute(CommandParameter);
        }

        // Visual feedback
        Device.BeginInvokeOnMainThread(async () =>
        {
            await _badgeContainer.FadeTo(0.5, 100);
            await _badgeContainer.FadeTo(1.0, 100);
        });
    }

    private void UpdateInteractivity()
    {
        _badgeContainer.GestureRecognizers.Clear();

        if (IsInteractive)
        {
            _badgeContainer.GestureRecognizers.Add(_tapGesture);
        }
    }
    private void OnBadgeContainerTapped(object sender, EventArgs e)
    {

        // Raise the event
        BadgeTapped?.Invoke(this, EventArgs.Empty);

        // Execute the command
        if (Command?.CanExecute(CommandParameter ?? BadgeText) == true)
        {
            Command.Execute(CommandParameter ?? BadgeText);
        }

        // Optional: Add visual feedback
        VisualFeedback();
    }

    private async void VisualFeedback()
    {
        if (IsInteractive)
        {
            await _badgeContainer.FadeTo(0.6, 100);
            await _badgeContainer.FadeTo(1.0, 100);
        }
    }

    public void AnimateBadge()
    {
        switch (BadgeAnimation)
        {
            case BadgeAnimation.Bounce:
                this.TranslateTo(0, -10, (uint)AnimationDuration / 2)
                    .ContinueWith(_ => this.TranslateTo(0, 0, (uint)AnimationDuration / 2));
                break;

            case BadgeAnimation.Pulse:
                this.FadeTo(0.7, (uint)AnimationDuration / 2)
                    .ContinueWith(_ => this.FadeTo(1.0, (uint)AnimationDuration / 2));
                break;

            case BadgeAnimation.Shake:
                this.TranslateTo(-5, 0, 50)
                    .ContinueWith(_ => this.TranslateTo(5, 0, 50))
                    .ContinueWith(_ => this.TranslateTo(0, 0, 50));
                break;

            case BadgeAnimation.Fade:
                this.FadeTo(0, (uint)AnimationDuration)
                    .ContinueWith(_ => this.FadeTo(1, (uint)AnimationDuration));
                break;

            case BadgeAnimation.Scale:
                this.ScaleTo(1.2, (uint)AnimationDuration / 2)
                    .ContinueWith(_ => this.ScaleTo(1.0, (uint)AnimationDuration / 2));
                break;
        }
    }

    private static void OnBadgeTemplateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BadgeView badgeView && newValue is DataTemplate template)
        {
            badgeView.ApplyBadgeTemplate(template);
        }
    }

    private void ApplyBadgeTemplate(DataTemplate template)
    {
        if (template != null)
        {
            var content = template.CreateContent() as View;
            if (content != null)
            {
                _badgeContainer.Content = content;
                // You might want to set binding context here if needed
                if (content.BindingContext == null)
                {
                    content.BindingContext = this;
                }
            }
        }
        else
        {
            _badgeContainer.Content = _badgeLabel;
        }
    }

    private void UpdateBadgePosition()
    {
        _badgeContainer.HorizontalOptions = BadgePosition switch
        {
            BadgePosition.TopRight or BadgePosition.BottomRight => LayoutOptions.End,
            BadgePosition.TopLeft or BadgePosition.BottomLeft => LayoutOptions.Start,
            _ => LayoutOptions.End
        };

        _badgeContainer.VerticalOptions = BadgePosition switch
        {
            BadgePosition.TopRight or BadgePosition.TopLeft => LayoutOptions.Start,
            BadgePosition.BottomRight or BadgePosition.BottomLeft => LayoutOptions.End,
            _ => LayoutOptions.Start
        };

        // Use actual height for margin calculation instead of fixed BadgeSize
        var actualHeight = _badgeContainer.HeightRequest;
        var margin = actualHeight / 3;

        _badgeContainer.Margin = BadgePosition switch
        {
            BadgePosition.TopRight => new Thickness(0, -margin, -margin, 0),
            BadgePosition.TopLeft => new Thickness(-margin, -margin, 0, 0),
            BadgePosition.BottomRight => new Thickness(0, 0, -margin, -margin),
            BadgePosition.BottomLeft => new Thickness(-margin, 0, 0, -margin),
            _ => new Thickness(0, -margin, -margin, 0)
        };
    }

    public void Animate()
    {
        switch (AnimationType)
        {
            case AnimationType.Scale:
                this.ScaleTo(1.2, 100)
                    .ContinueWith(_ => this.ScaleTo(1.0, 100));
                break;

            case AnimationType.Fade:
                this.FadeTo(0.5, 100)
                    .ContinueWith(_ => this.FadeTo(1.0, 100));
                break;

            case AnimationType.Shake:
                this.TranslateTo(-5, 0, 50)
                    .ContinueWith(_ => this.TranslateTo(5, 0, 50))
                    .ContinueWith(_ => this.TranslateTo(0, 0, 50));
                break;
        }
    }

    private static void OnBadgePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BadgeView badgeView)
        {
            badgeView.UpdateBadge();
        }
    }

    private static void OnContentChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BadgeView badgeView && newValue is View newContent)
        {
            badgeView._contentContainer.Content = newContent;
        }
    }
    #endregion
}
