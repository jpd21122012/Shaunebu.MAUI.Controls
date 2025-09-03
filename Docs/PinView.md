🚀 PinView .NET MAUI
===============================

![Platform Support](https://img.shields.io/badge/Platforms-Android%20%7C%20iOS-lightgrey)  
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

A fully customizable PIN entry control for .NET MAUI with secure input, animated PIN boxes, focus animations, and commands/events for PIN completion.

![PinView Screenshot](https://jpdblog.blob.core.windows.net/apps/PinView.png)

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

*   **PIN Entry** – Secure numeric or alphanumeric PIN input
*   **Custom PIN Length** – Supports any number of boxes
*   **Secure Mode** – Show dots instead of actual characters
*   **Animated Boxes** – Focus and value animations
*   **Auto-dismiss Keyboard** – Optionally hides keyboard on completion
*   **Customizable Box UI** – Size, spacing, shape, color, and font
*   **Tap to Focus** – Tap anywhere on the box to focus the input
*   **Command & Event** – Execute code when PIN entry is complete
    

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

    <!--  Default PINView  -->
    <Label FontAttributes="Bold" Text="Default 4-digit PIN" />
    <controls:PinView
        x:Name="pinDefault"
        AutoDismissKeyboard="True"
        PINLength="4" />

    <!--  6-digit PIN with Password  -->
    <Label FontAttributes="Bold" Text="6-digit PIN with Password" />
    <controls:PinView
        x:Name="pinPassword"
        AutoDismissKeyboard="True"
        BoxBackgroundColor="#FFEFEFEF"
        BoxFocusAnimation="ZoomInOut"
        BoxFocusColor="DodgerBlue"
        IsPassword="True"
        PINLength="6"
        Color="DarkGray" />

    <!--  Custom Spacing, Size and Shape  -->
    <Label FontAttributes="Bold" Text="Custom Size, Shape, and Spacing" />
    <controls:PinView
        x:Name="pinCustom"
        BoxFocusColor="LimeGreen"
        BoxShape="RoundCorner"
        BoxSize="60"
        BoxSpacing="15"
        BoxStrokeThickness="3"
        DotSize="25"
        PINLength="4"
        Color="DarkGreen" />

    <!--  Alphanumeric PIN  -->
    <Label FontAttributes="Bold" Text="Alphanumeric Input" />
    <controls:PinView
        x:Name="pinAlpha"
        BoxFocusColor="DeepPink"
        PINInputType="AlphaNumeric"
        PINLength="5"
        Color="Purple" />

</VerticalStackLayout>
```


Advanced Features
-----------------

*   **Programmatic PIN Assignment** – Set `PINValue` and boxes animate automatically
    
*   **Box Shape & Size** – Circle or square, with configurable radius
    
*   **Focus Animation** – None, ZoomInOut, or ScaleUp
    
*   **Box Colors** – Customize border, focus, dot, and background colors
    
*   **Font Customization** – Font size, family, and attributes for PIN characters
    
*   **Tap Anywhere to Focus** – Works on Android, iOS, and WinUI



Property Reference
------------------

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `PINValue` | string | `""` | Current PIN value. Two-way binding supported |
| `PINLength` | int | `4` | Number of PIN boxes displayed |
| `PINInputType` | InputKeyboardType | `Numeric` | Keyboard type: Numeric or AlphaNumeric |
| `AutoDismissKeyboard` | bool | false | Dismiss keyboard on PIN completion |
| `IsPassword` | bool | false | Show dots instead of characters |
| `FontSize` | double | 22 | Font size of characters |
| `FontFamily` | string | null | Font family for PIN characters |
| `FontAttributes` | FontAttributes | None | Font attributes (bold, italic) |
| `DotSize` | double | 20 | Size of the dot inside the PIN box |
| `Color` | Color | Black | Primary color of border and dot |
| `BoxSize` | double | 50 | Width/height of each PIN box |
| `BoxSpacing` | double | 5 | Space between PIN boxes |
| `BoxShape` | BoxShapeType | Circle | Box shape: Circle or RoundCorner/Square |
| `BoxFocusColor` | Color | Black | Color of focused box |
| `BoxFocusAnimation` | FocusAnimationType | None | Animation type on focus |
| `BoxStrokeThickness` | double | 1 | Border thickness of the box |
| `BoxBorderColor` | Color | Black | Border color |
| `BoxBackgroundColor` | Color | Transparent | Background color of each box |
| `PINEntryCompletedCommand` | ICommand | null | Command executed when PIN entry completes |

* * *

Methods & Events
----------------

### Methods

*   `FocusBox()` – Focus the PIN input and show the keyboard
    
*   `CreateControl()` – Initialize boxes based on properties
    
*   `SetInputType(InputKeyboardType)` – Set keyboard type programmatically
    

### Event Handlers

*   `PINEntryCompleted` – Raised when PIN entry is complete
    
*   `PINView_TextChanged` – Updates PINValue and triggers animations
    
*   `HiddenTextEntry_Focused` – Handles focus animations
    
*   `HiddenTextEntry_Unfocused` – Handles unfocus animations
    

* * *

Troubleshooting
---------------

**Q: PIN boxes not showing correct value**
*   Ensure `PINValue` is bound correctly
    
*   Use `CreateControl()` if changing `PINLength` dynamically
    
**Q: Focus animations not working**
*   Set `BoxFocusAnimation` and `BoxFocusColor`
    
*   Ensure `hiddenTextEntry` is focused
    
**Q: Keyboard not showing on tap**
*   Tap anywhere on box triggers `FocusBox()`
    
*   On WinUI, a small delay is added for keyboard focus


* * *

License
-------

MIT License © 2025 [Jorge Perales Diaz](https://shaunebu.com/)