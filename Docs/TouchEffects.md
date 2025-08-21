# 🚀 Touch Effects for .NET MAUI
![Platform Support](https://img.shields.io/badge/Platforms-Android%20|%20iOS-lightgrey)
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

TouchEffect is a comprehensive .NET MAUI library that provides advanced touch interaction capabilities for MAUI controls. It offers rich visual feedback, hover support, long-press gestures, and customizable animations across Android and iOS platforms.

<!--![Touch Effects](https://jpdblog.blob.core.windows.net/apps/TouchEffect.png)-->

## Table of Contents
1. [Installation](#installation)
2. [Basic Implementation](#basic-usage)
3. [Advanced Features](#advanced-features)
4. [Property Reference](#property-reference) 

---

## Installation

1. Add the control to your MAUI project:
   ```bash
   dotnet add package Shaunebu.MAUI.Controls
   ```

2. Register the namespace:
   ```xml
   xmlns:effects="clr-namespace:Shaunebu.MAUI.Controls;assembly=Shaunebu.MAUI.Controls.Effects"
   ```

Usage
-----

### Basic XAML Usage

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

### C# Usage

```csharp
var button = new Button { Text = "Tap Me" };
var touchEffect = new TouchEffect
{
    PressedBackgroundColor = Colors.LightBlue,
    PressedOpacity = 0.7
};
touchEffect.Completed += OnTouchCompleted;
button.Effects.Add(touchEffect);
```

Properties
----------

### Interaction Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `IsAvailable` | `bool` | `true` | Enables/disables the touch effect |
| `ShouldMakeChildrenInputTransparent` | `bool` | `true` | Makes child elements input transparent |
| `Command` | `ICommand` | `null` | Command to execute on tap |
| `LongPressCommand` | `ICommand` | `null` | Command to execute on long press |
| `CommandParameter` | `object` | `null` | Parameter for the command |
| `LongPressCommandParameter` | `object` | `null` | Parameter for long press command |
| `LongPressDuration` | `int` | `500` | Duration in ms for long press detection |
| `DisallowTouchThreshold` | `int` | `0` | Movement threshold to cancel touch |

### Visual State Properties

| Property | Type | Description |
| --- | --- | --- |
| `NormalBackgroundColor` | `Color` | Background color in normal state |
| `HoveredBackgroundColor` | `Color` | Background color when hovered |
| `PressedBackgroundColor` | `Color` | Background color when pressed |
| `NormalOpacity` | `double` | Opacity in normal state (0.0-1.0) |
| `HoveredOpacity` | `double` | Opacity when hovered |
| `PressedOpacity` | `double` | Opacity when pressed |
| `NormalScale` | `double` | Scale factor in normal state |
| `HoveredScale` | `double` | Scale factor when hovered |
| `PressedScale` | `double` | Scale factor when pressed |

### Animation Properties

| Property | Type | Description |
| --- | --- | --- |
| `AnimationDuration` | `int` | Default animation duration (ms) |
| `AnimationEasing` | `Easing` | Default easing function |
| `PressedAnimationDuration` | `int` | Pressed state animation duration |
| `PressedAnimationEasing` | `Easing` | Pressed state easing function |
| `NormalAnimationDuration` | `int` | Normal state animation duration |
| `NormalAnimationEasing` | `Easing` | Normal state easing function |
| `HoveredAnimationDuration` | `int` | Hovered state animation duration |
| `HoveredAnimationEasing` | `Easing` | Hovered state easing function |
| `PulseCount` | `int` | Number of pulses for pulse animation |

### Native Animation Properties

| Property | Type | Description |
| --- | --- | --- |
| `NativeAnimation` | `bool` | `false` | Enable platform-native ripple effect |
| `NativeAnimationColor` | `Color` | Ripple effect color |
| `NativeAnimationRadius` | `int` | Ripple effect radius |
| `NativeAnimationShadowRadius` | `int` | Shadow radius for native effect |
| `NativeAnimationBorderless` | `bool` | `false` | Use borderless ripple effect |

### Read-only Status Properties

| Property | Type | Description |
| --- | --- | --- |
| `Status` | `TouchStatus` | Current touch status (read-only) |
| `State` | `TouchState` | Current visual state (read-only) |
| `InteractionStatus` | `TouchInteractionStatus` | Current interaction status (read-only) |
| `HoverStatus` | `HoverStatus` | Current hover status (read-only) |
| `HoverState` | `HoverState` | Current hover visual state (read-only) |
| `IsToggled` | `bool?` | Toggle state (read/write) |

Events
------

```csharp
var touchEffect = new TouchEffect();

// Fires when touch status changes
touchEffect.StatusChanged += (s, e) => 
{
    Console.WriteLine($"Touch status: {e.Status}");
};

// Fires when visual state changes
touchEffect.StateChanged += (s, e) => 
{
    Console.WriteLine($"Visual state: {e.State}");
};

// Fires when interaction status changes
touchEffect.InteractionStatusChanged += (s, e) => 
{
    Console.WriteLine($"Interaction status: {e.InteractionStatus}");
};

// Fires when hover status changes
touchEffect.HoverStatusChanged += (s, e) => 
{
    Console.WriteLine($"Hover status: {e.Status}");
};

// Fires when hover visual state changes
touchEffect.HoverStateChanged += (s, e) => 
{
    Console.WriteLine($"Hover state: {e.State}");
};

// Fires when tap is completed
touchEffect.Completed += (s, e) => 
{
    Console.WriteLine($"Tap completed with parameter: {e.Parameter}");
};

// Fires when long press is completed
touchEffect.LongPressCompleted += (s, e) => 
{
    Console.WriteLine($"Long press completed with parameter: {e.Parameter}");
};
```

Advanced Examples
-----------------

### Custom Long Press with Visual Feedback

```xml
<Image Source="image.jpg">
    <Image.Effects>
        <effects:TouchEffect
            LongPressDuration="1000"
            PressedBackgroundColor="#40000000"
            PressedScale="0.95"
            LongPressCommand="{Binding DeleteCommand}"
            LongPressCommandParameter="{Binding .}"
            NativeAnimation="True"
            NativeAnimationColor="Red">
        </effects:TouchEffect>
    </Image.Effects>
</Image>
```

### Toggle Button with State Management

```xml
<Frame>
    <Frame.Effects>
        <effects:TouchEffect
            IsToggled="{Binding IsSelected}"
            NormalBackgroundColor="White"
            PressedBackgroundColor="LightGray"
            HoveredBackgroundColor="#F0F0F0"
            NormalScale="1.0"
            PressedScale="0.98"
            AnimationDuration="200"
            Command="{Binding ToggleSelectionCommand}">
        </effects:TouchEffect>
    </Frame.Effects>
    
    <Label Text="Toggle Me" />
</Frame>
```

### Custom Hover Effects

```xml
<Border>
    <Border.Effects>
        <effects:TouchEffect
            HoveredBackgroundColor="#10000000"
            HoveredTranslationY="-2"
            HoveredAnimationDuration="150"
            PressedBackgroundColor="#20000000"
            PressedTranslationY="0"
            PressedAnimationDuration="100">
        </effects:TouchEffect>
    </Border.Effects>
    
    <Label Text="Hover over me" />
</Border>
```

Platform-Specific Notes
-----------------------

### Android

*   Uses native ripple effects when `NativeAnimation=true`
    
*   Supports hover on devices with mouse/trackpad
    
*   Full accessibility support
    

### iOS

*   Custom hover effects on iOS 13+
    
*   Smooth animations with UIKit
    
*   3D touch support on capable devices


## License

MIT License © 2025 [Jorge Perales Diaz](https://shaunebu.com/)