🚀 Shimmer for .NET MAUI
===============================

![Platform Support](https://img.shields.io/badge/Platforms-Android%20%7C%20iOS-lightgrey)  
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

A lightweight shimmer/skeleton loader control for .NET MAUI.

![Shimmer Gif](https://jpdblog.blob.core.windows.net/apps/Shimmer.gif)

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

*   **Any View Support** – Wrap `Image`, `Label`, `Layouts`, or custom content
*   **Shapes** – Built-in `Rectangle`, `RoundedRectangle`, `Circle` placeholders
*   **CornerRadius** – Fully adjustable corner radius
*   **Gradient Animation** – Smooth shimmer animation over content
*   **Animation Direction** – LeftToRight, RightToLeft, TopToBottom, BottomToTop
*   **Shimmer Customization** – Control `ShimmerSpeed`, `ShimmerWidth`, `ShimmerColor`, `BaseColor`
*   **Placeholder Template** – Provide custom skeleton layout
*   **ShimmerOverlay** – Optional overlay for custom shimmer visuals
    

* * *

Installation
------------

1.  Add the control to your MAUI project:
    
  ```bash
  dotnet add package Shaunebu.MAUI.Controls
  ```

2.  Include the namespace in your XAML page:
   ```xml
   xmlns:controls="clr-namespace:Shaunebu.MAUI.Controls;assembly=Shaunebu.MAUI.Controls.ShimmerWrapper"
   ```

* * *

Basic Usage
-----------

```xml
<controls:ShimmerWrapper
    IsLoading="True"
    Shape="RoundedRectangle"
    CornerRadius="16"
    ShimmerSpeed="300"
    ShimmerWidth="0.25"
    ShimmerColor="#80FFFFFF"
    BaseColor="#20000000"
    AnimationDirection="LeftToRight"
    HeightRequest="40"
    WidthRequest="200"/>
```

### Wrapping an Image

`<controls:ShimmerWrapper IsLoading="True">     <Image         Source="dotnet_bot.png"         Aspect="AspectFill"         HeightRequest="100"         WidthRequest="100"/> </controls:ShimmerWrapper>`

### Custom Shimmer Overlay

```xml
<controls:ShimmerWrapper IsLoading="True">
    <Image
        Source="dotnet_bot.png"
        Aspect="AspectFill"
        HeightRequest="100"
        WidthRequest="100"/>
</controls:ShimmerWrapper>
```

* * *

Advanced Features
-----------------

*   **PlaceholderTemplate** – Supply a `DataTemplate` for complex skeleton layouts
*   **AnimationDirection** – Customize shimmer flow direction
*   **ShimmerSpeed** – Adjust the speed of shimmer animation
*   **ShimmerWidth** – Adjust width of the shimmer highlight
*   **CornerRadius & Shape** – Control rounded corners and predefined shapes
    

* * *

Property Reference
------------------

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `IsLoading` | bool | false | Enable or disable shimmer animation |
| `Shape` | ShimmerShape | None | Predefined shape: `Circle`, `RoundedRectangle`, `Rectangle` |
| `CornerRadius` | float | 12 | Radius for rounded corners |
| `AnimationDirection` | ShimmerDirection | LeftToRight | Direction of shimmer animation |
| `ShimmerSpeed` | int | 1000 | Shimmer animation speed in ms |
| `ShimmerWidth` | double | 0.3 | Width of shimmer gradient (0–1 = percentage) |
| `ShimmerColor` | Color | #80FFFFFF | Color of shimmer highlight |
| `BaseColor` | Color | #20000000 | Background color behind shimmer |
| `PlaceholderTemplate` | DataTemplate | null | Template for custom skeleton layout |
| `ShimmerOverlay` | View | null | Optional custom overlay view |

* * *

Troubleshooting
---------------

**Q: Shimmer not visible**
*   Ensure `IsLoading="True"`
    
*   Verify that the wrapped view has non-zero `HeightRequest`/`WidthRequest`
    
**Q: Shimmer animation is too fast/slow**
*   Adjust `ShimmerSpeed` property (higher = slower)
    
**Q: Gradient overflow corners**
*   Set `CornerRadius` to match wrapped content
    
*   Use `RoundedRectangle` or `Circle` shapes
    
**Q: Using Image or Layout directly**
*   Ensure `_userContent` is properly set or wrapped in `ShimmerWrapper`
    

* * *

License
-------

MIT License © 2025 [Jorge Perales Diaz](https://shaunebu.com/)