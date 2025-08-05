using Shaunebu.Controls.Models;
using Shaunebu.Controls.ViewModels;
using System.Diagnostics;

namespace Shaunebu.Controls.Controls
{
    public class KanbanColumn : VerticalStackLayout
    {
        #region Bindable Properties

        /// <summary>
        /// Identifies the bindable property for the <see cref="Status"/> property.
        /// </summary>
        /// <remarks>This property is used to define the <see cref="Status"/> property as a bindable
        /// property, enabling data binding and property change notifications.</remarks>
        public static readonly BindableProperty StatusProperty = BindableProperty.Create(nameof(Status), typeof(KanbanStatus), typeof(KanbanColumn),
                default(KanbanStatus), propertyChanged: OnStatusChanged);

        /// <summary>
        /// Identifies the bindable property for the collection of items displayed in the Kanban column.
        /// </summary>
        /// <remarks>This property is used to bind a collection of <see cref="KanbanItem"/> objects to the
        /// Kanban column. When the bound collection changes, the column updates its displayed items
        /// accordingly.</remarks>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<KanbanItem>),
                typeof(KanbanColumn), default(IEnumerable<KanbanItem>), propertyChanged: OnItemsSourceChanged);

        /// <summary>
        /// Identifies the <see cref="ItemTemplate"/> bindable property, which determines the template used to display
        /// items in the Kanban column.
        /// </summary>
        /// <remarks>This property is a bindable property, allowing it to be used in data binding
        /// scenarios.  The default value is <see langword="null"/>.</remarks>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate),
                typeof(KanbanColumn), default(DataTemplate), propertyChanged: OnItemTemplateChanged);

        /// <summary>
        /// The header template property
        /// </summary>
        public static readonly BindableProperty HeaderTemplateProperty = BindableProperty.Create(nameof(HeaderTemplate), typeof(DataTemplate),
           typeof(KanbanColumn), default(DataTemplate), propertyChanged: OnHeaderTemplateChanged);

        /// <summary>
        /// The drag over color property
        /// </summary>
        public static readonly BindableProperty DragOverColorProperty = BindableProperty.Create(nameof(DragOverColor), typeof(Color),
        typeof(KanbanColumn), Color.FromArgb("#3300FF00"));

        /// <summary>
        /// The drag leave color property
        /// </summary>
        public static readonly BindableProperty DragLeaveColorProperty = BindableProperty.Create(nameof(DragLeaveColor), typeof(Color),
          typeof(KanbanColumn), Color.FromArgb("#F0F0F0"));
        #endregion

        #region Public Properties             
        /// <summary>
        /// Gets or sets the color of the drag leave.
        /// </summary>
        /// <value>
        /// The color of the drag leave.
        /// </value>
        public Color DragLeaveColor
        {
            get => (Color)GetValue(DragLeaveColorProperty);
            set => SetValue(DragLeaveColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the drag over.
        /// </summary>
        /// <value>
        /// The color of the drag over.
        /// </value>
        public Color DragOverColor
        {
            get => (Color)GetValue(DragOverColorProperty);
            set => SetValue(DragOverColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the header template.
        /// </summary>
        /// <value>
        /// The header template.
        /// </value>
        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate)GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the current status of the Kanban item.
        /// </summary>
        public KanbanStatus Status
        {
            get => (KanbanStatus)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        /// <summary>
        /// Gets or sets the collection of <see cref="KanbanItem"/> objects to be displayed in the Kanban control.
        /// </summary>
        /// <remarks>Changes to this property will update the items displayed in the Kanban control.
        /// Ensure that the  collection implements proper change notification (e.g., <see
        /// cref="System.Collections.ObjectModel.ObservableCollection{T}"/>) if dynamic updates to the collection need
        /// to be reflected in the UI.</remarks>
        public IEnumerable<KanbanItem> ItemsSource
        {
            get => (IEnumerable<KanbanItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> used to display each item in the collection.
        /// </summary>
        /// <remarks>Use this property to specify a custom template for rendering items in the collection.
        /// If not set, a default template may be used depending on the control.</remarks>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Represents a container for vertically stacking items with a specified spacing.
        /// </summary>
        /// <remarks>This field is initialized with a default spacing of 8 units between items. It is used
        /// to organize child elements in a vertical layout.</remarks>
        private readonly VerticalStackLayout _itemsContainer = new() { Spacing = 8 };

        private View _headerView;
        private readonly ContentView _headerContainer = new ContentView();
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="KanbanColumn"/> class.
        /// </summary>
        /// <remarks>This constructor sets up the default appearance and layout of the Kanban column, 
        /// including spacing, background color, and padding. It also initializes the header label  and item container,
        /// and configures drag-and-drop functionality for the column.</remarks>
        public KanbanColumn()
        {

            Spacing = 10;
            BackgroundColor = Color.FromArgb("#F0F0F0");
            Padding = new Thickness(10);

            Children.Add(_headerContainer);
            Children.Add(_itemsContainer);

            SetupDragAndDrop();
        }

        #endregion

        #region Property Changed Handlers        
        /// <summary>
        /// Called when [header template changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnHeaderTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is KanbanColumn column)
            {
                column.UpdateHeader();
            }
        }

        /// <summary>
        /// Updates the header.
        /// </summary>
        private void UpdateHeader()
        {
            _headerContainer.Content = null;

            if (HeaderTemplate != null)
            {
                _headerView = (View)HeaderTemplate.CreateContent();
                if (_headerView is BindableObject bindable)
                {
                    bindable.BindingContext = Status;
                }
                _headerContainer.Content = _headerView;
            }
            else
            {
                // Default header if no template provided
                _headerContainer.Content = new Label
                {
                    Text = Status.ToString(),
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Colors.Red
                };
            }
        }

        /// <summary>
        /// Handles changes to the <see cref="KanbanColumn.Status"/> property.
        /// </summary>
        /// <remarks>This method updates the header label of the <see cref="KanbanColumn"/> to reflect the
        /// new status and logs the change for debugging purposes. The <paramref name="bindable"/> parameter must be a
        /// <see cref="KanbanColumn"/>, and the <paramref name="newValue"/> must be a <see
        /// cref="KanbanStatus"/>.</remarks>
        /// <param name="bindable">The <see cref="BindableObject"/> whose property has changed.</param>
        /// <param name="oldValue">The previous value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        private static void OnStatusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is KanbanColumn column && newValue is KanbanStatus status)
            {
                if (column._headerView?.BindingContext is KanbanStatus)
                {
                    column._headerView.BindingContext = status;
                }
                else if (column._headerContainer.Content is Label label)
                {
                    label.Text = status.ToString();
                }
            }
        }

        /// <summary>
        /// Handles changes to the <see cref="ItemTemplate"/> property of a <see cref="KanbanColumn"/>.
        /// </summary>
        /// <remarks>This method is invoked when the <see cref="ItemTemplate"/> property changes. It
        /// updates the items in the <see cref="KanbanColumn"/> to reflect the new template.</remarks>
        /// <param name="bindable">The <see cref="BindableObject"/> whose <see cref="ItemTemplate"/> property has changed.</param>
        /// <param name="oldValue">The previous value of the <see cref="ItemTemplate"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="ItemTemplate"/> property.</param>
        private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is KanbanColumn column)
            {
                Debug.WriteLine($"ItemTemplate changed in column: {newValue != null}");
                column.UpdateItems();
            }
        }

        /// <summary>
        /// Handles changes to the <see cref="ItemsSource"/> property of a <see cref="KanbanColumn"/>.
        /// </summary>
        /// <param name="bindable">The <see cref="BindableObject"/> whose <see cref="ItemsSource"/> property has changed.</param>
        /// <param name="oldValue">The previous value of the <see cref="ItemsSource"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="ItemsSource"/> property.</param>
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is KanbanColumn column)
            {
                column.UpdateItems();
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Updates the visual representation of the items in the container by creating views for each item in the data
        /// source.
        /// </summary>
        /// <remarks>This method clears the existing child views in the container and generates new views
        /// for each item in the <see cref="ItemsSource"/>  using the specified <see cref="ItemTemplate"/>. Each
        /// generated view is bound to the corresponding item in the data source. If either <see cref="ItemsSource"/> or
        /// <see cref="ItemTemplate"/> is null, the method exits without making any changes.  Additionally, a drag
        /// gesture is added to each view, allowing the item to be dragged with its data stored in the drag
        /// event.</remarks>
        private void UpdateItems()
        {
            try
            {
                Debug.WriteLine($"UpdateItems called - Template: {ItemTemplate != null}, Items: {ItemsSource?.Count()}");

                _itemsContainer.Children.Clear();

                if (ItemsSource == null || ItemTemplate == null)
                {
                    Debug.WriteLine($"UpdateItems blocked - ItemsSource: {ItemsSource != null}, ItemTemplate: {ItemTemplate != null}");
                    return;
                }

                foreach (var item in ItemsSource)
                {
                    var view = (View)ItemTemplate.CreateContent();

                    if (view is BindableObject bindableView)
                    {
                        bindableView.BindingContext = item;
                    }
                    else
                    {
                        Debug.WriteLine("Warning: Created view is not a BindableObject");
                        continue;
                    }

                    var dragGesture = new DragGestureRecognizer();
                    dragGesture.DragStarting += (s, e) =>
                    {
                        e.Data.Properties["KanbanItem"] = item;
                    };

                    view.GestureRecognizers.Add(dragGesture);
                    _itemsContainer.Children.Add(view);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in UpdateItems: {ex}");
            }
        }

        /// <summary>
        /// Configures the drag-and-drop functionality for the current view, enabling the ability to handle
        /// drag-and-drop gestures.
        /// </summary>
        /// <remarks>This method sets up a <see cref="DropGestureRecognizer"/> to handle drag-and-drop
        /// operations.  It changes the background color of the view during drag-over and drag-leave events, and
        /// processes dropped data  to move a <c>KanbanItem</c> to a new status within the associated
        /// <c>KanbanViewModel</c>.</remarks>
        private void SetupDragAndDrop()
        {
            var dropGesture = new DropGestureRecognizer { AllowDrop = true };

            dropGesture.DragOver += (s, e) =>
            {
                BackgroundColor = DragOverColor;
                e.AcceptedOperation = DataPackageOperation.Copy;
            };

            dropGesture.DragLeave += (s, e) =>
            {
                BackgroundColor = DragLeaveColor;
            };

            dropGesture.Drop += async (s, e) =>
            {
                BackgroundColor = DragLeaveColor;

                if (e.Data.Properties.TryGetValue("KanbanItem", out var obj) &&
                obj is KanbanItem item &&
                BindingContext is KanbanViewModel vm)
                {
                    var itemView = _itemsContainer.Children.FirstOrDefault(v =>
                        (v as BindableObject)?.BindingContext == item);

                    if (itemView != null)
                    {
                        _itemsContainer.Children.Remove(itemView);
                    }

                    await vm.MoveItemToStatus(item, Status);
                }
            };

            GestureRecognizers.Add(dropGesture);
        }
        #endregion
    }
}