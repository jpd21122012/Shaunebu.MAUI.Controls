using Shaunebu.Controls.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Shaunebu.Controls.Controls
{
    public class KanbanBoard : ContentView
    {
        #region Bindable Properties        
        /// <summary>
        /// The items source property
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<KanbanItem>), typeof(KanbanBoard),
                default(IEnumerable<KanbanItem>), BindingMode.TwoWay, propertyChanged: OnItemsSourceChanged);

        /// <summary>
        /// The statuses source property
        /// </summary>
        public static readonly BindableProperty StatusesSourceProperty = BindableProperty.Create(nameof(StatusesSource), typeof(IEnumerable<KanbanStatus>),
                typeof(KanbanBoard), default(IEnumerable<KanbanStatus>), BindingMode.TwoWay, propertyChanged: OnStatusesSourceChanged);

        /// <summary>
        /// The item template property
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate),
                typeof(KanbanBoard), default(DataTemplate));

        /// <summary>
        /// The column header template property
        /// </summary>
        public static readonly BindableProperty ColumnHeaderTemplateProperty = BindableProperty.Create(nameof(ColumnHeaderTemplate), typeof(DataTemplate),
        typeof(KanbanBoard), default(DataTemplate));

        /// <summary>
        /// The drag over color property
        /// </summary>
        public static readonly BindableProperty DragOverColorProperty = BindableProperty.Create(nameof(DragOverColor), typeof(Color),
                typeof(KanbanBoard), Color.FromArgb("#3300FF00"), propertyChanged: OnDragColorChanged);

        /// <summary>
        /// The drag leave color property
        /// </summary>
        public static readonly BindableProperty DragLeaveColorProperty = BindableProperty.Create(nameof(DragLeaveColor), typeof(Color),
            typeof(KanbanBoard), Color.FromArgb("#F0F0F0"), propertyChanged: OnDragColorChanged);

        /// <summary>
        /// The board background color property
        /// </summary>
        public static readonly BindableProperty BoardBackgroundColorProperty = BindableProperty.Create(nameof(BoardBackgroundColor), typeof(Color),
        typeof(KanbanBoard), Colors.LightGray, propertyChanged: OnBoardBackgroundColorChanged);
        #endregion

        #region Properties           
        /// <summary>
        /// Gets or sets the color of the board background.
        /// </summary>
        /// <value>
        /// The color of the board background.
        /// </value>
        public Color BoardBackgroundColor
        {
            get => (Color)GetValue(BoardBackgroundColorProperty);
            set => SetValue(BoardBackgroundColorProperty, value);
        }

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
        /// Gets or sets the column header template.
        /// </summary>
        /// <value>
        /// The column header template.
        /// </value>
        public DataTemplate ColumnHeaderTemplate
        {
            get => (DataTemplate)GetValue(ColumnHeaderTemplateProperty);
            set => SetValue(ColumnHeaderTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>
        /// The items source.
        /// </value>
        public IEnumerable<KanbanItem> ItemsSource
        {
            get => (IEnumerable<KanbanItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the statuses source.
        /// </summary>
        /// <value>
        /// The statuses source.
        /// </value>
        public IEnumerable<KanbanStatus> StatusesSource
        {
            get => (IEnumerable<KanbanStatus>)GetValue(StatusesSourceProperty);
            set => SetValue(StatusesSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>
        /// The item template.
        /// </value>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        #endregion

        #region Private Fields        
        /// <summary>
        /// The grid
        /// </summary>
        private readonly Grid _grid = new()
        {
            ColumnSpacing = 20,
            Padding = new Thickness(10),
            BackgroundColor = Colors.Black
        };

        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="KanbanBoard"/> class.
        /// </summary>
        public KanbanBoard()
        {
            _grid.BackgroundColor = BoardBackgroundColor;

            Content = new ScrollView
            {
                Orientation = ScrollOrientation.Horizontal,
                Content = _grid
            };
        }

        #endregion

        #region Property Changed Handlers             
        /// <summary>
        /// Called when [board background color changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnBoardBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is KanbanBoard board && newValue is Color color)
            {
                board._grid.BackgroundColor = color;
            }
        }

        /// <summary>
        /// Called when [drag color changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnDragColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is KanbanBoard board)
            {
                // Update all existing columns
                foreach (var child in board._grid.Children)
                {
                    if (child is KanbanColumn column)
                    {
                        column.DragOverColor = board.DragOverColor;
                        column.DragLeaveColor = board.DragLeaveColor;
                    }
                }
            }
        }

        /// <summary>
        /// Called when [items source changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is KanbanBoard board)
            {
                board.UpdateBoard();
            }
        }

        /// <summary>
        /// Called when [statuses source changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnStatusesSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is KanbanBoard board)
            {
                board.UpdateBoard();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the board.
        /// </summary>
        private void UpdateBoard()
        {
            try
            {
                _grid.Children.Clear();
                _grid.ColumnDefinitions.Clear();

                if (StatusesSource == null || ItemsSource == null || ItemTemplate == null)
                    return;

                var statusList = StatusesSource.ToList();
                var itemsList = ItemsSource.ToList();

                for (int i = 0; i < statusList.Count; i++)
                {
                    _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var status = statusList[i];
                    var columnItems = itemsList.Where(x => x.Status == status).ToList();

                    var column = new KanbanColumn
                    {
                        Status = status,
                        ItemsSource = new ObservableCollection<KanbanItem>(columnItems),
                        BindingContext = this.BindingContext,
                        HeaderTemplate = this.ColumnHeaderTemplate,
                        DragOverColor = this.DragOverColor,
                        DragLeaveColor = this.DragLeaveColor
                    };
                    column.SetValue(KanbanColumn.ItemTemplateProperty, this.ItemTemplate);

                    _grid.Add(column, i, 0);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in UpdateBoard: {ex}");
            }
        }

        #endregion
    }
}
