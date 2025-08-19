using Microsoft.Maui.Layouts;
using Shaunebu.Controls.Enums;

namespace Shaunebu.Controls.Controls;

public class DockLayout : Layout
{
    #region Fields    
    /// <summary>
    /// Gets or sets a value indicating whether [last child fill].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [last child fill]; otherwise, <c>false</c>.
    /// </value>
    public bool LastChildFill
    {
        get => (bool)GetValue(LastChildFillProperty);
        set => SetValue(LastChildFillProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing.
    /// </summary>
    /// <value>
    /// The spacing.
    /// </value>
    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether [animate resize].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [animate resize]; otherwise, <c>false</c>.
    /// </value>
    public bool AnimateResize
    {
        get => (bool)GetValue(AnimateResizeProperty);
        set => SetValue(AnimateResizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the duration of the animation.
    /// </summary>
    /// <value>
    /// The duration of the animation.
    /// </value>
    public uint AnimationDuration
    {
        get => (uint)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }
    #endregion

    #region Properties    
    /// <summary>
    /// The dock property
    /// </summary>
    public static readonly BindableProperty DockProperty =
       BindableProperty.CreateAttached(
           "Dock",
           typeof(DockPosition),
           typeof(DockLayout),
           DockPosition.Left,
           propertyChanged: OnDockChanged);

    /// <summary>
    /// The last child fill property
    /// </summary>
    public static readonly BindableProperty LastChildFillProperty =
       BindableProperty.Create(
           nameof(LastChildFill),
           typeof(bool),
           typeof(DockLayout),
           true,
           propertyChanged: OnLayoutPropertyChanged);

    /// <summary>
    /// The spacing property
    /// </summary>
    public static readonly BindableProperty SpacingProperty =
       BindableProperty.Create(
           nameof(Spacing),
           typeof(double),
           typeof(DockLayout),
           0.0,
           propertyChanged: OnLayoutPropertyChanged);

    /// <summary>
    /// The dock priority property
    /// </summary>
    public static readonly BindableProperty DockPriorityProperty =
       BindableProperty.CreateAttached(
           "DockPriority",
           typeof(int),
           typeof(DockLayout),
           0,
           propertyChanged: OnDockChanged);

    /// <summary>
    /// The minimum dock size property
    /// </summary>
    public static readonly BindableProperty MinDockSizeProperty =
      BindableProperty.CreateAttached(
          "MinDockSize",
          typeof(Size),
          typeof(DockLayout),
          new Size(-1, -1));

    /// <summary>
    /// The maximum dock size property
    /// </summary>
    public static readonly BindableProperty MaxDockSizeProperty =
        BindableProperty.CreateAttached(
            "MaxDockSize",
            typeof(Size),
            typeof(DockLayout),
            new Size(-1, -1));

    /// <summary>
    /// The animate resize property
    /// </summary>
    public static readonly BindableProperty AnimateResizeProperty =
        BindableProperty.Create(
            nameof(AnimateResize),
            typeof(bool),
            typeof(DockLayout),
            true);

    /// <summary>
    /// The animation duration property
    /// </summary>
    public static readonly BindableProperty AnimationDurationProperty =
        BindableProperty.Create(
            nameof(AnimationDuration),
            typeof(uint),
            typeof(DockLayout),
            250u);
    #endregion

    #region Methods    
    /// <summary>
    /// Gets the dock.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    public static DockPosition GetDock(BindableObject view) =>
    (DockPosition)view.GetValue(DockProperty);

    /// <summary>
    /// Sets the dock.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="value">The value.</param>
    public static void SetDock(BindableObject view, DockPosition value) =>
        view.SetValue(DockProperty, value);

    /// <summary>
    /// Gets the dock priority.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    public static int GetDockPriority(BindableObject view) =>
        (int)view.GetValue(DockPriorityProperty);

    /// <summary>
    /// Sets the dock priority.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="value">The value.</param>
    public static void SetDockPriority(BindableObject view, int value) =>
        view.SetValue(DockPriorityProperty, value);

    /// <summary>
    /// Gets the maximum size of the dock.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    public static Size GetMaxDockSize(BindableObject view) =>
      (Size)view.GetValue(MaxDockSizeProperty);

    /// <summary>
    /// Sets the maximum size of the dock.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="value">The value.</param>
    public static void SetMaxDockSize(BindableObject view, Size value) =>
        view.SetValue(MaxDockSizeProperty, value);

    /// <summary>
    /// Gets the minimum size of the dock.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    public static Size GetMinDockSize(BindableObject view) =>
        (Size)view.GetValue(MinDockSizeProperty);

    /// <summary>
    /// Sets the minimum size of the dock.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="value">The value.</param>
    public static void SetMinDockSize(BindableObject view, Size value) =>
        view.SetValue(MinDockSizeProperty, value);

    /// <summary>
    /// Called when [layout property changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnLayoutPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is DockLayout layout)
        {
            layout.InvalidateMeasure();
        }
    }

    /// <summary>
    /// Called when [dock changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnDockChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is IView view && view.Handler?.PlatformView != null)
        {
            view.Handler?.UpdateValue(nameof(IView.Frame));
        }
    }

    /// <summary>
    /// Creates a manager object that can measure this layout and arrange its children.
    /// </summary>
    /// <returns>
    /// An object that implements <see cref="T:Microsoft.Maui.Layouts.ILayoutManager" /> that manages this layout.
    /// </returns>
    protected override ILayoutManager CreateLayoutManager() =>
        new DockLayoutManager(this);
    #endregion
}
