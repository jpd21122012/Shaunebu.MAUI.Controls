# 🚀 Chips Control for .NET MAUI
![Platform Support](https://img.shields.io/badge/Platforms-Android%20|%20iOS-lightgrey)
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

Customize chip appearance with a lot of options

![Chip Control](https://jpdblog.blob.core.windows.net/apps/ChipControl.png)

## Table of Contents
1. [Features](#features)
2. [Installation](#installation)
3. [Basic Implementation](#basic-usage)
4. [Advanced Features](#advanced-features)
5. [Property Reference](#property-reference) 
6. [Troubleshooting](#troubleshooting)

---

## Features

*   ✅ **Multiple Chip Types** - Text, icon, closable, and badge chips
*   ✅ **Selection Modes** - Single, multiple, or no selection
*   ✅ **Custom Styling** - Full control over colors, borders, and appearance
*   ✅ **Badge Support** - Number badges and status indicators
*   ✅ **Event Handling** - Click, close, and selection events
*   ✅ **MVVM Support** - Full data binding support
*   ✅ **Accessibility** - Proper touch handling and focus management

## Installation

1. Add the control to your MAUI project:
   ```bash
   dotnet add package Shaunebu.MAUI.Controls
   ```

2. Register the namespace:
   ```xml
   xmlns:chip="clr-namespace:Shaunebu.MAUI.Controls;assembly=Shaunebu.MAUI.Controls.Chip"
   ```

## Usage

### Basic Implementation

```xml
<controls:Chip 
    Text="Basic Chip"
    ChipBackgroundColor="#FFE0E0E0"
    TextColor="Black"
    CornerRadius="16"/>
```

### Chip with Icon

```xml
<controls:Chip 
    Text="With Icon"
    Icon="icon_settings.png"
    ChipBackgroundColor="#FFE3F2FD"
    TextColor="#FF1976D2"/>
```

### Closable Chip

```xml
<controls:Chip 
    Text="Closable"
    IsClosable="True"
    ChipBackgroundColor="#FFFBE9E7"
    TextColor="#FFD32F2F"
    CloseCommand="{Binding RemoveChipCommand}"/>
```

### Chip with Badge

```xml
<controls:Chip 
    Text="Notifications"
    BadgeText="5"
    BadgeColor="#FFF44336"
    ChipBackgroundColor="#FFF3E5F5"
    TextColor="#FF7B1FA2"/>
```

### Selectable Chip

```xml
<controls:Chip 
    Text="Selectable"
    IsSelected="{Binding IsSelected}"
    ChipBackgroundColor="{Binding IsSelected, Converter={StaticResource SelectionColorConverter}}"
    Command="{Binding ChipTappedCommand}"/>
```



ChipsGroup Control
------------------

### Basic Chips Group

```xml
<controls:ChipsGroup 
    ChipItems="{Binding ChipModels}"
    SelectionMode="Multiple"/>
```
### Single Selection Group

```xml
<controls:ChipsGroup 
    ChipItems="{Binding FilterOptions}"
    SelectionMode="Single"
    SelectionChanged="OnSelectionChanged"/>
```

### Dynamic Chips Group

```xml
<controls:ChipsGroup 
    ChipItems="{Binding DynamicChips}"
    SelectionMode="None">
    <controls:ChipsGroup.Resources>
        <Style TargetType="controls:Chip">
            <Setter Property="CornerRadius" Value="20"/>
            <Setter Property="HeightRequest" Value="40"/>
        </Style>
    </controls:ChipsGroup.Resources>
</controls:ChipsGroup>
```

## API Reference

### Chip Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Text` | `string` | `string.Empty` | Chip display text |
| `TextColor` | `Color` | `Black` | Text color |
| `ChipBackgroundColor` | `Color` | `LightGray` | Background color |
| `BorderColor` | `Color` | `Gray` | Border color |
| `CornerRadius` | `double` | `16.0` | Corner radius |
| `Icon` | `ImageSource` | `null` | Icon image source |
| `IsClosable` | `bool` | `false` | Show close button |
| `IsSelected` | `bool` | `false` | Selection state (TwoWay) |
| `BadgeText` | `string` | `string.Empty` | Badge text content |
| `BadgeColor` | `Color` | `Red` | Badge background color |
| `Command` | `ICommand` | `null` | Tap command |
| `CommandParameter` | `object` | `null` | Command parameter |
| `CloseCommand` | `ICommand` | `null` | Close button command |

### ChipsGroup Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `ChipItems` | `ObservableCollection<ChipModel>` | Empty | Collection of chip models |
| `SelectionMode` | `ChipSelectionMode` | `None` | Selection behavior |
| `SelectedItems` | `ObservableCollection<ChipModel>` | Read-only | Currently selected chips |




### ChipSelectionMode Enum

```csharp
public enum ChipSelectionMode
{
    None,      // No selection allowed
    Single,    // Single chip selection
    Multiple   // Multiple chip selection
}
```

Data Models
-----------

### ChipModel

```csharp
public class ChipModel
{
    public string Text { get; set; }
    public Color BackgroundColor { get; set; } = Colors.LightGray;
    public Color TextColor { get; set; } = Colors.Black;
    public ImageSource Icon { get; set; }
    public string BadgeText { get; set; }
    public Color BadgeColor { get; set; } = Colors.Red;
    public bool IsClosable { get; set; }
}
```

### SelectionChangedEventArgs

```csharp
public class SelectionChangedEventArgs : EventArgs
{
    public IList<ChipModel> SelectedItems { get; }

    public SelectionChangedEventArgs(IList<ChipModel> selectedItems)
    {
        SelectedItems = selectedItems;
    }
}
```

Event Handling
--------------

### Chip Events

```csharp
// Click event
chip.Clicked += (s, e) => 
{
    // Handle chip click
};

// Close event  
chip.Closed += (s, e) =>
{
    // Handle chip close
};

// Command binding
chip.Command = new Command(() =>
{
    // Handle command execution
});
```

### ChipsGroup Events

```csharp
// Selection changed event
chipsGroup.SelectionChanged += (s, e) =>
{
    var selectedItems = e.SelectedItems;
    // Handle selection change
};

// XAML event handling
<controls:ChipsGroup SelectionChanged="OnSelectionChanged"/>

// Code-behind
private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
{
    // Handle selection change
}
```

ViewModel Implementation
------------------------

### Basic ViewModel

```csharp
public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ChipModel> ChipItems { get; } = new()
    {
        new ChipModel { Text = "Technology", BackgroundColor = Color.FromArgb("#FFE3F2FD"), TextColor = Color.FromArgb("#FF1976D2") },
        new ChipModel { Text = "Design", BackgroundColor = Color.FromArgb("#FFF3E5F5"), TextColor = Color.FromArgb("#FF7B1FA2") },
        new ChipModel { Text = "Development", BackgroundColor = Color.FromArgb("#FFF1F8E9"), TextColor = Color.FromArgb("#FF2E7D32"), Icon = "code.png" },
        new ChipModel { Text = "Testing", BackgroundColor = Color.FromArgb("#FFFFFDE7"), TextColor = Color.FromArgb("#FFF57F17"), BadgeText = "3", BadgeColor = Colors.Orange },
        new ChipModel { Text = "Deployment", BackgroundColor = Color.FromArgb("#FFFBE9E7"), TextColor = Color.FromArgb("#FFD32F2F"), IsClosable = true }
    };

    public ObservableCollection<ChipModel> SelectedChips { get; } = new();

    public ICommand RemoveChipCommand => new Command<ChipModel>(chip =>
    {
        ChipItems.Remove(chip);
    });

    public ICommand ChipTappedCommand => new Command<ChipModel>(chip =>
    {
        // Handle chip tap
    });

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

### Advanced ViewModel with Selection

```csharp
public class FilterViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ChipModel> FilterOptions { get; } = new()
    {
        new ChipModel { Text = "All", BackgroundColor = Colors.LightGray },
        new ChipModel { Text = "Active", BackgroundColor = Colors.LightGreen },
        new ChipModel { Text = "Completed", BackgroundColor = Colors.LightBlue },
        new ChipModel { Text = "Archived", BackgroundColor = Colors.LightPink }
    };

    private ObservableCollection<ChipModel> _selectedFilters = new();
    public ObservableCollection<ChipModel> SelectedFilters
    {
        get => _selectedFilters;
        set
        {
            _selectedFilters = value;
            OnPropertyChanged();
            ApplyFilters();
        }
    }

    private void ApplyFilters()
    {
        // Apply filtering logic based on selected chips
        Debug.WriteLine($"Selected filters: {string.Join(", ", SelectedFilters.Select(f => f.Text))}");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

Styling and Customization
-------------------------

### Resource Dictionary Styles

```xml
<ResourceDictionary>
    <!-- Default Chip Style -->
    <Style x:Key="DefaultChipStyle" TargetType="controls:Chip">
        <Setter Property="CornerRadius" Value="16"/>
        <Setter Property="HeightRequest" Value="36"/>
        <Setter Property="ChipBackgroundColor" Value="#FFE0E0E0"/>
        <Setter Property="TextColor" Value="#FF212121"/>
    </Style>

    <!-- Primary Chip Style -->
    <Style x:Key="PrimaryChipStyle" TargetType="controls:Chip" BasedOn="{StaticResource DefaultChipStyle}">
        <Setter Property="ChipBackgroundColor" Value="#FFE3F2FD"/>
        <Setter Property="TextColor" Value="#FF1976D2"/>
    </Style>

    <!-- Success Chip Style -->
    <Style x:Key="SuccessChipStyle" TargetType="controls:Chip" BasedOn="{StaticResource DefaultChipStyle}">
        <Setter Property="ChipBackgroundColor" Value="#FFF1F8E9"/>
        <Setter Property="TextColor" Value="#FF2E7D32"/>
    </Style>

    <!-- Warning Chip Style -->
    <Style x:Key="WarningChipStyle" TargetType="controls:Chip" BasedOn="{StaticResource DefaultChipStyle}">
        <Setter Property="ChipBackgroundColor" Value="#FFFFFDE7"/>
        <Setter Property="TextColor" Value="#FFF57F17"/>
    </Style>

    <!-- Error Chip Style -->
    <Style x:Key="ErrorChipStyle" TargetType="controls:Chip" BasedOn="{StaticResource DefaultChipStyle}">
        <Setter Property="ChipBackgroundColor" Value="#FFFBE9E7"/>
        <Setter Property="TextColor" Value="#FFD32F2F"/>
    </Style>
</ResourceDictionary>
```

### Custom Template Example

```xml
<controls:Chip 
    Text="Custom Chip"
    Style="{StaticResource PrimaryChipStyle}">
    <controls:Chip.Resources>
        <Style TargetType="Border">
            <Setter Property="StrokeThickness" Value="2"/>
        </Style>
    </controls:Chip.Resources>
</controls:Chip>
```

Advanced Usage
--------------

### Dynamic Chip Generation

```csharp
public void AddDynamicChips()
{
    var newChip = new ChipModel
    {
        Text = $"New Chip {DateTime.Now:T}",
        BackgroundColor = Colors.LightBlue,
        TextColor = Colors.DarkBlue,
        IsClosable = true
    };

    ChipItems.Add(newChip);
}

public void ClearSelectedChips()
{
    SelectedItems.Clear();
    // This will automatically update the visual state
}
```

### Custom Chip Templates

```xml
<controls:ChipsGroup ChipItems="{Binding CustomChips}">
    <controls:ChipsGroup.Resources>
        <DataTemplate x:Key="CustomChipTemplate">
            <controls:Chip
                Text="{Binding Text}"
                ChipBackgroundColor="{Binding Status, Converter={StaticResource StatusToColorConverter}}"
                TextColor="White"
                CornerRadius="20"
                IsClosable="{Binding IsRemovable}"
                BadgeText="{Binding Count}"
                BadgeColor="{Binding Priority, Converter={StaticResource PriorityToColorConverter}}"/>
        </DataTemplate>
    </controls:ChipsGroup.Resources>
</controls:ChipsGroup>
```

### Validation and Error States

```csharp
public void ValidateChips()
{
    foreach (var chip in ChipItems)
    {
        if (string.IsNullOrEmpty(chip.Text))
        {
            // Mark invalid chips
            chip.BackgroundColor = Colors.LightPink;
            chip.TextColor = Colors.Red;
        }
    }
}
```


Common Issues and Solutions
---------------------------

### Chips Not Appearing

```xml
<!-- Ensure proper sizing -->
<controls:Chip HeightRequest="36" WidthRequest="120"/>
```

### Binding Not Working

```csharp
// Use ObservableCollection for dynamic updates
ChipItems = new ObservableCollection<ChipModel>();
```

### Selection Not Updating

```csharp
// Ensure TwoWay binding for IsSelected
chip.IsSelected = true; // Programmatic selection
```

### Events Not Firing

```csharp
// Check if commands are properly initialized
chip.Command = new Command(() => { /* logic */ });
```

Browser Compatibility
---------------------

Works on all .NET MAUI supported platforms:
*   ✅ iOS
*   ✅ Android
*   ✅ Windows
*   ✅ macOS


## License

MIT License © 2025 [Jorge Perales Diaz](https://shaunebu.com/)