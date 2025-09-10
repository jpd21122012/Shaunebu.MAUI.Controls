🚀 BadgeView.NET MAUI
===============================

![Platform Support](https://img.shields.io/badge/Platforms-Android%20%7C%20iOS-lightgrey)  
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

A fully customizable badge control for .NET MAUI with dynamic positioning, animations, interactive tap gestures, and commands/events for badge taps.

![BadgeView Control Screenshot](https://jpdblog.blob.core.windows.net/apps/BadgeView.png)

Table of Contents
-----------------

1.  [Features](#features)
    
2.  [Installation](#installation)
    
3.  [Basic Usage](#basic-usage)
    
4.  [Advanced Features](#advanced-features)
    
5.  [Property Reference](#property-reference)
    
6.  [Troubleshooting](#troubleshooting)
    

* * *

Features
--------

*   **Dynamic Badge Text** – Shows numbers, text, or custom templates
    
*   **Badge Shapes** – Circle, Rectangle, Pill, or Custom shapes
    
*   **Badge Types** – Primary, Secondary, Success, Error, Warning, Info, Custom
    
*   **Animations** – Bounce, Pulse, Shake, Fade, Scale, or None
    
*   **Interactive Tap** – Tap events and commands supported
    
*   **Auto Hide** – Hide badge when value is empty or zero
    
*   **Custom Styling** – Background, text, stroke, corner radius, font size, and color
    
*   **Dynamic Sizing** – Badge width adjusts to text length
    
*   **Badge Positioning** – TopRight, TopLeft, BottomRight, BottomLeft
    

* * *

Installation
------------

1.  Add the control to your MAUI project:
    
  ```bash
  dotnet add package Shaunebu.MAUI.Controls
  ```

2.  Include the namespace in your XAML page:
   ```xml
   xmlns:controls="clr-namespace:Shaunebu.MAUI.Controls;assembly=Shaunebu.MAUI.Controls"
   ```

* * *

Basic Usage
-----------

```xml
<VerticalStackLayout Padding="20" Spacing="25">

    <!-- Simple Badge on a Button -->
    <controls:BadgeView
        BadgeText="5"
        BadgeType="Warning"
        Command="{Binding BadgeTappedCommand}"
        CommandParameter="Hello from badge!"
        IsInteractive="True">
        <controls:BadgeView.MainContent>
            <Button Text="Interactive"/>
        </controls:BadgeView.MainContent>
    </controls:BadgeView>

    <!-- Badge with Auto Hide -->
    <controls:BadgeView
        BadgeText="0"
        AutoHide="True"
        BadgeType="Success">
        <controls:BadgeView.MainContent>
            <Label Text="No Badge Visible"/>
        </controls:BadgeView.MainContent>
    </controls:BadgeView>

    <!-- Badge with Custom Template -->
    <controls:BadgeView
        BadgeType="Custom"
        BadgeTemplate="{StaticResource CustomBadgeTemplate}">
        <controls:BadgeView.MainContent>
            <Button Text="Custom Badge"/>
        </controls:BadgeView.MainContent>
    </controls:BadgeView>

</VerticalStackLayout>

```


Advanced Features
-----------------

*   **Programmatic Badge Text** – Change `BadgeText` dynamically and badge updates automatically
    
*   **Custom Templates** – Fully replace badge UI with `BadgeTemplate`
    
*   **Tap Command** – Execute a command or event when the badge is tapped
    
*   **Badge Animations** – Animate on update using Bounce, Pulse, Shake, Fade, or Scale
    
*   **Dynamic Sizing & Shape** – Width adjusts to text, with configurable shape, corner radius, and size
    
*   **Positioning Options** – Place badge on any corner of the content



Property Reference
------------------

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `BadgeText` | string | `"0"` | Text or number displayed on the badge |
| `BadgeType` | BadgeType | `Primary` | Visual style of the badge |
| `BadgeShape` | BadgeShape | `Circle` | Shape of the badge |
| `BadgeSize` | double | `20` | Base height of the badge |
| `FontSize` | double | `12` | Font size of badge text |
| `CornerRadius` | double | `10` | Corner radius for rectangle/pill badges |
| `BackgroundColor` | Color | `null` | Badge background color |
| `TextColor` | Color | `null` | Text color |
| `StrokeColor` | Color | `null` | Border color |
| `StrokeThickness` | double | `0` | Border thickness |
| `IsInteractive` | bool | `true` | Enable tap gestures |
| `AutoHide` | bool | `false` | Hide badge if text is empty or zero |
| `BadgeAnimation` | BadgeAnimation | `None` | Animation applied on update |
| `AnimationDuration` | int | `300` | Duration of animation in ms |
| `BadgePosition` | BadgePosition | `TopRight` | Corner of the content to attach badge |
| `Command` | ICommand | `null` | Command executed on tap |
| `CommandParameter` | object | `null` | Parameter passed to command |
| `BadgeTemplate` | DataTemplate | `null` | Custom template for badge content |
| `MainContent` | View | `null` | The main content that badge overlays |

* * *

Methods & Events
----------------

### Methods

*   `AnimateBadge()` – Triggers the configured badge animation
    
*   `VisualFeedback()` – Temporary visual feedback for taps
    

### Events

*   `Tapped` – Raised when the badge is tapped
    
*   `BadgeTapped` – Alternative event raised for badge taps
    

* * *

Troubleshooting
---------------

**Q: Tap command or event not firing**
*   Ensure `IsInteractive="True"`
    
*   Set the `Command` or subscribe to `Tapped`/`BadgeTapped`
    
*   Do not override `Content`; use `MainContent`
    
**Q: Badge not showing correct size**
*   Adjust `BadgeSize` and `FontSize`
    
*   Badge width adapts to text length automatically
    
**Q: Badge not visible**
*   If `AutoHide` is `True` and `BadgeText` is empty or zero, badge will be hidden

* * *

License
-------

MIT License © 2025 [Jorge Perales Diaz](https://shaunebu.com/)