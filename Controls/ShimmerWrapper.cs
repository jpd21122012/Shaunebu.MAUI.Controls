using Shaunebu.Controls.Enums;

namespace Shaunebu.Controls.Controls;

[ContentProperty(nameof(UserContent))]
public class ShimmerWrapper : ContentView
{
    /// <summary>
    /// The is loading property
    /// </summary>
    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(ShimmerWrapper), false, propertyChanged: OnIsLoadingChanged);

    /// <summary>
    /// The corner radius property
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(ShimmerWrapper), 12f, propertyChanged: OnShapeChanged);

    /// <summary>
    /// The shape property
    /// </summary>
    public static readonly BindableProperty ShapeProperty =
        BindableProperty.Create(nameof(Shape), typeof(ShimmerShape), typeof(ShimmerWrapper), ShimmerShape.None, propertyChanged: OnShapeChanged);

    /// <summary>
    /// The placeholder template property
    /// </summary>
    public static readonly BindableProperty PlaceholderTemplateProperty =
        BindableProperty.Create(nameof(PlaceholderTemplate), typeof(DataTemplate), typeof(ShimmerWrapper), propertyChanged: OnPlaceholderTemplateChanged);

    /// <summary>
    /// The animation direction property
    /// </summary>
    public static readonly BindableProperty AnimationDirectionProperty =
        BindableProperty.Create(nameof(AnimationDirection), typeof(ShimmerDirection), typeof(ShimmerWrapper), ShimmerDirection.LeftToRight, propertyChanged: OnAnimationDirectionChanged);

    /// <summary>
    /// The shimmer speed property
    /// </summary>
    public static readonly BindableProperty ShimmerSpeedProperty =
        BindableProperty.Create(nameof(ShimmerSpeed), typeof(int), typeof(ShimmerWrapper), 1000, propertyChanged: OnShimmerSettingsChanged);

    /// <summary>
    /// The shimmer width property
    /// </summary>
    public static readonly BindableProperty ShimmerWidthProperty =
        BindableProperty.Create(nameof(ShimmerWidth), typeof(double), typeof(ShimmerWrapper), 0.3, propertyChanged: OnShimmerSettingsChanged);

    /// <summary>
    /// The shimmer color property
    /// </summary>
    public static readonly BindableProperty ShimmerColorProperty =
        BindableProperty.Create(nameof(ShimmerColor), typeof(Color), typeof(ShimmerWrapper), Colors.White.WithAlpha(0.5f), propertyChanged: OnShimmerSettingsChanged);

    /// <summary>
    /// The base color property
    /// </summary>
    public static readonly BindableProperty BaseColorProperty =
        BindableProperty.Create(nameof(BaseColor), typeof(Color), typeof(ShimmerWrapper), Colors.Gray.WithAlpha(0.2f), propertyChanged: OnShimmerSettingsChanged);

    /// <summary>
    /// The shimmer overlay property
    /// </summary>
    public static readonly BindableProperty ShimmerOverlayProperty =
        BindableProperty.Create(nameof(ShimmerOverlay), typeof(View), typeof(ShimmerWrapper));

    /// <summary>
    /// The container
    /// </summary>
    private readonly Grid _container;

    /// <summary>
    /// The shimmer
    /// </summary>
    private readonly ShimmerControl _shimmer;

    /// <summary>
    /// The user content
    /// </summary>
    private View _userContent;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShimmerWrapper"/> class.
    /// </summary>
    public ShimmerWrapper()
    {
        _container = new Grid();
        _shimmer = new ShimmerControl
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            IsActive = true
        };
        base.Content = _container;
    }

    /// <summary>
    /// Gets or sets the content of the user.
    /// </summary>
    /// <value>
    /// The content of the user.
    /// </value>
    public View UserContent
    {
        get => _userContent;
        set
        {
            if (_userContent != value)
            {
                _userContent = value;
                UpdateContent();
            }
        }
    }

    /// <summary>
    /// Gets or sets the corner radius.
    /// </summary>
    /// <value>
    /// The corner radius.
    /// </value>
    public float CornerRadius { get => (float)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is loading.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is loading; otherwise, <c>false</c>.
    /// </value>
    public bool IsLoading { get => (bool)GetValue(IsLoadingProperty); set => SetValue(IsLoadingProperty, value); }

    /// <summary>
    /// Gets or sets the shape.
    /// </summary>
    /// <value>
    /// The shape.
    /// </value>
    public ShimmerShape Shape { get => (ShimmerShape)GetValue(ShapeProperty); set => SetValue(ShapeProperty, value); }

    /// <summary>
    /// Gets or sets the placeholder template.
    /// </summary>
    /// <value>
    /// The placeholder template.
    /// </value>
    public DataTemplate PlaceholderTemplate { get => (DataTemplate)GetValue(PlaceholderTemplateProperty); set => SetValue(PlaceholderTemplateProperty, value); }

    /// <summary>
    /// Gets or sets the animation direction.
    /// </summary>
    /// <value>
    /// The animation direction.
    /// </value>
    public ShimmerDirection AnimationDirection { get => (ShimmerDirection)GetValue(AnimationDirectionProperty); set => SetValue(AnimationDirectionProperty, value); }

    /// <summary>
    /// Gets or sets the shimmer speed.
    /// </summary>
    /// <value>
    /// The shimmer speed.
    /// </value>
    public int ShimmerSpeed { get => (int)GetValue(ShimmerSpeedProperty); set => SetValue(ShimmerSpeedProperty, value); }

    /// <summary>
    /// Gets or sets the width of the shimmer.
    /// </summary>
    /// <value>
    /// The width of the shimmer.
    /// </value>
    public double ShimmerWidth { get => (double)GetValue(ShimmerWidthProperty); set => SetValue(ShimmerWidthProperty, value); }

    /// <summary>
    /// Gets or sets the color of the shimmer.
    /// </summary>
    /// <value>
    /// The color of the shimmer.
    /// </value>
    public Color ShimmerColor { get => (Color)GetValue(ShimmerColorProperty); set => SetValue(ShimmerColorProperty, value); }

    /// <summary>
    /// Gets or sets the color of the base.
    /// </summary>
    /// <value>
    /// The color of the base.
    /// </value>
    public Color BaseColor { get => (Color)GetValue(BaseColorProperty); set => SetValue(BaseColorProperty, value); }

    /// <summary>
    /// Gets or sets the shimmer overlay.
    /// </summary>
    /// <value>
    /// The shimmer overlay.
    /// </value>
    public View ShimmerOverlay { get => (View)GetValue(ShimmerOverlayProperty); set => SetValue(ShimmerOverlayProperty, value); }

    /// <summary>
    /// Updates the content.
    /// </summary>
    private void UpdateContent()
    {
        _container.Children.Clear();

        if (PlaceholderTemplate != null)
        {
            var placeholderView = PlaceholderTemplate.CreateContent() as View;
            if (placeholderView != null)
                _userContent = placeholderView;
        }
        else if (Shape != ShimmerShape.None)
        {
            _userContent = Shape switch
            {
                ShimmerShape.Circle => new BoxView { CornerRadius = 999, HeightRequest = 80, WidthRequest = 80 },
                ShimmerShape.RoundedRectangle => new BoxView { CornerRadius = CornerRadius, HeightRequest = 20, WidthRequest = 150 },
                _ => new BoxView { CornerRadius = CornerRadius, HeightRequest = 20, WidthRequest = 200 }
            };
        }

        if (_userContent != null)
            _container.Children.Add(_userContent);

        // Add shimmer overlay or default shimmer
        if (ShimmerOverlay != null)
            _container.Children.Add(ShimmerOverlay);
        else
        {
            _shimmer.AnimationDirection = AnimationDirection;
            _shimmer.ShimmerSpeed = ShimmerSpeed;
            _shimmer.ShimmerWidth = ShimmerWidth;
            _shimmer.ShimmerColor = ShimmerColor;
            _shimmer.BaseColor = BaseColor;

            _container.Children.Add(_shimmer);
        }

        UpdateShimmerState();
    }

    /// <summary>
    /// Updates the state of the shimmer.
    /// </summary>
    private void UpdateShimmerState()
    {
        if (_userContent == null) return;

        if (IsLoading)
        {
            _shimmer.IsVisible = true;
            _shimmer.StartShimmer();
            _userContent.Opacity = 0.3;
            _userContent.InputTransparent = true;
        }
        else
        {
            _shimmer.IsVisible = false;
            _shimmer.StopShimmer();
            _userContent.Opacity = 1;
            _userContent.InputTransparent = false;
        }
    }

    /// <summary>
    /// Called when [is loading changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnIsLoadingChanged(BindableObject bindable, object oldValue, object newValue) => (bindable as ShimmerWrapper)?.UpdateShimmerState();

    /// <summary>
    /// Called when [shape changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnShapeChanged(BindableObject bindable, object oldValue, object newValue) => (bindable as ShimmerWrapper)?.UpdateContent();

    /// <summary>
    /// Called when [placeholder template changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnPlaceholderTemplateChanged(BindableObject bindable, object oldValue, object newValue) => (bindable as ShimmerWrapper)?.UpdateContent();

    /// <summary>
    /// Called when [animation direction changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnAnimationDirectionChanged(BindableObject bindable, object oldValue, object newValue) { if (bindable is ShimmerWrapper wrapper) wrapper._shimmer.Invalidate(); }

    /// <summary>
    /// Called when [shimmer settings changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnShimmerSettingsChanged(BindableObject bindable, object oldValue, object newValue) { if (bindable is ShimmerWrapper wrapper) wrapper._shimmer.Invalidate(); }
}