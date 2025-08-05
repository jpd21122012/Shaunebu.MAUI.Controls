using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using global::Shaunebu.Controls.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Shaunebu.Controls.ViewModels
{
    public partial class KanbanViewModel : ObservableObject
    {
        #region Fields
        #endregion

        #region Services
        #endregion

        #region Validators
        #endregion

        #region Properties
        /// <summary>
        /// The dragged item
        /// </summary>
        [ObservableProperty] private KanbanItem _draggedItem;

        /// <summary>
        /// The items
        /// </summary>
        private ObservableCollection<KanbanItem> items;
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ObservableCollection<KanbanItem> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }
        /// <summary>
        /// The statuses
        /// </summary>
        private ObservableCollection<KanbanStatus> statuses;
        /// <summary>
        /// Gets or sets the statuses.
        /// </summary>
        /// <value>
        /// The statuses.
        /// </value>
        public ObservableCollection<KanbanStatus> Statuses
        {
            get { return statuses; }
            set { SetProperty(ref statuses, value); }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Gets the item dragged command.
        /// </summary>
        /// <value>
        /// The item dragged command.
        /// </value>
        public IRelayCommand<KanbanItem> ItemDraggedCommand { get; }

        /// <summary>
        /// Gets the item dropped command.
        /// </summary>
        /// <value>
        /// The item dropped command.
        /// </value>
        public IRelayCommand<KanbanStatus> ItemDroppedCommand { get; }

        /// <summary>
        /// Gets the item tapped command.
        /// </summary>
        /// <value>
        /// The item tapped command.
        /// </value>
        public IRelayCommand<KanbanItem> ItemTappedCommand { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="KanbanViewModel"/> class.
        /// </summary>
        public KanbanViewModel()
        {
            Items = new ObservableCollection<KanbanItem>();
            Statuses = new ObservableCollection<KanbanStatus>();

            ItemDraggedCommand = new RelayCommand<KanbanItem>(OnItemDragged);
            ItemDroppedCommand = new RelayCommand<KanbanStatus>(OnItemDropped);
            ItemTappedCommand = new RelayCommand<KanbanItem>(OnItemTapped);

            InitializeData();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the data.
        /// </summary>
        private void InitializeData()
        {
            Statuses.Clear();
            Items.Clear();

            foreach (var status in Enum.GetValues<KanbanStatus>())
            {
                Statuses.Add(status);
                Debug.WriteLine($"Added status: {status}");
            }

            var random = new Random();
            var colors = new[] { Colors.Red, Colors.Blue, Colors.Green, Colors.Yellow };

            for (int i = 1; i <= 20; i++)
            {
                var item = new KanbanItem
                {
                    Title = $"Task {i}",
                    Description = $"Description for task {i}",
                    Status = (KanbanStatus)random.Next(0, 4),
                    CategoryColor = colors[random.Next(0, 4)],
                    DueDate = DateTime.Now.AddDays(random.Next(1, 30))
                };
                Items.Add(item);
                Debug.WriteLine($"Added item: {item.Title} with status: {item.Status}");
            }
        }

        /// <summary>
        /// Called when [item dragged].
        /// </summary>
        /// <param name="item">The item.</param>
        private void OnItemDragged(KanbanItem item)
        {
            if (item == null) return;

            DraggedItem = item;
            Debug.WriteLine($"Started dragging: {item.Title}");
        }

        /// <summary>
        /// Called when [item dropped].
        /// </summary>
        /// <param name="newStatus">The new status.</param>
        private void OnItemDropped(KanbanStatus newStatus)
        {
            if (DraggedItem == null || DraggedItem.Status == newStatus)
            {
                DraggedItem = null;
                return;
            }

            try
            {
                var oldStatus = DraggedItem.Status;
                DraggedItem.Status = newStatus;

                Debug.WriteLine($"Moved item from {oldStatus} to {newStatus}");

                Device.BeginInvokeOnMainThread(() =>
                {
                    var tempList = new List<KanbanItem>(Items);
                    Items = new ObservableCollection<KanbanItem>(tempList);
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Drop failed: {ex.Message}");
            }
            finally
            {
                DraggedItem = null;
            }
        }

        /// <summary>
        /// Moves the item to status.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="newStatus">The new status.</param>
        public async Task MoveItemToStatus(KanbanItem item, KanbanStatus newStatus)
        {
            if (item == null || item.Status == newStatus)
                return;

            try
            {
                var oldStatus = item.Status;

                Items.Remove(item);

                item.Status = newStatus;

                Items.Add(item);

                Debug.WriteLine($"Moved '{item.Title}' from {oldStatus} to {newStatus}");

                Device.BeginInvokeOnMainThread(() =>
                {
                    Items = new ObservableCollection<KanbanItem>(Items);
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Move failed: {ex}");
            }
        }

        /// <summary>
        /// Called when [item tapped].
        /// </summary>
        /// <param name="item">The item.</param>
        private void OnItemTapped(KanbanItem item)
        {
            if (item == null) return;

            Debug.WriteLine($"Tapped: {item.Title}");
        }

        /// <summary>
        /// Bulks the update.
        /// </summary>
        /// <param name="updateAction">The update action.</param>
        public void BulkUpdate(Action updateAction)
        {
            var tempItems = new ObservableCollection<KanbanItem>(Items);
            updateAction?.Invoke();
            Items = tempItems;
        }
        #endregion
    }
}
