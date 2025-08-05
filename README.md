# MAUI Controls Library by Shaunebu
![NuGet Version](https://img.shields.io/nuget/v/Shaunebu.MAUI.Controls?color=blue&label=NuGet)
![Platform Support](https://img.shields.io/badge/Platforms-Android%20|%20iOS-lightgrey)
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

![MAUI Controls Showcase](https://jpdblog.blob.core.windows.net/apps/ShaunebuControls.png)  

## 📦 Included Controls

| Control | Description | Documentation |
|---------|-------------|---------------|
| [**FloatingChatButton**](#floatingchatbutton) | Smart circular action button with chat features | [Details](Docs/FloatingChatButton.md)  |
| [**KanbanBoard**](#kanbanboard) | Drag-and-drop task management board | [Details](Docs/KanbanBoard.md) |

---

## ✨ FloatingChatButton
`Shaunebu.Controls.FloatingChatButton`

### Features
- 360° position anchoring
- Unread message counter badge
- Pulse animation effect
- Customizable SVG/PNG icon support

```
<controls:FloatingChatButton
    BadgeCount="{Binding UnreadMessages}"
    ButtonColor="#FF4081"
    Command="{Binding OpenChatCommand}"/>
```

## ✨ Kanban Board
`Shaunebu.Controls.KanbanBoard`

### Features
- Dynamic column generation
- Smooth drag-and-drop operations
- Custom card and header templates
- Status tracking with visual indicators

```
<controls:KanbanBoard
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


## 🚀 Getting Started
### Installation
```
dotnet add package Shaunebu.MAUI.Controls
```

Basic Usage
Add the namespace:

```
xmlns:controls="clr-namespace:Shaunebu.MAUI.Controls;assembly=Shaunebu.MAUI.Controls"
```
Use any control:

xml
```
<controls:FloatingChatButton 
    Command="{Binding OpenChatCommand}"
    Icon="chat.png"/>
```

### 🎨 Customization
All controls support:

- Styles via ResourceDictionary
- Dynamic theming (light/dark mode)
- Platform-specific tweaks using OnPlatform


Example:

```
<Style TargetType="controls:FloatingChatButton">
    <Setter Property="BackgroundColor" Value="{AppThemeBinding Light=White, Dark=#222222}"/>
</Style>
```

    

### ⁉️ Support

Report issues:  

📧 [jorge.p@jpdblog.com](https://mailto:jorge.p@jpdblog.com)  
🐛 [GitHub Issues](https://github.com/jpd21122012/FloatingChatButton/issues)

----------
### 📄 License


MIT License © 2025 [Jorge Perales Diaz](https://jpdblog.com/)