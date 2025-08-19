# 🚀 Segmented Control for .NET MAUI
![Platform Support](https://img.shields.io/badge/Platforms-Android%20|%20iOS-lightgrey)
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

A customizable and feature-rich segmented control for .NET MAUI

![Dock Layout Screenshot](https://jpdblog.blob.core.windows.net/apps/SegmentedControl.png)

## Table of Contents
1. [Features](#features)
2. [Installation](#installation)
3. [Basic Implementation](#basic-usage)
4. [Advanced Features](#advanced-features)
5. [Property Reference](#property-reference) 
6. [Troubleshooting](#troubleshooting)

---

## Features

*   ✅ **Custom Item Templates** - Fully customizable segment content
*   ✅ **Disabled State Support** - Disable specific segments or entire control
*   ✅ **Smooth Animations** - Configurable selection animations
*   ✅ **Icon Support** - Icons, text, or any custom content
*   ✅ **Dual Orientation** - Horizontal or vertical layout
*   ✅ **Two-Way Binding** - Full MVVM support
*   ✅ **Command Support** - Execute commands on selection
*   ✅ **Complete Styling** - Full control over appearance
*   ✅ **Collection Binding** - Dynamic item updates
*   ✅ **Accessibility** - Proper touch handling

## Installation

1. Add the control to your MAUI project:
   ```bash
   dotnet add package Shaunebu.MAUI.Controls
   ```

2. Register the namespace:
   ```xml
   xmlns:kanban="clr-namespace:Shaunebu.MAUI.Controls;assembly=Shaunebu.MAUI.Controls.SegmentedControl"
   ```

## Usage

### Basic Implementation

```xml
    <controls:SegmentedControl
    Items="{Binding Options}"
    SelectedIndex="{Binding SelectedOptionIndex}"
    SelectedBackgroundColor="RoyalBlue"
    UnselectedBackgroundColor="LightGray"
    SelectedTextColor="White"
    UnselectedTextColor="Black"
    CornerRadius="10"
    BorderColor="Gray"
    BorderWidth="1" />
```

## API Reference

### Core Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IList` | `null` | Collection of items to display |
| `SelectedIndex` | `int` | `-1` | Currently selected index (TwoWay) |
| `SelectedItem` | `object` | `null` | Currently selected item (TwoWay) |
| `Command` | `ICommand` | `null` | Command to execute on selection |
| `CommandParameter` | `object` | `null` | Parameter for the command |

### Styling Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `CornerRadius` | `double` | `5.0` | Radius for rounded corners |
| `SelectedBackgroundColor` | `Color` | `Blue` | Background color of selected segment |
| `UnselectedBackgroundColor` | `Color` | `LightGray` | Background color of unselected segments |
| `SelectedTextColor` | `Color` | `White` | Text color of selected segment |
| `UnselectedTextColor` | `Color` | `Black` | Text color of unselected segments |
| `BorderColor` | `Color` | `LightGray` | Border color |
| `BorderWidth` | `double` | `1.0` | Border thickness |

### Advanced Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `ItemTemplate` | `DataTemplate` | `null` | Template for segment content |
| `DisabledBackgroundColor` | `Color` | `Gray` | Background for disabled segments |
| `DisabledTextColor` | `Color` | `DarkGray` | Text color for disabled segments |
| `Orientation` | `StackOrientation` | `Horizontal` | Layout direction |
| `AnimationDuration` | `uint` | `100` | Selection animation duration in ms |
| `DisabledIndices` | `IList<int>` | `[]` | List of disabled segment indices |
| `IsEnabled` | `bool` | `true` | Enable/disable entire control |


Usage Examples
--------------

### Basic Text Segments

```xml

<controls:SegmentedControl
    Items="{Binding Options}"
    SelectedIndex="{Binding SelectedOptionIndex}"
    SelectedBackgroundColor="#007ACC"
    UnselectedBackgroundColor="#E0E0E0"
    SelectedTextColor="White"
    UnselectedTextColor="Black"
    CornerRadius="8"
    BorderColor="#CCCCCC"
    BorderWidth="1" />
```

### With Icons and Custom Template

```xml

<controls:SegmentedControl 
    Items="{Binding IconOptions}"
    SelectedIndex="{Binding SelectedIconOptionIndex}"
    SelectedBackgroundColor="#FF5722"
    UnselectedBackgroundColor="#F5F5F5">
    
    <controls:SegmentedControl.ItemTemplate>
        <DataTemplate>
            <HorizontalStackLayout Spacing="8" Padding="12">
                <Image Source="{Binding Icon}" 
                       WidthRequest="24" 
                       HeightRequest="24"
                       VerticalOptions="Center"/>
                <Label Text="{Binding Text}" 
                       FontSize="14"
                       VerticalOptions="Center"/>
            </HorizontalStackLayout>
        </DataTemplate>
    </controls:SegmentedControl.ItemTemplate>
</controls:SegmentedControl>
```

### Vertical Orientation

```xml

<controls:SegmentedControl 
    Items="{Binding VerticalOptions}"
    SelectedIndex="{Binding SelectedVerticalOptionIndex}"
    Orientation="Vertical"
    HeightRequest="200"
    SelectedBackgroundColor="#4CAF50"
    CornerRadius="6"/>
```

### Disabled Segments

```xml

<controls:SegmentedControl 
    Items="{Binding Options}"
    SelectedIndex="{Binding SelectedOptionIndex}"
    DisabledIndices="{Binding DisabledIndices}"
    DisabledBackgroundColor="#BDBDBD"
    DisabledTextColor="#757575"/>
```

### With Animations

```xml
<controls:SegmentedControl 
    Items="{Binding Options}"
    SelectedIndex="{Binding SelectedOptionIndex}"
    AnimationDuration="200"
    SelectedBackgroundColor="#9C27B0"/>
```

ViewModel Implementation
------------------------

```csharp
public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<string> Options { get; } = new()
    {
        "Option 1", "Option 2", "Option 3"
    };

    public ObservableCollection<IconOption> IconOptions { get; } = new()
    {
        new IconOption { Text = "Home", Icon = "home.png" },
        new IconOption { Text = "Search", Icon = "search.png" },
        new IconOption { Text = "Settings", Icon = "settings.png" }
    };

    public List<int> DisabledIndices { get; } = new() { 1 };

    private int _selectedIndex;
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (_selectedIndex != value)
            {
                _selectedIndex = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
    }

    public string SelectedItem => SelectedIndex >= 0 ? Options[SelectedIndex] : "None";

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class IconOption
{
    public string Text { get; set; }
    public string Icon { get; set; }
}
```

Customization Guide
-------------------

### Custom Item Templates

```xml

<controls:SegmentedControl.Items>
    <x:Array Type="{x:Type x:String}">
        <x:String>Low</x:String>
        <x:String>Medium</x:String>
        <x:String>High</x:String>
    </x:Array>
</controls:SegmentedControl.Items>

<controls:SegmentedControl.ItemTemplate>
    <DataTemplate>
        <Grid Padding="10">
            <Frame CornerRadius="20" 
                   BackgroundColor="{Binding IsSelected, Converter={StaticResource SelectionToColorConverter}}"
                   Padding="15,8">
                <Label Text="{Binding .}" 
                       FontAttributes="Bold"
                       HorizontalOptions="Center"/>
            </Frame>
        </Grid>
    </DataTemplate>
</controls:SegmentedControl.ItemTemplate>
```


### Dynamic Items

```csharp

// Add items dynamically
var segmentedControl = new SegmentedControl();
segmentedControl.Items = new ObservableCollection<string> { "Dynamic", "Items", "Here" };

// Respond to collection changes
var items = new ObservableCollection<string>();
items.CollectionChanged += (s, e) => 
{
    // Control automatically updates
};
```

Event Handling
--------------

### Command Execution

```csharp
public ICommand SegmentSelectedCommand => new Command(() =>
{
    // Handle selection change
    Debug.WriteLine($"Selected: {SelectedItem}");
});

// XAML
<controls:SegmentedControl Command="{Binding SegmentSelectedCommand}"/>
```

### Event Handlers in Code-Behind

```csharp

segmentedControl.PropertyChanged += (s, e) =>
{
    if (e.PropertyName == nameof(SegmentedControl.SelectedIndex))
    {
        // Handle selection change
    }
};
```

Performance Tips
----------------

1.  **Use ObservableCollections** for dynamic items
    
2.  **Reuse ItemTemplates** when possible
    
3.  **Set AnimationDuration to 0** for maximum performance
    
4.  **Avoid complex layouts** in ItemTemplates for large collections
    
5.  **Use lightweight elements** in custom templates




## License

MIT License © 2025 [Jorge Perales Diaz](https://shaunebu.com/)