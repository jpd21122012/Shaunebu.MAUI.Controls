# 🚀 Kanban Control for .NET MAUI
![Platform Support](https://img.shields.io/badge/Platforms-Android%20|%20iOS-lightgrey)
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

A flexible, interactive Kanban board implementation for .NET MAUI with drag-and-drop functionality.

![Kanban Board Screenshot](https://jpdblog.blob.core.windows.net/apps/kanBanBoard.png)

## Features

- 🎯 **Dynamic Columns** - Bind to any status collection (enums, strings, or custom objects)
- 🖐️ **Drag-and-Drop** - Smooth item movement between columns
- 🎨 **Fully Customizable** - Templates for cards, headers, and styling
- 📊 **Data Binding** - Works with MVVM patterns
- 📱 **Cross-Platform** - iOS, Android, Windows, Mac Catalyst

## Installation

1. Add the control to your MAUI project:
   ```bash
   dotnet add package Shaunebu.MAUI.Controls
   ```

2. Register the namespace:
   ```xml
   xmlns:kanban="clr-namespace:Shaunebu.MAUI.Controls;assembly=Shaunebu.MAUI.Controls.FloatingChatButton"
   ```

## Usage

### Basic Implementation

```xml
<kanban:KanbanBoard
    ItemsSource="{Binding Tasks}"
    StatusesSource="{Binding Statuses}"
    DragOverColor="#33FF0000"
    DragLeaveColor="#F5F5F5">
    
    <kanban:KanbanBoard.CardItemTemplate>
        <DataTemplate>
            <Frame BackgroundColor="{Binding CategoryColor}"
                   CornerRadius="8">
                <Label Text="{Binding Title}" />
            </Frame>
        </DataTemplate>
    </kanban:KanbanBoard.CardItemTemplate>
    
    <kanban:KanbanBoard.ColumnHeaderTemplate>
        <DataTemplate>
            <Label Text="{Binding}" 
                   FontAttributes="Bold"/>
        </DataTemplate>
    </kanban:KanbanBoard.ColumnHeaderTemplate>
</kanban:KanbanBoard>
```

## API Reference

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `ItemsSource` | `IEnumerable<KanbanItem>` | Collection of items to display |
| `StatusesSource` | `IEnumerable<object>` | Available statuses for columns |
| `ItemTemplate` | `DataTemplate` | Template for cards *(legacy)* |
| `CardItemTemplate` | `DataTemplate` | Preferred template for cards |
| `ColumnHeaderTemplate` | `DataTemplate` | Template for column headers |
| `DragOverColor` | `Color` | Highlight color during drag (default: #3300FF00) |
| `DragLeaveColor` | `Color` | Column background color (default: #F0F0F0) |
| `BoardBackgroundColor` | `Color` | Grid background color (default: Transparent) |

### Methods

```csharp
public void RefreshBoard() // Forces UI to reload
```

## Advanced Scenarios

### Custom Status Types

```csharp
// ViewModel
public class CustomStatus 
{
    public string DisplayName { get; set; }
    public Color HeaderColor { get; set; }
}

// Usage
Statuses = new List<CustomStatus>
{
    new CustomStatus { DisplayName = "Backlog", HeaderColor = Colors.Gray }
};
```

### Handling Drag Events

```csharp
// In your ViewModel
public ICommand ItemDroppedCommand => new Command<KanbanItem>(item =>
{
    Debug.WriteLine($"Item {item.Title} moved to {item.Status}");
});
```

## Styling

Override these styles in your `Resources.xaml`:

```xml
<Style TargetType="kanban:KanbanColumn">
    <Setter Property="BackgroundColor" Value="#FFFFFF"/>
    <Setter Property="Spacing" Value="10"/>
</Style>

<Style TargetType="kanban:KanbanCard">
    <Setter Property="BackgroundColor" Value="White"/>
    <Setter Property="Shadow" Value="Small"/>
</Style>
```

## Troubleshooting

### Items Not Appearing?
1. Verify `ItemsSource` and `StatusesSource` are populated
2. Check `CardItemTemplate` bindings match your item properties
3. Ensure `Status` property matches between items and statuses

### Drag-and-Drop Not Working?
1. Confirm `BindingContext` is set on the KanbanBoard
2. Verify your ViewModel implements `MoveItemToStatus` method
3. Check for console errors in debug output

## Example ViewModel

```csharp
public class KanbanViewModel : ObservableObject
{
    public ObservableCollection<KanbanItem> Tasks { get; } = new();
    public ObservableCollection<string> Statuses { get; } = new() { "Todo", "Done" };

    public KanbanViewModel()
    {
        // Sample data
        Tasks.Add(new KanbanItem { Title = "Fix UI Bug", Status = "Todo" });
    }
    
    public async Task MoveItemToStatus(KanbanItem item, object newStatus)
    {
        item.Status = newStatus.ToString();
        await Task.CompletedTask;
    }
}
```

## License

MIT License © 2025 [Jorge Perales Diaz](https://jpdblog.com/)