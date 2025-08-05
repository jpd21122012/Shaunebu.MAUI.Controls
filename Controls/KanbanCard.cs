using Shaunebu.Controls.Extensions;
using Shaunebu.Controls.Models;
using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;

namespace Shaunebu.Controls.Controls
{
    public class KanbanCard : Border
    {
        #region Fields
        /// <summary>
        /// The is dragging
        /// </summary>
        private bool _isDragging;

        /// <summary>
        /// The drag start position
        /// </summary>
        private Point _dragStartPosition;
        #endregion


        #region Properties
        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="KanbanCard"/> class.
        /// </summary>
        /// <remarks>
        /// A Frame has a default <see cref="P:Microsoft.Maui.Controls.Layout.Padding" /> of 20.
        /// </remarks>
        public KanbanCard()
        {
            this.BackgroundColor = Colors.White;
            this.Padding = new Thickness(15);
            this.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(8) };
            this.Stroke= Colors.LightGray;

            Debug.WriteLine($"Creating card with BC: {BindingContext}");

            var titleLabel = new Label { FontAttributes = FontAttributes.Bold };
            titleLabel.SetBinding(Label.TextProperty, nameof(KanbanItem.Title));

            var dueLabel = new Label { FontSize = 12, TextColor = Colors.Gray };
            dueLabel.SetBinding(Label.TextProperty,
                new Binding(nameof(KanbanItem.DueDate), stringFormat: "Due: {0:MMM dd}"));

            Content = new VerticalStackLayout
            {
                Children = { titleLabel, dueLabel },
                Padding = new Thickness(5)
            };

            InitializeDragGestures();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the drag gestures.
        /// </summary>
        private void InitializeDragGestures()
        {
            var dragGesture = new DragGestureRecognizer { CanDrag = true };
            dragGesture.DragStarting += (s, e) =>
            {
                if (BindingContext is KanbanItem item)
                {
                    Debug.WriteLine($"Start position: {_dragStartPosition}");
                    _isDragging = true;
                    _dragStartPosition = new Point(this.TranslationX, this.TranslationY);

                    e.Data.Properties["KanbanItem"] = item;
                    e.Data.Properties["SourceColumn"] = this.FindParent<KanbanColumn>();

                    this.ScaleTo(1.05, 100);
                    this.FadeTo(0.8, 100);
                    this.ZIndex = 1;

                    Debug.WriteLine($"Started dragging {item.Title}");
                }
            };

            dragGesture.DropCompleted += (s, e) =>
            {
                Debug.WriteLine($"Current translation: ({TranslationX}, {TranslationY})");
                _isDragging = false;
                this.ScaleTo(1.0, 100);
                this.FadeTo(1.0, 100);
                this.ZIndex = 0;
            };

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += (s, e) =>
            {
                if (!_isDragging) return;

                switch (e.StatusType)
                {
                    case GestureStatus.Running:
                        this.TranslationX = _dragStartPosition.X + e.TotalX;
                        this.TranslationY = _dragStartPosition.Y + e.TotalY;
                        break;

                    case GestureStatus.Completed:
                    case GestureStatus.Canceled:
                        _isDragging = false;
                        this.TranslateTo(0, 0, 250, Easing.SpringOut);
                        break;
                }
            };

            GestureRecognizers.Add(dragGesture);
            GestureRecognizers.Add(panGesture);
        }
        #endregion
    }
}
