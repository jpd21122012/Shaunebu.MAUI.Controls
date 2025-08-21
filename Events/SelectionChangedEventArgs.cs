using System.Collections;

namespace Shaunebu.Controls.Events;

public class SelectionChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the old selection.
    /// </summary>
    /// <value>
    /// The old selection.
    /// </value>
    public IList OldSelection { get; }

    /// <summary>
    /// Creates new selection.
    /// </summary>
    /// <value>
    /// The new selection.
    /// </value>
    public IList NewSelection { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionChangedEventArgs"/> class.
    /// </summary>
    /// <param name="newSelection">The new selection.</param>
    public SelectionChangedEventArgs(IList newSelection)
    {
        OldSelection = new List<object>();
        NewSelection = newSelection;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionChangedEventArgs"/> class.
    /// </summary>
    /// <param name="oldSelection">The old selection.</param>
    /// <param name="newSelection">The new selection.</param>
    public SelectionChangedEventArgs(IList oldSelection, IList newSelection)
    {
        OldSelection = oldSelection;
        NewSelection = newSelection;
    }
}
