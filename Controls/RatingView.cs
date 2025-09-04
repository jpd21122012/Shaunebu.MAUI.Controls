using Microsoft.Maui.Controls.Shapes;
using Shaunebu.Controls.Enums;
using Shaunebu.Controls.Events;
using Shaunebu.Controls.Helpers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace Shaunebu.Controls.Controls;

/// <summary>Rating view control.</summary>
public partial class RatingView : TemplatedView
{
    // --- existing bindable properties (kept) ---
    public static readonly BindableProperty CustomShapePathProperty = BindableProperty.Create(nameof(CustomShapePath), typeof(string), typeof(RatingView), defaultValue: null, propertyChanged: OnCustomShapePathPropertyChanged);
    public static readonly BindableProperty ShapePaddingProperty = BindableProperty.Create(nameof(ShapePadding), typeof(Thickness), typeof(RatingView), new Thickness(0), propertyChanged: OnShapePaddingPropertyChanged, defaultValueCreator: static _ => new Thickness(0));
    public static readonly BindableProperty ShapeProperty = BindableProperty.Create(nameof(Shape), typeof(RatingViewShape), typeof(RatingView), defaultValue: RatingViewShape.Star, propertyChanged: OnShapePropertyChanged, defaultValueCreator: static _ => RatingViewShape.Star);
    public static readonly BindableProperty ShapeBorderColorProperty = BindableProperty.Create(nameof(ShapeBorderColor), typeof(Color), typeof(RatingView), defaultValue: Colors.Grey, propertyChanged: OnShapeBorderColorChanged, defaultValueCreator: static _ => Colors.Grey);
    public static readonly BindableProperty ShapeBorderThicknessProperty = BindableProperty.Create(nameof(ShapeBorderThickness), typeof(double), typeof(RatingView), defaultValue: 1.0, propertyChanged: OnShapeBorderThicknessChanged, defaultValueCreator: static _ => 1.0);
    public static readonly BindableProperty ShapeDiameterProperty = BindableProperty.Create(nameof(ShapeDiameter), typeof(double), typeof(RatingView), defaultValue: 20.0, propertyChanged: OnShapeDiameterSizeChanged, defaultValueCreator: static _ => 20.0);
    public static readonly BindableProperty EmptyShapeColorProperty = BindableProperty.Create(nameof(EmptyShapeColor), typeof(Color), typeof(RatingView), defaultValue: Colors.Transparent, propertyChanged: OnRatingColorChanged, defaultValueCreator: static _ => Colors.Transparent);
    public static readonly BindableProperty FillColorProperty = BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(RatingView), defaultValue: Colors.Yellow, propertyChanged: OnRatingColorChanged, defaultValueCreator: static _ => Colors.Yellow);
    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(RatingView), defaultValue: false, propertyChanged: OnIsReadOnlyChanged);
    public static readonly BindableProperty MaximumRatingProperty = BindableProperty.Create(nameof(MaximumRating), typeof(int), typeof(RatingView), defaultValue: 5, validateValue: IsMaximumRatingValid, propertyChanged: OnMaximumRatingChange);
    public static readonly BindableProperty FillWhenTappedProperty = BindableProperty.Create(nameof(FillOption), typeof(RatingViewFillOption), typeof(RatingView), defaultValue: RatingViewFillOption.Shape, propertyChanged: OnRatingColorChanged);
    public static readonly BindableProperty RatingProperty = BindableProperty.Create(nameof(Rating), typeof(double), typeof(RatingView), defaultValue: 0.0, validateValue: IsRatingValid, propertyChanged: OnRatingChanged);
    public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(RatingView), defaultValue: 10.0, propertyChanged: OnSpacingChanged);

    // ItemTemplate property (already declared earlier in your copy — ensure this matches)
    public static readonly BindableProperty ItemTemplateProperty =
        BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(RatingView),
            default(DataTemplate),
            propertyChanged: OnRatingPropertyChanged);

    readonly WeakEventManager weakEventManager = new();

    ///<summary>Instantiates <see cref="RatingView"/> .</summary>
    public RatingView()
    {
        RatingLayout.SetBinding<RatingView, object>(BindingContextProperty, static ratingView => ratingView.BindingContext, source: this);
        base.ControlTemplate = new ControlTemplate(() => RatingLayout);

        AddChildrenToLayout(0, MaximumRating);
    }

    // Event for rating changed
    public event EventHandler<RatingChangedEventArgs> RatingChanged
    {
        add => weakEventManager.AddEventHandler(value);
        remove => weakEventManager.RemoveEventHandler(value);
    }

    public new ControlTemplate ControlTemplate => base.ControlTemplate;

    // --- Properties (kept) ---
    public string? CustomShapePath
    {
        get => (string?)GetValue(CustomShapePathProperty);
        set => SetValue(CustomShapePathProperty, value);
    }

    [AllowNull]
    public Color EmptyShapeColor
    {
        get => (Color)GetValue(EmptyShapeColorProperty);
        set => SetValue(EmptyShapeColorProperty, value ?? Colors.Transparent);
    }

    [AllowNull]
    public Color FillColor
    {
        get => (Color)GetValue(FillColorProperty);
        set => SetValue(FillColorProperty, value ?? Colors.Transparent);
    }

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    public Thickness ShapePadding
    {
        get => (Thickness)GetValue(ShapePaddingProperty);
        set => SetValue(ShapePaddingProperty, value);
    }

    public double ShapeDiameter
    {
        get => (double)GetValue(ShapeDiameterProperty);
        set => SetValue(ShapeDiameterProperty, value);
    }

    public int MaximumRating
    {
        get => (int)GetValue(MaximumRatingProperty);
        set
        {
            switch (value)
            {
                case <= 0:
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(MaximumRating)} must be greater than 0");
                case > 10:
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(MaximumRating)} cannot be greater than 10");
                default:
                    SetValue(MaximumRatingProperty, value);
                    break;
            }
        }
    }

    public double Rating
    {
        get => (double)GetValue(RatingProperty);
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(Rating)} cannot be less than 0");
            }

            if (value > MaximumRating)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(Rating)} cannot be greater than {nameof(MaximumRating)}");
            }

            SetValue(RatingProperty, value);
        }
    }

    public RatingViewFillOption FillOption
    {
        get => (RatingViewFillOption)GetValue(FillWhenTappedProperty);
        set => SetValue(FillWhenTappedProperty, value);
    }

    public RatingViewShape Shape
    {
        get => (RatingViewShape)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    [AllowNull]
    public Color ShapeBorderColor
    {
        get => (Color)GetValue(ShapeBorderColorProperty);
        set => SetValue(ShapeBorderColorProperty, value ?? Colors.Transparent);
    }

    public double ShapeBorderThickness
    {
        get => (double)GetValue(ShapeBorderThicknessProperty);
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ShapeBorderThickness), $"{nameof(ShapeBorderThickness)} must be greater than 0");
            }

            SetValue(ShapeBorderThicknessProperty, value);
        }
    }

    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    // internal layout (kept)
    internal HorizontalStackLayout RatingLayout { get; } = new();

    static int GetRatingWhenMaximumRatingEqualsOne(double rating) => rating.Equals(0.0) ? 1 : 0;

    // CreateChild unchanged (used as default when ItemTemplate == null)
    static Border CreateChild(in string shape, in Thickness itemPadding, in double shapeBorderThickness, in double itemShapeSize, in Brush shapeBorderColor, in Color itemColor) => new()
    {
        BackgroundColor = itemColor,
        Margin = 0,
        Padding = itemPadding,
        Stroke = new SolidColorBrush(Colors.Transparent),
        StrokeThickness = 0,

        Content = new Path
        {
            Aspect = Stretch.Uniform,
            Data = (Geometry?)new PathGeometryConverter().ConvertFromInvariantString(shape),
            HeightRequest = itemShapeSize,
            Stroke = shapeBorderColor,
            StrokeLineCap = PenLineCap.Round,
            StrokeLineJoin = PenLineJoin.Round,
            StrokeThickness = shapeBorderThickness,
            WidthRequest = itemShapeSize,
        }
    };

    static ReadOnlyCollection<IView> GetVisualTreeDescendantsWithBorderAndShape(VisualElement root, bool isShapeFill)
    {
        var result = new List<IView>();

        // Assuming the first child is the HorizontalStackLayout
        var stackLayout = root.GetVisualTreeDescendants().OfType<HorizontalStackLayout>().FirstOrDefault();
        if (stackLayout == null)
            return result.AsReadOnly();

        foreach (var child in stackLayout.Children)
        {
            if (isShapeFill)
            {
                // If using a Border, grab its Content; otherwise use the element itself
                if (child is Border border && border.Content is IView content)
                {
                    result.Add(content);
                }
                else
                {
                    result.Add(child); // now works because result is List<IView>
                }
            }
            else
            {
                // Return the element itself for background/fill handling
                result.Add(child);
            }
        }

        return result.AsReadOnly();
    }



    static bool IsMaximumRatingValid(BindableObject bindable, object value)
    {
        return (int)value is >= 1 and <= 10;
    }

    static void OnIsReadOnlyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ratingView = (RatingView)bindable;
        foreach (var child in ratingView.RatingLayout.Children.Cast<Border>())
        {
            if (!ratingView.IsReadOnly)
            {
                TapGestureRecognizer tapGestureRecognizer = new();
                tapGestureRecognizer.Tapped += ratingView.OnShapeTapped;
                child.GestureRecognizers.Add(tapGestureRecognizer);
                continue;
            }

            child.GestureRecognizers.Clear();
        }
    }

    static void OnMaximumRatingChange(BindableObject bindable, object oldValue, object newValue)
    {
        var ratingView = (RatingView)bindable;
        var layout = ratingView.RatingLayout;
        var newMaximumRatingValue = (int)newValue;
        var oldMaximumRatingValue = (int)oldValue;
        if (newMaximumRatingValue < oldMaximumRatingValue)
        {
            for (var lastElement = layout.Count - 1; lastElement >= newMaximumRatingValue; lastElement--)
            {
                layout.RemoveAt(lastElement);
            }

            ratingView.UpdateShapeFills(ratingView.FillOption);
        }
        else if (newMaximumRatingValue > oldMaximumRatingValue)
        {
            ratingView.AddChildrenToLayout(oldMaximumRatingValue - 1, newMaximumRatingValue - 1);
        }

        if (newMaximumRatingValue < ratingView.Rating) // Ensure Rating is never greater than MaximumRating 
        {
            ratingView.Rating = newMaximumRatingValue;
        }
    }

    static void OnRatingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ratingView = (RatingView)bindable;
        var newRating = (double)newValue;

        ratingView.UpdateShapeFills(ratingView.FillOption);
        ratingView.OnRatingChangedEvent(new RatingChangedEventArgs(newRating));
    }

    static void OnSpacingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ratingView = (RatingView)bindable;
        ratingView.RatingLayout.Spacing = (double)newValue;
    }

    static void OnRatingColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ratingView = (RatingView)bindable;
        ratingView.UpdateShapeFills(ratingView.FillOption);
    }

    // New: when ItemTemplate or layout-related properties change, rebuild children
    static void OnRatingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RatingView rv)
        {
            // Rebuild entire layout (safe, preserves gestures)
            rv.RatingLayout.Children.Clear();
            rv.AddChildrenToLayout(0, rv.MaximumRating);
        }
    }

    static LinearGradientBrush GetPartialFillBrush(Color filledColor, double partialFill, Color emptyColor)
    {
        partialFill = Math.Clamp(partialFill, 0.0, 1.0);

        var stops = new GradientStopCollection
            {
                new GradientStop(filledColor, 0f),
                new GradientStop(filledColor, (float)partialFill),
                new GradientStop(emptyColor, (float)partialFill),
            };

        return new LinearGradientBrush(stops, new Point(0, 0), new Point(1, 0));
    }


    static bool IsRatingValid(BindableObject bindable, object value)
    {
        return (double)value is >= 0.0 and <= 10;
    }

    static void OnCustomShapePathPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ratingView = (RatingView)bindable;
        var newShape = (string)newValue;

        if (ratingView.Shape is not RatingViewShape.Custom)
        {
            return;
        }

        string newShapePathData;
        if (string.IsNullOrEmpty(newShape))
        {
            ratingView.Shape = RatingViewShape.Star;
            newShapePathData = PathShapes.Star;
        }
        else
        {
            newShapePathData = newShape;
        }

        ratingView.ChangeShape(newShapePathData);
    }

    static void OnShapePaddingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ratingView = (RatingView)bindable;
        for (var element = 0; element < ratingView.RatingLayout.Count; element++)
        {
            ((Border)ratingView.RatingLayout.Children[element]).Padding = (Thickness)newValue;
        }
    }

    static void OnShapeBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ratingView = (RatingView)bindable;
        for (var element = 0; element < ratingView.RatingLayout.Count; element++)
        {
            var border = (Border)ratingView.RatingLayout.Children[element];
            if (border.Content is not null)
            {
                ((Path)border.Content.GetVisualTreeDescendants()[0]).Stroke = (Color)newValue;
            }
        }
    }

    static void OnShapeBorderThicknessChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ratingView = (RatingView)bindable;
        for (var element = 0; element < ratingView.RatingLayout.Count; element++)
        {
            var border = (Border)ratingView.RatingLayout.Children[element];
            if (border.Content is not null)
            {
                ((Path)border.Content.GetVisualTreeDescendants()[0]).StrokeThickness = (double)newValue;
            }
        }
    }

    static void OnShapePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ratingView = (RatingView)bindable;
        ratingView.ChangeShape(ratingView.GetShapePathData((RatingViewShape)newValue));
    }

    static void OnShapeDiameterSizeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ratingView = (RatingView)bindable;
        for (var element = 0; element < ratingView.RatingLayout.Count; element++)
        {
            var border = (Border)ratingView.RatingLayout.Children[element];
            if (border.Content is null)
            {
                continue;
            }

            var rating = (Path)border.Content.GetVisualTreeDescendants()[0];
            rating.WidthRequest = (double)newValue;
            rating.HeightRequest = (double)newValue;
        }
    }

    // Modified: AddChildrenToLayout uses ItemTemplate if present
    void AddChildrenToLayout(int minimumRating, int maximumRating)
    {
        RatingLayout.Spacing = Spacing;
        var shape = GetShapePathData(Shape);

        for (var i = minimumRating; i < maximumRating; i++)
        {
            Border child;

            if (ItemTemplate is not null)
            {
                // Create template content and wrap in Border so layout and gestures remain consistent
                var content = ItemTemplate.CreateContent();
                View contentView = null;

                // DataTemplate.CreateContent returns either View or a Cell (rare). We'll try to cast.
                if (content is View v) contentView = v;
                else if (content is ViewCell vc) contentView = vc.View;
                else
                {
                    // fallback: wrap unrecognized content into a ContentView
                    contentView = new ContentView { Content = content as View };
                }

                child = new Border
                {
                    BackgroundColor = BackgroundColor,
                    Margin = 0,
                    Padding = ShapePadding,
                    Stroke = new SolidColorBrush(Colors.Transparent),
                    StrokeThickness = 0,
                    Content = contentView,
                    WidthRequest = ShapeDiameter,
                    HeightRequest = ShapeDiameter
                };
            }
            else
            {
                // Default behavior: create border + path
                child = CreateChild(shape, ShapePadding, ShapeBorderThickness, ShapeDiameter, new SolidColorBrush(ShapeBorderColor), BackgroundColor);
            }

            if (!IsReadOnly)
            {
                TapGestureRecognizer tapGestureRecognizer = new();
                tapGestureRecognizer.Tapped += OnShapeTapped;
                child.GestureRecognizers.Add(tapGestureRecognizer);
            }

            RatingLayout.Children.Add(child);
        }

        UpdateShapeFills(FillOption);
    }

    void ChangeShape(string shape)
    {
        for (var element = 0; element < RatingLayout.Count; element++)
        {
            var border = (Border)RatingLayout.Children[element];
            if (border.Content is not null)
            {
                if (border.Content is Path p)
                {
                    p.Data = (Geometry?)new PathGeometryConverter().ConvertFromInvariantString(shape);
                }
                else
                {
                    // If template content is used, we won't change template internals here.
                    // Optionally: if template exposes a named Path, you could look it up and set Data.
                }
            }
        }
    }

    string GetShapePathData(RatingViewShape shape) => shape switch
    {
        RatingViewShape.Custom when CustomShapePath is null => throw new InvalidOperationException($"Unable to draw RatingViewShape.Custom because {nameof(CustomShapePath)} is null. Please provide an SVG Path to {nameof(CustomShapePath)}."),
        RatingViewShape.Custom => CustomShapePath,
        RatingViewShape.Circle => PathShapes.Circle,
        RatingViewShape.Dislike => PathShapes.Dislike,
        RatingViewShape.Heart => PathShapes.Heart,
        RatingViewShape.Like => PathShapes.Like,
        RatingViewShape.Star => PathShapes.Star,
        _ => throw new NotSupportedException($"{shape} is not yet supported")
    };

    void OnShapeTapped(object? sender, TappedEventArgs? e)
    {
        if (sender is not Border tappedShape)
        {
            return;
        }

        var tappedShapeIndex = RatingLayout.Children.IndexOf(tappedShape);

        Rating = MaximumRating > 1
            ? tappedShapeIndex + 1
            : GetRatingWhenMaximumRatingEqualsOne(Rating);
    }

    // Modified UpdateShapeFills: support template content that implements IRatingItem
    void UpdateShapeFills(RatingViewFillOption ratingViewFillOption)
    {
        var fullFillCount = (int)Math.Floor(Rating);
        var partialFill = Rating - fullFillCount;

        // Get all IView items in layout
        var items = GetVisualTreeDescendantsWithBorderAndShape((VisualElement)RatingLayout.GetVisualTreeDescendants()[0], ratingViewFillOption is RatingViewFillOption.Shape);

        for (int i = 0; i < items.Count; i++)
        {
            var view = items[i];
            double percent = Math.Clamp(Rating - i, 0, 1); // 1 = full, 0 = empty, fractional = partial

            // If the item implements IRatingItem, update its state
            if (view is IRatingItem ratingItem)
            {
                ratingItem.UpdateState(percent > 0, percent);
            }
            else if (view is Border border && border.Content is IRatingItem innerRatingItem)
            {
                innerRatingItem.UpdateState(percent > 0, percent);
            }
            else
            {
                // Optional: for backward compatibility with Shape/Path templates
                if (view is Shape shape && ratingViewFillOption is RatingViewFillOption.Shape)
                {
                    if (i < fullFillCount)
                        shape.Fill = FillColor;
                    else if (i == fullFillCount && partialFill > 0)
                        shape.Fill = GetPartialFillBrush(FillColor, partialFill, EmptyShapeColor);
                    else
                        shape.Fill = EmptyShapeColor;
                }
                else if (view is Border b)
                {
                    if (ratingViewFillOption is RatingViewFillOption.Background)
                    {
                        if (i < fullFillCount)
                            b.Background = new SolidColorBrush(FillColor);
                        else if (i == fullFillCount && partialFill > 0)
                            b.Background = GetPartialFillBrush(FillColor, partialFill, BackgroundColor);
                        else
                            b.Background = new SolidColorBrush(BackgroundColor);
                    }
                }
            }
        }
    }


    void OnRatingChangedEvent(RatingChangedEventArgs e) => weakEventManager.HandleEvent(this, e, nameof(RatingChanged));
}

// -----------------------------------------------------------------------------
// Small interface templates can implement to receive updates from RatingView.
// Place this in the same file or a separate file (MauiApp9.Controls) as you prefer.
// -----------------------------------------------------------------------------
public interface IRatingItem
{
    /// <summary>
    /// Called when the control needs to update the visual state of the item.
    /// </summary>
    /// <param name="isFilled">True if the item is at least partially filled.</param>
    /// <param name="fillPercent">Value between 0 and 1 representing how much the item is filled.</param>
    void UpdateState(bool isFilled, double fillPercent);
}
