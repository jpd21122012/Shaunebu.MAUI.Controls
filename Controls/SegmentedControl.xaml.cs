using Microsoft.Maui.Controls.Shapes;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Input;

namespace Shaunebu.Controls.Controls;

[ContentProperty(nameof(Items))]
public partial class SegmentedControl : ContentView
{
    #region Bindable Properties

    public static readonly BindableProperty ItemsProperty =
        BindableProperty.Create(nameof(Items), typeof(IList), typeof(SegmentedControl), null,
            propertyChanged: OnItemsChanged);

    public static readonly BindableProperty SelectedIndexProperty =
        BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(SegmentedControl), -1,
            BindingMode.TwoWay, propertyChanged: OnSelectedIndexChanged);

    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(SegmentedControl), null,
            BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SegmentedControl));

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(SegmentedControl));

    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(SegmentedControl), 5.0);

    public static readonly BindableProperty SelectedTextColorProperty =
        BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(SegmentedControl), Colors.White);

    public static readonly BindableProperty UnselectedTextColorProperty =
        BindableProperty.Create(nameof(UnselectedTextColor), typeof(Color), typeof(SegmentedControl), Colors.Black);

    public static readonly BindableProperty SelectedBackgroundColorProperty =
        BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(SegmentedControl), Colors.Blue);

    public static readonly BindableProperty UnselectedBackgroundColorProperty =
        BindableProperty.Create(nameof(UnselectedBackgroundColor), typeof(Color), typeof(SegmentedControl), Colors.LightGray);

    public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(SegmentedControl), Colors.LightGray);

    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(SegmentedControl), 1.0);

    public static readonly BindableProperty ItemTemplateProperty =
        BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(SegmentedControl), null,
            propertyChanged: OnItemTemplateChanged);

    public static readonly BindableProperty DisabledBackgroundColorProperty =
        BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(SegmentedControl), Colors.Gray);

    public static readonly BindableProperty DisabledTextColorProperty =
        BindableProperty.Create(nameof(DisabledTextColor), typeof(Color), typeof(SegmentedControl), Colors.DarkGray);

    public static readonly BindableProperty OrientationProperty =
        BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(SegmentedControl), StackOrientation.Horizontal,
            propertyChanged: OnOrientationChanged);

    public static readonly BindableProperty IsEnabledProperty =
        BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(SegmentedControl), true,
            propertyChanged: OnIsEnabledChanged);

    public static readonly BindableProperty AnimationDurationProperty =
        BindableProperty.Create(nameof(AnimationDuration), typeof(uint), typeof(SegmentedControl), (uint)100);

    public static readonly BindableProperty DisabledIndicesProperty =
        BindableProperty.Create(nameof(DisabledIndices), typeof(IList<int>), typeof(SegmentedControl), new List<int>());

    #endregion

    #region Properties

    public IList Items
    {
        get => (IList)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }

    public Color UnselectedTextColor
    {
        get => (Color)GetValue(UnselectedTextColorProperty);
        set => SetValue(UnselectedTextColorProperty, value);
    }

    public Color SelectedBackgroundColor
    {
        get => (Color)GetValue(SelectedBackgroundColorProperty);
        set => SetValue(SelectedBackgroundColorProperty, value);
    }

    public Color UnselectedBackgroundColor
    {
        get => (Color)GetValue(UnselectedBackgroundColorProperty);
        set => SetValue(UnselectedBackgroundColorProperty, value);
    }

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    public Color DisabledBackgroundColor
    {
        get => (Color)GetValue(DisabledBackgroundColorProperty);
        set => SetValue(DisabledBackgroundColorProperty, value);
    }

    public Color DisabledTextColor
    {
        get => (Color)GetValue(DisabledTextColorProperty);
        set => SetValue(DisabledTextColorProperty, value);
    }

    public StackOrientation Orientation
    {
        get => (StackOrientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    public uint AnimationDuration
    {
        get => (uint)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    public IList<int> DisabledIndices
    {
        get => (IList<int>)GetValue(DisabledIndicesProperty);
        set => SetValue(DisabledIndicesProperty, value);
    }

    #endregion

    public SegmentedControl()
    {
        InitializeComponent();
    }

    #region Property Changed Handlers

    private static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SegmentedControl control)
        {
            if (oldValue is INotifyCollectionChanged oldCollection)
            {
                oldCollection.CollectionChanged -= control.OnItemsCollectionChanged;
            }

            if (newValue is INotifyCollectionChanged newCollection)
            {
                newCollection.CollectionChanged += control.OnItemsCollectionChanged;
            }

            control.Render();
        }
    }

    private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SegmentedControl control)
        {
            control.UpdateSelectedItem();
            control.UpdateSelection();
            control.InvalidateLayout();
        }
    }

    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SegmentedControl control)
        {
            control.UpdateSelectedIndex();
            control.UpdateSelection();
            control.InvalidateLayout();
        }
    }

    private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SegmentedControl control)
        {
            control.Render();
        }
    }

    private static void OnOrientationChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SegmentedControl control)
        {
            control.Render();
        }
    }

    private static void OnIsEnabledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SegmentedControl control)
        {
            control.UpdateEnabledState();
        }
    }

    private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        Render();
    }

    #endregion

    #region Rendering Methods

    private void Render()
    {
        containerGrid.ColumnDefinitions.Clear();
        containerGrid.RowDefinitions.Clear();
        containerGrid.Children.Clear();

        if (Items == null || Items.Count == 0)
            return;

        // Setup grid based on orientation
        if (Orientation == StackOrientation.Horizontal)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                containerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }
        }
        else
        {
            for (int i = 0; i < Items.Count; i++)
            {
                containerGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            }
        }

        // Create segments
        for (int i = 0; i < Items.Count; i++)
        {
            var item = Items[i];
            var isDisabled = DisabledIndices?.Contains(i) ?? false;
            var content = CreateSegmentContent(item, i);
            var container = CreateSegmentContainer(content, i, isDisabled);

            if (Orientation == StackOrientation.Horizontal)
            {
                Grid.SetColumn(container, i);
            }
            else
            {
                Grid.SetRow(container, i);
            }

            containerGrid.Children.Add(container);
        }
    }

    private View CreateSegmentContent(object item, int index)
    {
        var isDisabled = DisabledIndices?.Contains(index) ?? false;

        if (ItemTemplate != null)
        {
            var content = (View)ItemTemplate.CreateContent();
            if (content is BindableObject bindable)
            {
                bindable.BindingContext = item;
            }
            return content;
        }

        return new Label
        {
            Text = item?.ToString(),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            TextColor = isDisabled ? DisabledTextColor :
                       index == SelectedIndex ? SelectedTextColor : UnselectedTextColor
        };
    }

    private View CreateSegmentContainer(View content, int index, bool isDisabled)
    {
        var border = new Border
        {
            Content = content,
            StrokeShape = new RoundRectangle { CornerRadius = 0 },
            Stroke = BorderColor,
            BackgroundColor = isDisabled ? DisabledBackgroundColor :
                            index == SelectedIndex ? SelectedBackgroundColor : UnselectedBackgroundColor,
            Padding = 10,
        };

        // For the first item (left/top rounded corners)
        if (index == 0)
        {
            border.StrokeShape = new RoundRectangle
            {
                CornerRadius = Orientation == StackOrientation.Horizontal
                    ? new CornerRadius((float)CornerRadius, 0, (float)CornerRadius, 0) // Horizontal: left corners
                    : new CornerRadius((float)CornerRadius, (float)CornerRadius, 0, 0) // Vertical: top corners
            };
        }
        // For the last item (right/bottom rounded corners)
        else if (index == Items.Count - 1)
        {
            border.StrokeShape = new RoundRectangle
            {
                CornerRadius = Orientation == StackOrientation.Horizontal
                    ? new CornerRadius(0, (float)CornerRadius, 0, (float)CornerRadius) // Horizontal: right corners
                    : new CornerRadius(0, 0, (float)CornerRadius, (float)CornerRadius) // Vertical: bottom corners
            };
        }
        // For middle items (no rounded corners)
        else
        {
            border.StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(0)
            };
        }

        // Create container grid
        var container = new Grid();
        if (Orientation == StackOrientation.Horizontal)
        {
            container.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            Grid.SetColumn(border, 0);
        }
        else
        {
            container.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            Grid.SetRow(border, 0);
        }
        container.Children.Add(border);

        // Add tap gesture if not disabled
        if (!isDisabled)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => HandleSegmentTapped(index);
            container.GestureRecognizers.Add(tapGesture);
        }

        return container;
    }

    private async void HandleSegmentTapped(int index)
    {
        if (index < 0 || index >= Items.Count || DisabledIndices?.Contains(index) == true)
            return;

        // Animation: Briefly fade the tapped segmentAnimationDurationProperty 
        if (AnimationDuration > 0 && containerGrid.Children.Count > index)
        {
            if (containerGrid.Children[index] is Grid container &&
                container.Children[0] is Border border)
            {
                // Fade out slightly when tapped
                await border.FadeTo(0.7, AnimationDuration / 2);

                // Change selection
                SelectedIndex = index;

                // Fade back in
                await border.FadeTo(1.0, AnimationDuration / 2);
            }
        }
        else
        {
            // No animation - instant selection
            SelectedIndex = index;
        }

        // Execute command if available
        if (Command?.CanExecute(CommandParameter) == true)
        {
            Command.Execute(CommandParameter);
        }
    }

    #endregion

    #region Selection Methods

    private void UpdateSelectedItem()
    {
        if (Items == null || SelectedIndex < 0 || SelectedIndex >= Items.Count)
        {
            SelectedItem = null;
        }
        else
        {
            SelectedItem = Items[SelectedIndex];
        }
    }

    private void UpdateSelectedIndex()
    {
        if (Items == null || SelectedItem == null)
        {
            SelectedIndex = -1;
        }
        else
        {
            SelectedIndex = Items.IndexOf(SelectedItem);
        }
    }

    private async Task OnSegmentTapped(int index)
    {
        if (index < 0 || index >= Items.Count || DisabledIndices?.Contains(index) == true)
            return;

        // Animate selection
        if (AnimationDuration > 0)
        {
            var selectedBorder = containerGrid.Children[SelectedIndex] as Border;
            var newBorder = containerGrid.Children[index] as Border;

            if (selectedBorder != null && newBorder != null)
            {
                await Task.WhenAll(
                    selectedBorder.FadeTo(0.7, AnimationDuration / 2),
                    newBorder.FadeTo(0.7, AnimationDuration / 2)
                );
            }
        }

        SelectedIndex = index;

        if (Command != null && Command.CanExecute(CommandParameter))
        {
            Command.Execute(CommandParameter);
        }

        if (AnimationDuration > 0)
        {
            var selectedBorder = containerGrid.Children[SelectedIndex] as Border;
            if (selectedBorder != null)
            {
                await selectedBorder.FadeTo(1, AnimationDuration / 2);
            }
        }
    }

    private void UpdateSelection()
    {
        if (containerGrid.Children == null || Items == null) return;

        for (int i = 0; i < containerGrid.Children.Count; i++)
        {
            if (containerGrid.Children[i] is Grid container &&
                container.Children[0] is Border border)
            {
                var isDisabled = DisabledIndices?.Contains(i) ?? false;

                border.BackgroundColor = isDisabled ? DisabledBackgroundColor :
                                      i == SelectedIndex ? SelectedBackgroundColor : UnselectedBackgroundColor;

                if (border.Content is Label label)
                {
                    label.TextColor = isDisabled ? DisabledTextColor :
                                    i == SelectedIndex ? SelectedTextColor : UnselectedTextColor;
                }
            }
        }
    }

    private void UpdateEnabledState()
    {
        if (containerGrid.Children == null)
            return;

        foreach (var child in containerGrid.Children)
        {
            if (child is Border border)
            {
                var index = Orientation == StackOrientation.Horizontal ?
                           Grid.GetColumn(border) : Grid.GetRow(border);

                var isDisabled = DisabledIndices?.Contains(index) ?? false;

                if (isDisabled || !IsEnabled)
                {
                    border.BackgroundColor = DisabledBackgroundColor;
                    if (border.Content is Label label)
                    {
                        label.TextColor = DisabledTextColor;
                    }
                    border.GestureRecognizers.Clear();
                }
                else
                {
                    border.BackgroundColor = index == SelectedIndex ? SelectedBackgroundColor : UnselectedBackgroundColor;
                    if (border.Content is Label label)
                    {
                        label.TextColor = index == SelectedIndex ? SelectedTextColor : UnselectedTextColor;
                    }

                    if (border.GestureRecognizers.Count == 0)
                    {
                        var tapGesture = new TapGestureRecognizer();
                        tapGesture.Tapped += async (s, e) => await OnSegmentTapped(index);
                        border.GestureRecognizers.Add(tapGesture);
                    }
                }
            }
        }
    }

    #endregion
}