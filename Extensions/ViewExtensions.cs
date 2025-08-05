namespace Shaunebu.Controls.Extensions;

public static class ViewExtensions
{
    /// <summary>
    /// Gets the absolute position.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    public static Point GetAbsolutePosition(this View view)
    {
        var position = new Point(view.X, view.Y);
        var parent = view.Parent as View;

        while (parent != null)
        {
            position.X += parent.X;
            position.Y += parent.Y;
            parent = parent.Parent as View;
        }

        return position;
    }

    /// <summary>
    /// Forces the layout update.
    /// </summary>
    /// <param name="layout">The layout.</param>
    public static void ForceLayoutUpdate(this Layout layout)
    {
        layout.InvalidateMeasure();
        foreach (var child in layout.Children)
        {
            if (child is View view)
            {
                view.InvalidateMeasure();
            }
        }
    }

    /// <summary>
    /// Finds the parent.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    public static T FindParent<T>(this View view) where T : View
    {
        var parent = view.Parent;
        while (parent != null)
        {
            if (parent is T matchedParent)
                return matchedParent;
            parent = parent.Parent;
        }
        return null;
    }

    /// <summary>
    /// Requests the layout update.
    /// </summary>
    /// <param name="view">The view.</param>
    public static void RequestLayoutUpdate(this View view)
    {
        view.InvalidateMeasure();

        // For Layouts, update all children
        if (view is Layout layout)
        {
            foreach (var child in layout.Children)
            {
                child.InvalidateMeasure();
            }
        }
    }

    /// <summary>
    /// Resizes to.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="length">The length.</param>
    /// <param name="easing">The easing.</param>
    /// <returns></returns>
    public static Task<bool> ResizeTo(this VisualElement view, double width, double height, uint length = 250, Easing easing = null)
    {
        var tcs = new TaskCompletionSource<bool>();

        var widthAnimation = new Animation(v => view.WidthRequest = v, view.Width, width, easing);
        var heightAnimation = new Animation(v => view.HeightRequest = v, view.Height, height, easing);

        new Animation {
            { 0, 1, widthAnimation },
            { 0, 1, heightAnimation }
        }.Commit(view, "Resize", length, finished: (v, c) => tcs.SetResult(c));

        return tcs.Task;
    }
}
