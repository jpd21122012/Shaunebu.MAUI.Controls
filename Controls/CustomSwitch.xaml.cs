using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;

namespace Shaunebu.Controls.Controls;

public partial class CustomSwitch : ContentView
{
    #region Bindable Properties

    public static readonly BindableProperty IsToggledProperty =
        BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(CustomSwitch), false,
            BindingMode.TwoWay, propertyChanged: OnIsToggledChanged);

    public static readonly BindableProperty ThumbColorProperty =
        BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(CustomSwitch), Colors.White);

    public static readonly BindableProperty OnColorProperty =
        BindableProperty.Create(nameof(OnColor), typeof(Color), typeof(CustomSwitch), Color.FromArgb("#FF4CAF50"));

    public static readonly BindableProperty OffColorProperty =
        BindableProperty.Create(nameof(OffColor), typeof(Color), typeof(CustomSwitch), Color.FromArgb("#FF9E9E9E"));

    public static readonly BindableProperty ThumbCornerRadiusProperty =
        BindableProperty.Create(nameof(ThumbCornerRadius), typeof(double), typeof(CustomSwitch), 12.0);

    public static readonly BindableProperty TrackCornerRadiusProperty =
        BindableProperty.Create(nameof(TrackCornerRadius), typeof(double), typeof(CustomSwitch), 14.0);

    public static readonly BindableProperty ThumbSizeProperty =
        BindableProperty.Create(nameof(ThumbSize), typeof(double), typeof(CustomSwitch), 24.0);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomSwitch));

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomSwitch));

    public static readonly BindableProperty IsEnabledProperty =
        BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(CustomSwitch), true,
            propertyChanged: OnIsEnabledChanged);

    public static readonly BindableProperty OnTextProperty =
        BindableProperty.Create(nameof(OnText), typeof(string), typeof(CustomSwitch), "ON",
            propertyChanged: OnTextPropertyChanged);

    public static readonly BindableProperty OffTextProperty =
        BindableProperty.Create(nameof(OffText), typeof(string), typeof(CustomSwitch), "OFF",
            propertyChanged: OnTextPropertyChanged);

    public static readonly BindableProperty ShowTextProperty =
        BindableProperty.Create(nameof(ShowText), typeof(bool), typeof(CustomSwitch), false,
            propertyChanged: OnShowTextChanged);

    public static readonly BindableProperty AnimationDurationProperty =
        BindableProperty.Create(nameof(AnimationDuration), typeof(uint), typeof(CustomSwitch), (uint)100);

    public static readonly BindableProperty ThumbShadowProperty =
        BindableProperty.Create(nameof(ThumbShadow), typeof(Shadow), typeof(CustomSwitch), null,
            propertyChanged: OnThumbShadowChanged);

    public static readonly BindableProperty TrackHeightProperty =
        BindableProperty.Create(nameof(TrackHeight), typeof(double), typeof(CustomSwitch), 30.0);

    public static readonly BindableProperty TrackWidthProperty =
        BindableProperty.Create(nameof(TrackWidth), typeof(double), typeof(CustomSwitch), 50.0);

    public static readonly BindableProperty ThumbOnColorProperty =
        BindableProperty.Create(nameof(ThumbOnColor), typeof(Color), typeof(CustomSwitch), null);

    public static readonly BindableProperty ThumbOffColorProperty =
        BindableProperty.Create(nameof(ThumbOffColor), typeof(Color), typeof(CustomSwitch), null);

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomSwitch), Colors.White);

    public static readonly BindableProperty TextFontSizeProperty =
        BindableProperty.Create(nameof(TextFontSize), typeof(double), typeof(CustomSwitch), 10.0);

    #endregion

    #region Visual Elements

    private Border track;
    private Border thumb;
    private Frame thumbFrame;
    private Label onLabel;
    private Label offLabel;
    private Grid textContainer;

    #endregion

    public CustomSwitch()
    {
        InitializeComponent();
        CreateVisualElements();
        SizeChanged += OnSizeChanged;
    }

    #region Properties

    public bool IsToggled
    {
        get => (bool)GetValue(IsToggledProperty);
        set => SetValue(IsToggledProperty, value);
    }

    public Color ThumbColor
    {
        get => (Color)GetValue(ThumbColorProperty);
        set => SetValue(ThumbColorProperty, value);
    }

    public Color OnColor
    {
        get => (Color)GetValue(OnColorProperty);
        set => SetValue(OnColorProperty, value);
    }

    public Color OffColor
    {
        get => (Color)GetValue(OffColorProperty);
        set => SetValue(OffColorProperty, value);
    }

    public double ThumbCornerRadius
    {
        get => (double)GetValue(ThumbCornerRadiusProperty);
        set => SetValue(ThumbCornerRadiusProperty, value);
    }

    public double TrackCornerRadius
    {
        get => (double)GetValue(TrackCornerRadiusProperty);
        set => SetValue(TrackCornerRadiusProperty, value);
    }

    public double ThumbSize
    {
        get => (double)GetValue(ThumbSizeProperty);
        set => SetValue(ThumbSizeProperty, value);
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

    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    public string OnText
    {
        get => (string)GetValue(OnTextProperty);
        set => SetValue(OnTextProperty, value);
    }

    public string OffText
    {
        get => (string)GetValue(OffTextProperty);
        set => SetValue(OffTextProperty, value);
    }

    public bool ShowText
    {
        get => (bool)GetValue(ShowTextProperty);
        set => SetValue(ShowTextProperty, value);
    }

    public uint AnimationDuration
    {
        get => (uint)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    public Shadow ThumbShadow
    {
        get => (Shadow)GetValue(ThumbShadowProperty);
        set => SetValue(ThumbShadowProperty, value);
    }

    public double TrackHeight
    {
        get => (double)GetValue(TrackHeightProperty);
        set => SetValue(TrackHeightProperty, value);
    }

    public double TrackWidth
    {
        get => (double)GetValue(TrackWidthProperty);
        set => SetValue(TrackWidthProperty, value);
    }

    public Color ThumbOnColor
    {
        get => (Color)GetValue(ThumbOnColorProperty);
        set => SetValue(ThumbOnColorProperty, value);
    }

    public Color ThumbOffColor
    {
        get => (Color)GetValue(ThumbOffColorProperty);
        set => SetValue(ThumbOffColorProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public double TextFontSize
    {
        get => (double)GetValue(TextFontSizeProperty);
        set => SetValue(TextFontSizeProperty, value);
    }

    #endregion

    #region Property Changed Handlers

    private static void OnIsToggledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CustomSwitch customSwitch)
        {
            customSwitch.UpdateVisualState();
            customSwitch.ToggleAnimation();
            customSwitch.RaiseToggledEvent((bool)newValue);
        }
    }

    private static void OnIsEnabledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CustomSwitch customSwitch)
        {
            customSwitch.UpdateEnabledState();
        }
    }

    private static void OnShowTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CustomSwitch customSwitch)
        {
            customSwitch.UpdateTextVisibility();
            customSwitch.RecreateVisualElementsIfNeeded();
        }
    }

    private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CustomSwitch customSwitch)
        {
            customSwitch.UpdateTextContent();
        }
    }

    private static void OnThumbShadowChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CustomSwitch customSwitch && customSwitch.thumb != null)
        {
            customSwitch.thumb.Shadow = customSwitch.ThumbShadow;
        }
    }

    #endregion

    #region Event Handlers

    public event EventHandler<ToggledEventArgs> Toggled;

    private void RaiseToggledEvent(bool isToggled)
    {
        Toggled?.Invoke(this, new ToggledEventArgs(isToggled));
    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
        UpdateVisualState();
    }

    #endregion

    #region Visual Creation

    private void CreateVisualElements()
    {
        switchContainer.Children.Clear();

        // Set container size
        switchContainer.WidthRequest = TrackWidth;
        switchContainer.HeightRequest = TrackHeight;

        // Create track
        track = new Border
        {
            BackgroundColor = OffColor,
            StrokeShape = new RoundRectangle { CornerRadius = TrackCornerRadius },
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            WidthRequest = TrackWidth,
            HeightRequest = TrackHeight
        };

        // Create thumb
        thumb = new Border
        {
            BackgroundColor = ThumbOffColor ?? ThumbColor,
            StrokeShape = new RoundRectangle { CornerRadius = ThumbCornerRadius },
            WidthRequest = ThumbSize,
            HeightRequest = ThumbSize,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(2, 0, 0, 0),
            ZIndex = 10 // Ensure thumb is above text
        };

        // Apply shadow if specified
        if (ThumbShadow != null)
        {
            thumb.Shadow = ThumbShadow;
        }

        // Create container for thumb
        thumbFrame = new Frame
        {
            Content = thumb,
            Padding = 0,
            HasShadow = false,
            CornerRadius = (float)ThumbCornerRadius,
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            ZIndex = 10 // Ensure thumb is above text
        };

        // Create text container
        textContainer = new Grid
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            ColumnSpacing = 0,
            RowSpacing = 0,
            IsVisible = ShowText
        };

        textContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        textContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

        // Create text labels
        onLabel = new Label
        {
            Text = OnText,
            TextColor = TextColor,
            FontSize = TextFontSize,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center
        };

        offLabel = new Label
        {
            Text = OffText,
            TextColor = TextColor,
            FontSize = TextFontSize,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center
        };

        Grid.SetColumn(onLabel, 0);
        Grid.SetColumn(offLabel, 1);

        textContainer.Children.Add(onLabel);
        textContainer.Children.Add(offLabel);

        // Add tap gesture
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += OnTapped;
        track.GestureRecognizers.Add(tapGesture);
        thumbFrame.GestureRecognizers.Add(tapGesture);
        textContainer.GestureRecognizers.Add(tapGesture);

        // Add pan gesture for drag support
        var panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnPanUpdated;
        track.GestureRecognizers.Add(panGesture);
        thumbFrame.GestureRecognizers.Add(panGesture);
        textContainer.GestureRecognizers.Add(panGesture);

        // Add to container in correct order
        switchContainer.Children.Add(track);
        switchContainer.Children.Add(textContainer);
        switchContainer.Children.Add(thumbFrame);

        UpdateVisualState();
        UpdateEnabledState();
    }

    private void RecreateVisualElementsIfNeeded()
    {
        // Recreate visual elements if text visibility changes significantly
        if ((ShowText && textContainer == null) || (!ShowText && textContainer != null))
        {
            CreateVisualElements();
        }
        else
        {
            UpdateTextVisibility();
        }
    }

    #endregion

    #region Interaction Handlers

    private void OnTapped(object sender, EventArgs e)
    {
        if (!IsEnabled) return;

        IsToggled = !IsToggled;

        if (Command?.CanExecute(CommandParameter) == true)
        {
            Command.Execute(CommandParameter);
        }
    }

    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        if (!IsEnabled) return;

        switch (e.StatusType)
        {
            case GestureStatus.Started:
                // Start of drag
                break;

            case GestureStatus.Running:
                // During drag
                var dragThreshold = TrackWidth / 3;
                if (Math.Abs(e.TotalX) > dragThreshold)
                {
                    IsToggled = e.TotalX > 0;
                }
                break;

            case GestureStatus.Completed:
                // End of drag
                var completeThreshold = TrackWidth / 4;
                if (Math.Abs(e.TotalX) > completeThreshold)
                {
                    IsToggled = e.TotalX > 0;
                }
                break;
        }
    }

    #endregion

    #region Visual Updates

    private void UpdateVisualState()
    {
        if (track == null || thumb == null || thumbFrame == null) return;

        // Update track color
        track.BackgroundColor = IsToggled ? OnColor : OffColor;

        // Update thumb color
        if (IsToggled && ThumbOnColor != null)
        {
            thumb.BackgroundColor = ThumbOnColor;
        }
        else if (!IsToggled && ThumbOffColor != null)
        {
            thumb.BackgroundColor = ThumbOffColor;
        }
        else
        {
            thumb.BackgroundColor = ThumbColor;
        }

        // Update thumb position
        if (switchContainer.Width > 0)
        {
            var thumbPosition = IsToggled ?
                switchContainer.Width - ThumbSize - 4 : 2;

            thumbFrame.TranslationX = thumbPosition;
        }

        // Update text visibility and content
        UpdateTextVisibility();
        UpdateTextContent();
    }

    private void UpdateTextVisibility()
    {
        if (textContainer == null || onLabel == null || offLabel == null) return;

        textContainer.IsVisible = ShowText;

        if (ShowText)
        {
            // Show the appropriate label based on state
            onLabel.Opacity = IsToggled ? 1 : 0;
            offLabel.Opacity = IsToggled ? 0 : 1;
        }
    }

    private void UpdateTextContent()
    {
        if (onLabel != null) onLabel.Text = OnText;
        if (offLabel != null) offLabel.Text = OffText;
        if (onLabel != null) onLabel.TextColor = TextColor;
        if (offLabel != null) offLabel.TextColor = TextColor;
        if (onLabel != null) onLabel.FontSize = TextFontSize;
        if (offLabel != null) offLabel.FontSize = TextFontSize;
    }

    private void UpdateEnabledState()
    {
        if (track == null) return;

        this.Opacity = IsEnabled ? 1 : 0.6;

        // Update gesture recognizers based on enabled state
        UpdateGestureRecognizers();
    }

    private void UpdateGestureRecognizers()
    {
        if (track == null || thumbFrame == null || textContainer == null) return;

        // Clear existing gestures
        var gesturesToRemove = new List<IGestureRecognizer>();

        foreach (var recognizer in track.GestureRecognizers)
        {
            if (recognizer is TapGestureRecognizer || recognizer is PanGestureRecognizer)
            {
                gesturesToRemove.Add(recognizer);
            }
        }
        foreach (var recognizer in gesturesToRemove)
        {
            track.GestureRecognizers.Remove(recognizer);
        }

        gesturesToRemove.Clear();
        foreach (var recognizer in thumbFrame.GestureRecognizers)
        {
            if (recognizer is TapGestureRecognizer || recognizer is PanGestureRecognizer)
            {
                gesturesToRemove.Add(recognizer);
            }
        }
        foreach (var recognizer in gesturesToRemove)
        {
            thumbFrame.GestureRecognizers.Remove(recognizer);
        }

        gesturesToRemove.Clear();
        foreach (var recognizer in textContainer.GestureRecognizers)
        {
            if (recognizer is TapGestureRecognizer || recognizer is PanGestureRecognizer)
            {
                gesturesToRemove.Add(recognizer);
            }
        }
        foreach (var recognizer in gesturesToRemove)
        {
            textContainer.GestureRecognizers.Remove(recognizer);
        }

        // Add gestures if enabled
        if (IsEnabled)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += OnTapped;

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;

            track.GestureRecognizers.Add(tapGesture);
            track.GestureRecognizers.Add(panGesture);

            thumbFrame.GestureRecognizers.Add(tapGesture);
            thumbFrame.GestureRecognizers.Add(panGesture);

            textContainer.GestureRecognizers.Add(tapGesture);
            textContainer.GestureRecognizers.Add(panGesture);
        }
    }

    #endregion

    #region Animation

    private async void ToggleAnimation()
    {
        if (thumbFrame == null || !IsEnabled) return;

        // Bounce animation
        await thumbFrame.ScaleTo(0.9, AnimationDuration / 2, Easing.CubicOut);
        await thumbFrame.ScaleTo(1.0, AnimationDuration / 2, Easing.CubicIn);

        // Smooth movement
        if (switchContainer.Width > 0)
        {
            var thumbPosition = IsToggled ?
                switchContainer.Width - ThumbSize - 4 : 2;

            await thumbFrame.TranslateTo(thumbPosition, 0, AnimationDuration, Easing.CubicInOut);
        }
    }

    #endregion

    #region Public Methods

    public void Toggle()
    {
        IsToggled = !IsToggled;
    }

    public async Task ToggleWithAnimationAsync()
    {
        if (!IsEnabled) return;

        IsToggled = !IsToggled;
        await Task.Delay((int)AnimationDuration);
    }

    #endregion
}

public class ToggledEventArgs : EventArgs
{
    public bool Value { get; }

    public ToggledEventArgs(bool value)
    {
        Value = value;
    }
}