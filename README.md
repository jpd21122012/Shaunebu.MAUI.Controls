# MAUI Controls Library by Shaunebu
![NuGet Version](https://img.shields.io/nuget/v/Shaunebu.MAUI.Controls?color=blue&label=NuGet)
![Platform Support](https://img.shields.io/badge/Platforms-Android%20|%20iOS-lightgrey)
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

<a href="https://www.buymeacoffee.com/jorgepd" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" align="left" style="height: 37px !important;width: 170px !important;" ></a>

<br>

![MAUI Controls Showcase](https://jpdblog.blob.core.windows.net/apps/ShaunebuControls.png)  

## 📦 Included Controls

| Control | Description | Documentation |
|---------|-------------|---------------|
| [**FloatingChatButton**](#floatingchatbutton) | Smart circular action button with chat features | [Details](Docs/FloatingChatButton.md) |
| [**KanbanBoard**](#kanbanboard) | Drag-and-drop task management board | [Details](Docs/KanbanBoard.md) |
| [**DockLayout**](#docklayout) |  Edge-docking container with priority and animation support | [Details](Docs/DockLayout.md) |
| [**SegmentedControl**](#segmentedcontrol) |  A customizable and feature-rich segmented control | [Details](Docs/SegmentedControl.md) |
| [**Custom Switch**](#customswitch) | A fully customizable switch control | [Details](Docs/CustomSwitch.md) |
| [**Chips Control**](#chipscontrol) | Customize chip appearance with a lot of options | [Details](Docs/Chips.md) |
| [**Touch Effects**](#toucheffects) | Provides advanced touch interaction capabilities for MAUI controls | [Details](Docs/TouchEffects.md) |


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



## ✨ DockLayout
`Shaunebu.Controls.DockLayout`

### Features
- Edge Docking (Top/Left/Right/Bottom/Fill)
- Auto-fill last child (LastChildFill)
- Priority Control (DockPriority)
- Smart Spacing between elements
- Animated Transitions
- Size Constraints (Min/Max)

```
<controls:DockLayout 
    Spacing="5" 
    LastChildFill="True"
    AnimateResize="True">
    
    <!-- Header -->
    <Label DockLayout.Dock="Top" 
           Text="Header" 
           HeightRequest="50"/>

    <!-- Sidebar -->
    <BoxView DockLayout.Dock="Left" 
             WidthRequest="100"
             DockLayout.MinDockSize="80,0"/>

    <!-- Main Content -->
    <Frame DockLayout.Dock="Fill"
           CornerRadius="10">
        <Label Text="Content"/>
    </Frame>
</controls:DockLayout>
```

## ✨ SegmentedControl
`Shaunebu.Controls.SegmentedControl`

### Features
- Fully customizable segment content
- Disable specific segments or entire control
- Configurable selection animations
- Icons, text, or any custom content
- Horizontal or vertical layout
- Dynamic item updates

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



## ✨ CustomSwitch
`Shaunebu.Controls.CustomSwitch`

### Features
- Display "ON"/"OFF" or any custom text
- Configurable selection animations with bounce effects
- Different colors for on/off states
- Customizable shadow for the thumb
- Swipe to toggle functionality
- Adjustable track and thumb dimensions

```xml
<controls:CustomSwitch
    IsToggled="{Binding IsSwitchOn, Mode=TwoWay}"
    OnColor="#FF4CAF50"
    OffColor="#FF9E9E9E"
    ThumbColor="#FFFFFFFF"
    ThumbSize="24"
    ThumbCornerRadius="12"
    TrackCornerRadius="14"
    TrackWidth="50"
    TrackHeight="30"/>
```


## ✨ Chip Control
`Shaunebu.Controls.Chip`

### Features
- Text, icon, closable, and badge chips
- Single, multiple, or no selection
- Full control over colors, borders, and appearance
- Number badges and status indicators
- Click, close, and selection events
- Full data binding support

```xml
<controls:Chip 
    Text="Basic Chip"
    ChipBackgroundColor="#FFE0E0E0"
    TextColor="Black"
    CornerRadius="16"/>
```

```xml
<controls:ChipsGroup 
    ChipItems="{Binding ChipModels}"
    SelectionMode="Multiple"/>
```


## ✨ TouchEffects
`Shaunebu.Controls.TouchEffect`


```xml
<ContentPage xmlns:effects="clr-namespace:YourLibrary.Effects;assembly=YourLibrary">
    
    <Button Text="Tap Me">
        <Button.Effects>
            <effects:TouchEffect 
                PressedBackgroundColor="LightBlue"
                PressedOpacity="0.7"
                Command="{Binding TapCommand}" />
        </Button.Effects>
    </Button>

</ContentPage>
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

📧 [jorge.p@jpdblog.com](https://mailto:support@shaunebu.com)  
🐛 [GitHub Issues](https://github.com/jpd21122012/Shaunebu.MAUI.Controls/issues)

----------
### 📄 License


MIT License © 2025 [Jorge Perales Diaz](https://jpdblog.com/)