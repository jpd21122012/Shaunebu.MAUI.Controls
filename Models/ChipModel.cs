namespace Shaunebu.Controls.Models
{
    public class ChipModel
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; } = "";

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the badge text.
        /// </summary>
        /// <value>
        /// The badge text.
        /// </value>
        public string BadgeText { get; set; }

        /// <summary>
        /// Gets or sets the color of the badge.
        /// </summary>
        /// <value>
        /// The color of the badge.
        /// </value>
        public Color BadgeColor { get; set; } = Colors.LightCoral;

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        public Color BackgroundColor { get; set; } = Colors.LightGray;

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>
        /// The color of the text.
        /// </value>
        public Color TextColor { get; set; } = Colors.Black;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is closable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is closable; otherwise, <c>false</c>.
        /// </value>
        public bool IsClosable { get; set; } = false;

        /// <summary>
        /// Gets or sets the width request.
        /// </summary>
        /// <value>
        /// The width request.
        /// </value>
        public double WidthRequest { get; set; } = -1;
    }
}
