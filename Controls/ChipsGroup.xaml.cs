using Shaunebu.Controls.Enums;
using Shaunebu.Controls.Events;
using Shaunebu.Controls.Models;
using System.Collections.ObjectModel;
using SelectionChangedEventArgs = Shaunebu.Controls.Events.SelectionChangedEventArgs;

namespace Shaunebu.Controls.Controls;

public partial class ChipsGroup : ContentView
{
    /// <summary>
    /// The chip items property
    /// </summary>
    public static readonly BindableProperty ChipItemsProperty = BindableProperty.Create(nameof(ChipItems), typeof(ObservableCollection<ChipModel>),
            typeof(ChipsGroup), new ObservableCollection<ChipModel>(), propertyChanged: OnChipItemsChanged);

    /// <summary>
    /// Gets or sets the chip items.
    /// </summary>
    /// <value>
    /// The chip items.
    /// </value>
    public ObservableCollection<ChipModel> ChipItems
    {
        get => (ObservableCollection<ChipModel>)GetValue(ChipItemsProperty);
        set => SetValue(ChipItemsProperty, value);
    }

    /// <summary>
    /// The selection mode property
    /// </summary>
    public static readonly BindableProperty SelectionModeProperty =
        BindableProperty.Create(
            nameof(SelectionMode),
            typeof(ChipSelectionMode),
            typeof(ChipsGroup),
            ChipSelectionMode.None);

    /// <summary>
    /// Gets or sets the selection mode.
    /// </summary>
    /// <value>
    /// The selection mode.
    /// </value>
    public ChipSelectionMode SelectionMode
    {
        get => (ChipSelectionMode)GetValue(SelectionModeProperty);
        set => SetValue(SelectionModeProperty, value);
    }

    /// <summary>
    /// Gets the selected items.
    /// </summary>
    /// <value>
    /// The selected items.
    /// </value>
    public ObservableCollection<ChipModel> SelectedItems { get; private set; } = new();

    /// <summary>
    /// Occurs when [selection changed].
    /// </summary>
    public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChipsGroup"/> class.
    /// </summary>
    public ChipsGroup()
    {
        InitializeComponent();
        ChipItems.CollectionChanged += (s, e) => BuildChips();
    }

    /// <summary>
    /// Called when [chip items changed].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private static void OnChipItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ChipsGroup group)
        {
            if (oldValue is ObservableCollection<ChipModel> oldCollection)
                oldCollection.CollectionChanged -= (s, e) => group.BuildChips();

            if (newValue is ObservableCollection<ChipModel> newCollection)
                newCollection.CollectionChanged += (s, e) => group.BuildChips();

            group.BuildChips();
        }
    }

    /// <summary>
    /// Builds the chips.
    /// </summary>
    private void BuildChips()
    {
        if (chipsLayout == null || ChipItems == null) return;

        chipsLayout.Children.Clear();

        foreach (var model in ChipItems)
        {
            var chip = new Chip
            {
                Text = model.Text,
                ChipBackgroundColor = model.BackgroundColor,
                TextColor = model.TextColor,
                Icon = model.Icon,
                BadgeText = model.BadgeText,
                BadgeColor = model.BadgeColor,
                IsClosable = model.IsClosable,
                HeightRequest = 40
            };

            // Handle chip closed
            chip.Closed += (s, e) => ChipItems.Remove(model);

            // Handle chip clicked for selection
            chip.Clicked += (s, e) =>
            {
                if (SelectionMode == ChipSelectionMode.None) return;

                switch (SelectionMode)
                {
                    case ChipSelectionMode.Single:
                        SelectedItems.Clear();
                        SelectedItems.Add(model);
                        break;

                    case ChipSelectionMode.Multiple:
                        if (SelectedItems.Contains(model))
                            SelectedItems.Remove(model);
                        else
                            SelectedItems.Add(model);
                        break;
                }

                // Update visual state
                chip.IsSelected = SelectedItems.Contains(model);

                SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(SelectedItems));
            };

            chip.IsSelected = SelectedItems.Contains(model);

            chipsLayout.Children.Add(chip);
        }
    }
}
