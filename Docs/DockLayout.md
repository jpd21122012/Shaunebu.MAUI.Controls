# 🚀 Dock Layout for .NET MAUI
![Platform Support](https://img.shields.io/badge/Platforms-Android%20|%20iOS-lightgrey)
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

A professional docking panel for .NET MAUI applications

![Dock Layout Screenshot](https://jpdblog.blob.core.windows.net/apps/DockLayout.png)

## Table of Contents
1. [Features](#features)
2. [Installation](#installation)
3. [Basic Implementation](#basic-usage)
4. [Advanced Features](#advanced-features)
5. [Property Reference](#property-reference) 
6. [Troubleshooting](#troubleshooting)

---

## Features

- **Docking** - Position elements to top, left, right, bottom, or fill remaining space
- **LastChildFill** - Auto-fill remaining space with last child
- **Spacing** - Configurable gaps between docked items
- **Priority** - Control docking order precedence
- **Size Constraints** - Minimum/Maximum docked item sizes
- **Animations** - Smooth resizing transitions
- **Nesting** - Supports nested DockLayouts

## Installation

1. Add the control to your MAUI project:
   ```bash
   dotnet add package Shaunebu.MAUI.Controls
   ```

2. Register the namespace:
   ```xml
   xmlns:dock="clr-namespace:Shaunebu.MAUI.Controls;assembly=Shaunebu.MAUI.Controls.DockLayout"
   ```

## Usage

### Basic Implementation

```xml
    <controls:DockLayout
        AnimateResize="True"
        AnimationDuration="300"
        LastChildFill="True"
        Spacing="5">

        <Label
            controls:DockLayout.Dock="Top"
            controls:DockLayout.DockPriority="1"
            controls:DockLayout.MinDockSize="100,50"
            BackgroundColor="LightBlue"
            HeightRequest="50"
            Text="Top Header" />

        <BoxView
            controls:DockLayout.Dock="Left"
            controls:DockLayout.DockPriority="2"
            controls:DockLayout.MaxDockSize="150,1000"
            WidthRequest="100"
            Color="LightGray" />


        <controls:DockLayout
            controls:DockLayout.Dock="Right"
            BackgroundColor="LightGreen"
            Spacing="2"
            WidthRequest="80">

            <BoxView
                controls:DockLayout.Dock="Top"
                HeightRequest="30"
                Color="Green" />

            <BoxView controls:DockLayout.Dock="Fill" Color="DarkGreen" />
        </controls:DockLayout>

        <BoxView
            controls:DockLayout.Dock="Bottom"
            controls:DockLayout.MinDockSize="0,40"
            HeightRequest="40"
            Color="LightPink" />

        <Label
            controls:DockLayout.Dock="Fill"
            BackgroundColor="White"
            FontSize="16"
            HorizontalOptions="Center"
            Text="Main Content Area"
            VerticalOptions="Center" />

    </controls:DockLayout>
```

## API Reference

### Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Spacing` | double | 0 | Space between docked items |
| `LastChildFill` | bool | true | Whether last child fills remaining space |
| `AnimateResize` | bool | true | Enable resize animations |
| `AnimationDuration` | uint | 250 | Animation duration in milliseconds |

### Attached Properties (for child elements)

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Dock` | enum | Left | Dock position (Top/Left/Right/Bottom/Fill) |
| `DockPriority` | int | 0 | Docking order priority (higher docks first) |
| `MinDockSize` | Size | (-1,-1) | Minimum size when docked |
| `MaxDockSize` | Size | (-1,-1) | Maximum size when docked |



## Troubleshooting

**Q: My items aren't docking correctly**
*   Verify `Dock` property is set on all children
    
*   Check for conflicting `HorizontalOptions/VerticalOptions`
    
**Q: Animations aren't smooth**
*   Reduce `AnimationDuration`
    
*   Set `AnimateResize="False"` on complex layouts
    
**Q: Last child isn't filling**
*   Ensure `LastChildFill="True"`
    
*   Verify no other children are set to `Dock="Fill"`



## License

MIT License © 2025 [Jorge Perales Diaz](https://shaunebu.com/)