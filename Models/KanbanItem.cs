using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Shaunebu.Controls.Models
{
    public partial class KanbanItem : ObservableObject
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public KanbanStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the color of the category.
        /// </summary>
        /// <value>
        /// The color of the category.
        /// </value>

        public Color CategoryColor { get; set; }
        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// The tags
        /// </summary>
        public ObservableCollection<string> Tags = new();
    }
}
