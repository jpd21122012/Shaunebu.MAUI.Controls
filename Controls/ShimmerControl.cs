using Shaunebu.Controls.Enums;

namespace Shaunebu.Controls.Controls;

public class ShimmerControl : GraphicsView, IDrawable
{
    /// <summary>
    /// The is active property
    /// </summary>
    public static readonly BindableProperty IsActiveProperty =
        BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(ShimmerControl), true, propertyChanged: OnIsActiveChanged);

    /// <summary>
    /// The animation direction property
    /// </summary>
    public static readonly BindableProperty AnimationDirectionProperty =
        BindableProperty.Create(nameof(AnimationDirection), typeof(ShimmerDirection), typeof(ShimmerControl), ShimmerDirection.LeftToRight, propertyChanged: OnPropertyChangedInvalidate);

    /// <summary>
    /// The shimmer speed property
    /// </summary>
    public static readonly BindableProperty ShimmerSpeedProperty =
        BindableProperty.Create(nameof(ShimmerSpeed), typeof(int), typeof(ShimmerControl), 1000, propertyChanged: OnPropertyChangedInvalidate);

    /// <summary>
    /// The shimmer width property
    /// </summary>
    public static readonly BindableProperty ShimmerWidthProperty =
        BindableProperty.Create(nameof(ShimmerWidth), typeof(double), typeof(ShimmerControl), 0.3, propertyChanged: OnPropertyChangedInvalidate);

    /// <summary>
    /// The shimmer color property
    /// </summary>
    public static readonly BindableProperty ShimmerColorProperty =
        BindableProperty.Create(nameof(ShimmerColor), typeof(Color), typeof(ShimmerControl), Colors.White.WithAlpha(0.5f), propertyChanged: OnPropertyChangedInvalidate);

    /// <summary>
    /// The base color property
    /// </summary>
    public static readonly BindableProperty BaseColorProperty =
        BindableProperty.Create(nameof(BaseColor), typeof(Color), typeof(ShimmerControl), Colors.Gray.WithAlpha(0.2f), propertyChanged: OnPropertyChangedInvalidate);
    /// <summary>
    /// The shimmer position
    /// </summary>
    private double _shimmerPosition = -100;

    /// <summary>
    /// The timer
    /// </summary>
    private IDispatcherTimer _timer;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShimmerControl"/> class.
    /// </summary>
    public ShimmerControl()
    {
        Drawable = this;
        _timer = Application.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(16);
        _timer.Tick += OnTimerTick;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is active.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
    /// </value>
    public bool IsActive { get => (bool)GetValue(IsActiveProperty); set => SetValue(IsActiveProperty, value); }

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
    /// Draws the content onto the specified canvas within the given rectangle.
    /// </summary>
    /// <param name="canvas">The canvas to draw onto.</param>
    /// <param name="dirtyRect">The rectangle in which to draw.</param>
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (!IsActive) return;

        // Draw base
        canvas.FillColor = BaseColor;
        canvas.FillRectangle(dirtyRect);

        // Calculate gradient
        double width = dirtyRect.Width;
        double height = dirtyRect.Height;
        double band = ShimmerWidth <= 1 ? width * ShimmerWidth : ShimmerWidth;

        var start = _shimmerPosition;
        var end = _shimmerPosition + band;

        Point startPoint = new Point(0, 0);
        Point endPoint = new Point(band, 0);

        switch (AnimationDirection)
        {
            case ShimmerDirection.LeftToRight: startPoint = new Point(start, 0); endPoint = new Point(end, 0); break;
            case ShimmerDirection.RightToLeft: startPoint = new Point(width - start, 0); endPoint = new Point(width - end, 0); break;
            case ShimmerDirection.TopToBottom: startPoint = new Point(0, start); endPoint = new Point(0, end); break;
            case ShimmerDirection.BottomToTop: startPoint = new Point(0, height - start); endPoint = new Point(0, height - end); break;
        }

        var gradient = new LinearGradientBrush(
            new GradientStopCollection
            {
                new GradientStop(Colors.Transparent,0),
                new GradientStop(ShimmerColor,0.5f),
                new GradientStop(Colors.Transparent,1)
            },
            startPoint, endPoint
        );

        canvas.SetFillPaint(gradient, dirtyRect);
        canvas.FillRectangle(dirtyRect);
    }

    /// <summary>
    /// Called when [timer tick].
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void OnTimerTick(object sender, EventArgs e)
    {
        if (!IsActive) return;

        _shimmerPosition += ShimmerSpeed / 60.0;

        if (_shimmerPosition > Width + Height + 200) _shimmerPosition = -200;

        Invalidate();
    }

    /// <summary>
    /// Called when [is active changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnIsActiveChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ShimmerControl shimmer)
        {
            if ((bool)newValue) shimmer.StartShimmer(); else shimmer.StopShimmer();
        }
    }

    /// <summary>
    /// Called when [property changed invalidate].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnPropertyChangedInvalidate(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ShimmerControl shimmer) shimmer.Invalidate();
    }
    /// <summary>
    /// Starts the shimmer.
    /// </summary>
    public void StartShimmer() => _timer.Start();

    /// <summary>
    /// Stops the shimmer.
    /// </summary>
    public void StopShimmer() => _timer.Stop();
}