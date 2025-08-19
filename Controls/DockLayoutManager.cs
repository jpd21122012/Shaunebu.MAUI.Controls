using Microsoft.Maui.Layouts;
using Shaunebu.Controls.Enums;

namespace Shaunebu.Controls.Controls;

public class DockLayoutManager : LayoutManager
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DockLayoutManager"/> class.
    /// </summary>
    /// <param name="layout">The layout.</param>
    public DockLayoutManager(DockLayout layout) : base(layout) { }

    /// <summary>
    /// Measures the specified width constraint.
    /// </summary>
    /// <param name="widthConstraint">The width constraint.</param>
    /// <param name="heightConstraint">The height constraint.</param>
    /// <returns></returns>
    public override Size Measure(double widthConstraint, double heightConstraint)
    {
        var layout = (DockLayout)Layout;
        double width = 0;
        double height = 0;
        double remainingWidth = widthConstraint;
        double remainingHeight = heightConstraint;

        // Get children ordered by priority then by their index
        var orderedChildren = GetOrderedChildren(layout);

        foreach (var child in orderedChildren)
        {
            if (child.Visibility == Visibility.Collapsed)
                continue;

            var dockPosition = GetDockForView(child);
            Size measuredSize;

            // Handle nested DockLayouts differently
            if (child is DockLayout nestedDock)
            {
                measuredSize = MeasureNestedDockLayout(nestedDock, dockPosition, ref remainingWidth, ref remainingHeight);
            }
            else
            {
                measuredSize = MeasureRegularChild(child, dockPosition, ref remainingWidth, ref remainingHeight);
            }

            // Update total dimensions
            if (dockPosition is DockPosition.Left or DockPosition.Right)
            {
                width += measuredSize.Width + layout.Spacing;
                height = Math.Max(height, measuredSize.Height);
            }
            else if (dockPosition is DockPosition.Top or DockPosition.Bottom)
            {
                height += measuredSize.Height + layout.Spacing;
                width = Math.Max(width, measuredSize.Width);
            }
        }

        // Measure fill child last
        var fillChild = GetFillChild(layout);
        if (fillChild != null && fillChild.Visibility != Visibility.Collapsed)
        {
            var fillSize = fillChild.Measure(remainingWidth, remainingHeight);
            width = Math.Max(width, remainingWidth);
            height = Math.Max(height, remainingHeight);
        }

        return new Size(width, height);
    }

    /// <summary>
    /// Arranges the children.
    /// </summary>
    /// <param name="bounds">The bounds.</param>
    /// <returns></returns>
    public override Size ArrangeChildren(Rect bounds)
    {
        var layout = (DockLayout)Layout;
        double left = bounds.Left;
        double top = bounds.Top;
        double right = bounds.Right;
        double bottom = bounds.Bottom;

        // Get children ordered by priority then by their index
        var orderedChildren = GetOrderedChildren(layout);

        // First arrange non-fill children
        foreach (var child in orderedChildren.Where(c => GetDockForView(c) != DockPosition.Fill))
        {
            if (child.Visibility == Visibility.Collapsed)
                continue;

            var dockPosition = GetDockForView(child);
            var childSize = child.DesiredSize;

            // Apply size constraints
            childSize = ApplySizeConstraints(child, childSize);

            // Arrange the child
            var newBounds = CalculateChildBounds(dockPosition, childSize, ref left, ref top, ref right, ref bottom);

            // Use animation if enabled
            if (layout.AnimateResize)
            {
                AnimateChildResize(child, newBounds).ConfigureAwait(false);
            }
            else
            {
                child.Arrange(newBounds);
            }
        }

        // Then arrange fill child
        var fillChild = GetFillChild(layout);
        if (fillChild != null && fillChild.Visibility != Visibility.Collapsed)
        {
            var fillBounds = new Rect(left, top, Math.Max(0, right - left), Math.Max(0, bottom - top));

            if (layout.AnimateResize)
            {
                AnimateChildResize(fillChild, fillBounds).ConfigureAwait(false);
            }
            else
            {
                fillChild.Arrange(fillBounds);
            }
        }

        return new Size(right - bounds.Left, bottom - bounds.Top);
    }

    #region Helper Methods

    /// <summary>
    /// Gets the ordered children.
    /// </summary>
    /// <param name="layout">The layout.</param>
    /// <returns></returns>
    private IOrderedEnumerable<IView> GetOrderedChildren(DockLayout layout)
    {
        return layout.OrderBy(c =>
        {
            if (c is BindableObject bo)
                return DockLayout.GetDockPriority(bo);
            return 0;
        }).ThenBy(layout.IndexOf);
    }

    /// <summary>
    /// Gets the fill child.
    /// </summary>
    /// <param name="layout">The layout.</param>
    /// <returns></returns>
    private IView GetFillChild(DockLayout layout)
    {
        return layout.LastChildFill
            ? layout.LastOrDefault(c => GetDockForView(c) == DockPosition.Fill ||
                                       (layout.IndexOf(c) == layout.Count - 1 && GetDockForView(c) != DockPosition.Fill))
            : layout.FirstOrDefault(c => GetDockForView(c) == DockPosition.Fill);
    }

    /// <summary>
    /// Measures the nested dock layout.
    /// </summary>
    /// <param name="nestedDock">The nested dock.</param>
    /// <param name="dockPosition">The dock position.</param>
    /// <param name="remainingWidth">Width of the remaining.</param>
    /// <param name="remainingHeight">Height of the remaining.</param>
    /// <returns></returns>
    private Size MeasureNestedDockLayout(DockLayout nestedDock, DockPosition dockPosition,
        ref double remainingWidth, ref double remainingHeight)
    {
        var measuredSize = nestedDock.Measure(
            dockPosition is DockPosition.Left or DockPosition.Right ? double.PositiveInfinity : remainingWidth,
            dockPosition is DockPosition.Top or DockPosition.Bottom ? double.PositiveInfinity : remainingHeight);

        // Apply spacing
        if (dockPosition is DockPosition.Left or DockPosition.Right)
        {
            remainingWidth = Math.Max(0, remainingWidth - measuredSize.Width - ((DockLayout)Layout).Spacing);
        }
        else if (dockPosition is DockPosition.Top or DockPosition.Bottom)
        {
            remainingHeight = Math.Max(0, remainingHeight - measuredSize.Height - ((DockLayout)Layout).Spacing);
        }

        return measuredSize;
    }

    /// <summary>
    /// Measures the regular child.
    /// </summary>
    /// <param name="child">The child.</param>
    /// <param name="dockPosition">The dock position.</param>
    /// <param name="remainingWidth">Width of the remaining.</param>
    /// <param name="remainingHeight">Height of the remaining.</param>
    /// <returns></returns>
    private Size MeasureRegularChild(IView child, DockPosition dockPosition,
        ref double remainingWidth, ref double remainingHeight)
    {
        var measuredSize = child.Measure(
            dockPosition is DockPosition.Left or DockPosition.Right ? double.PositiveInfinity : remainingWidth,
            dockPosition is DockPosition.Top or DockPosition.Bottom ? double.PositiveInfinity : remainingHeight);

        // Apply size constraints
        measuredSize = ApplySizeConstraints(child, measuredSize);

        // Apply spacing
        if (dockPosition is DockPosition.Left or DockPosition.Right)
        {
            remainingWidth = Math.Max(0, remainingWidth - measuredSize.Width - ((DockLayout)Layout).Spacing);
        }
        else if (dockPosition is DockPosition.Top or DockPosition.Bottom)
        {
            remainingHeight = Math.Max(0, remainingHeight - measuredSize.Height - ((DockLayout)Layout).Spacing);
        }

        return measuredSize;
    }

    /// <summary>
    /// Applies the size constraints.
    /// </summary>
    /// <param name="child">The child.</param>
    /// <param name="measuredSize">Size of the measured.</param>
    /// <returns></returns>
    private Size ApplySizeConstraints(IView child, Size measuredSize)
    {
        if (child is BindableObject bindable)
        {
            var minSize = DockLayout.GetMinDockSize(bindable);
            var maxSize = DockLayout.GetMaxDockSize(bindable);

            var width = measuredSize.Width;
            var height = measuredSize.Height;

            if (minSize.Width >= 0) width = Math.Max(width, minSize.Width);
            if (minSize.Height >= 0) height = Math.Max(height, minSize.Height);
            if (maxSize.Width >= 0) width = Math.Min(width, maxSize.Width);
            if (maxSize.Height >= 0) height = Math.Min(height, maxSize.Height);

            return new Size(width, height);
        }
        return measuredSize;
    }

    /// <summary>
    /// Calculates the child bounds.
    /// </summary>
    /// <param name="dockPosition">The dock position.</param>
    /// <param name="childSize">Size of the child.</param>
    /// <param name="left">The left.</param>
    /// <param name="top">The top.</param>
    /// <param name="right">The right.</param>
    /// <param name="bottom">The bottom.</param>
    /// <returns></returns>
    private Rect CalculateChildBounds(DockPosition dockPosition, Size childSize,
        ref double left, ref double top, ref double right, ref double bottom)
    {
        var layout = (DockLayout)Layout;

        return dockPosition switch
        {
            DockPosition.Left => new Rect(left, top, childSize.Width, bottom - top),
            DockPosition.Top => new Rect(left, top, right - left, childSize.Height),
            DockPosition.Right => new Rect(right - childSize.Width, top, childSize.Width, bottom - top),
            DockPosition.Bottom => new Rect(left, bottom - childSize.Height, right - left, childSize.Height),
            _ => new Rect(left, top, right - left, bottom - top)
        };
    }

    /// <summary>
    /// Animates the child resize.
    /// </summary>
    /// <param name="child">The child.</param>
    /// <param name="newBounds">The new bounds.</param>
    private async Task AnimateChildResize(IView child, Rect newBounds)
    {
        var layout = (DockLayout)Layout;
        if (!layout.AnimateResize)
        {
            child.Arrange(newBounds);
            return;
        }

        var tcs = new TaskCompletionSource<bool>();

        // Check if child is IAnimatable (most MAUI views are)
        if (child is IAnimatable animatableChild)
        {
            var animation = new Animation(
                progress =>
                {
                    child.Arrange(newBounds);
                });

            animation.Commit(
                owner: animatableChild,
                name: "DockResizeAnimation",
                length: layout.AnimationDuration,
                finished: (v, c) => tcs.SetResult(true));
        }
        else
        {
            // Fallback for non-animatable views
            child.Arrange(newBounds);
            tcs.SetResult(true);
        }

        await tcs.Task;
    }

    /// <summary>
    /// Gets the dock for view.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    private DockPosition GetDockForView(IView view)
    {
        if (view is BindableObject bindable)
        {
            return DockLayout.GetDock(bindable);
        }
        return DockPosition.Left;
    }

    #endregion
}
