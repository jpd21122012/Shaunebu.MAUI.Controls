# 🚀 Custom Switch Control for .NET MAUI
![Platform Support](https://img.shields.io/badge/Platforms-Android%20|%20iOS-lightgrey)
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

A fully customizable switch control for .NET MAUI

![Dock Layout Screenshot](https://jpdblog.blob.core.windows.net/apps/CustomSwitch.png)

## Table of Contents
1. [Features](#features)
2. [Installation](#installation)
3. [Basic Implementation](#basic-usage)
4. [Advanced Features](#advanced-features)
5. [Property Reference](#property-reference) 
6. [Troubleshooting](#troubleshooting)

---

## Features

*   ✅ **Custom Text Labels** - Display "ON"/"OFF" or any custom text
*   ✅ **Smooth Animations** - Configurable selection animations with bounce effects
*   ✅ **Dual Thumb Colors** - Different colors for on/off states
*   ✅ **Shadow Effects** - Customizable shadow for the thumb
*   ✅ **Drag Gesture Support** - Swipe to toggle functionality
*   ✅ **Custom Sizing** - Adjustable track and thumb dimensions
*   ✅ **Disabled State** - Visual and functional disabled state
*   ✅ **Event Support** - Toggled event and Command support
*   ✅ **Two-Way Binding** - Full MVVM support
*   ✅ **Custom Corner Radius** - Adjustable rounding for track and thumb
*   ✅ **Text Styling** - Customizable text color and font size

## Installation

1. Add the control to your MAUI project:
   ```bash
   dotnet add package Shaunebu.MAUI.Controls
   ```

2. Register the namespace:
   ```xml
   xmlns:kanban="clr-namespace:Shaunebu.MAUI.Controls;assembly=Shaunebu.MAUI.Controls.CustomSwitch"
   ```

## Usage

### Basic Implementation

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

## API Reference

### Core Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `IsToggled` | `bool` | `false` | Switch state (TwoWay binding) |
| `Command` | `ICommand` | `null` | Command to execute on toggle |
| `CommandParameter` | `object` | `null` | Command parameter |
| `IsEnabled` | `bool` | `true` | Enable/disable the switch |

### Visual Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `OnColor` | `Color` | `#FF4CAF50` | Track color when on |
| `OffColor` | `Color` | `#FF9E9E9E` | Track color when off |
| `ThumbColor` | `Color` | `White` | Default thumb color |
| `ThumbOnColor` | `Color` | `null` | Thumb color when on (optional) |
| `ThumbOffColor` | `Color` | `null` | Thumb color when off (optional) |
| `ThumbSize` | `double` | `24` | Thumb diameter |
| `ThumbCornerRadius` | `double` | `12` | Thumb corner radius |
| `TrackCornerRadius` | `double` | `14` | Track corner radius |
| `TrackWidth` | `double` | `50` | Total switch width |
| `TrackHeight` | `double` | `30` | Total switch height |

### Text Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `ShowText` | `bool` | `false` | Show text labels |
| `OnText` | `string` | `"ON"` | Text when switch is on |
| `OffText` | `string` | `"OFF"` | Text when switch is off |
| `TextColor` | `Color` | `White` | Text color |
| `TextFontSize` | `double` | `10` | Text font size |

### Animation & Effects

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `AnimationDuration` | `uint` | `100` | Animation duration in ms |
| `ThumbShadow` | `Shadow` | `null` | Thumb shadow effect |



Usage Examples
--------------

### Basic Switch

```xml
<controls:CustomSwitch 
    IsToggled="{Binding IsEnabled}"
    OnColor="#FF4CAF50"
    OffColor="#FF9E9E9E"
    ThumbColor="#FFFFFFFF"/>
```

### Switch with Text Labels

```xml
<controls:CustomSwitch 
    IsToggled="{Binding NotificationsEnabled}"
    ShowText="True"
    OnText="YES"
    OffText="NO"
    OnColor="#FF2196F3"
    OffColor="#FF9E9E9E"
    TextColor="White"
    TextFontSize="9"
    TrackWidth="60"/>
```

### Custom Styled Switch

```xml
<controls:CustomSwitch 
    IsToggled="{Binding DarkMode}"
    OnColor="#FF333333"
    OffColor="#FFCCCCCC"
    ThumbOnColor="#FFFFFFFF"
    ThumbOffColor="#FFE0E0E0"
    ThumbSize="28"
    TrackHeight="35"
    TrackWidth="65">
    <controls:CustomSwitch.ThumbShadow>
        <Shadow Brush="Black" Offset="0,2" Opacity="0.3" Radius="4"/>
    </controls:CustomSwitch.ThumbShadow>
</controls:CustomSwitch>
```

### Disabled Switch

```xml
<controls:CustomSwitch 
    IsToggled="{Binding IsActive}"
    OnColor="#FFFF9800"
    OffColor="#FF9E9E9E"
    IsEnabled="False"/>
```

### Large Switch with Animation

```xml
<controls:CustomSwitch 
    IsToggled="{Binding LargeOption}"
    OnColor="#FF9C27B0"
    OffColor="#FFE1BEE7"
    ThumbSize="32"
    TrackHeight="40"
    TrackWidth="70"
    AnimationDuration="200"/>
```

Event Handling
--------------

### XAML Event Handling

```xml
<controls:CustomSwitch 
    IsToggled="{Binding IsToggled}"
    Toggled="OnSwitchToggled"/>
```

```csharp
private void OnSwitchToggled(object sender, ToggledEventArgs e)
{
    Console.WriteLine($"Switch toggled: {e.Value}");
    // Handle toggle event
}
```

### Command Binding

```xml
<controls:CustomSwitch 
    IsToggled="{Binding IsToggled}"
    Command="{Binding SwitchToggledCommand}"
    CommandParameter="{Binding SomeParameter}"/>
```

ViewModel Implementation
------------------------

```csharp
public class MainViewModel : INotifyPropertyChanged
{
    private bool _isEnabled;
    private bool _notificationsEnabled;
    private bool _darkMode;

    public bool IsEnabled
    {
        get => _isEnabled;
        set => SetProperty(ref _isEnabled, value);
    }

    public bool NotificationsEnabled
    {
        get => _notificationsEnabled;
        set => SetProperty(ref _notificationsEnabled, value);
    }

    public bool DarkMode
    {
        get => _darkMode;
        set => SetProperty(ref _darkMode, value);
    }

    public ICommand SwitchToggledCommand => new Command<bool>((isToggled) =>
    {
        Console.WriteLine($"Switch toggled: {isToggled}");
        // Handle command execution
    });

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
```

Customization Guide
-------------------

### Custom Text Templates

```xml
<controls:CustomSwitch 
    IsToggled="{Binding SpanishMode}"
    ShowText="True"
    OnText="SÍ"
    OffText="NO"
    OnColor="#FF4CAF50"
    TextColor="White"
    TextFontSize="11"/>
```

### Different Sizes

```xml
<!-- Small Switch -->
<controls:CustomSwitch 
    ThumbSize="20"
    TrackHeight="25"
    TrackWidth="45"
    ThumbCornerRadius="10"
    TrackCornerRadius="12"/>

<!-- Large Switch -->
<controls:CustomSwitch 
    ThumbSize="32"
    TrackHeight="45"
    TrackWidth="80"
    ThumbCornerRadius="16"
    TrackCornerRadius="20"/>
```

### Custom Colors

```xml
<controls:CustomSwitch 
    OnColor="#FFFF5252"    <!-- Red -->
    OffColor="#FFFFCCBC"   <!-- Light Red -->
    ThumbOnColor="#FFFFFFFF"
    ThumbOffColor="#FFF5F5F5"/>
```

Animation Control
-----------------

### Custom Animation Duration

```xml
<controls:CustomSwitch 
    AnimationDuration="50"    <!-- Fast -->
    AnimationDuration="200"   <!-- Medium -->
    AnimationDuration="500"   <!-- Slow -->
/>
```

### No Animation

```xml
<controls:CustomSwitch 
    AnimationDuration="0"/>
```

Advanced Features
-----------------

### Drag Gesture Support

The switch supports swipe gestures - users can drag the thumb to toggle the switch.

### Shadow Effects

```xml
<controls:CustomSwitch>
    <controls:CustomSwitch.ThumbShadow>
        <Shadow Brush="Black" Offset="0,2" Opacity="0.3" Radius="4"/>
    </controls:CustomSwitch.ThumbShadow>
</controls:CustomSwitch>
```

### Different Thumb Colors for States

```xml
<controls:CustomSwitch 
    ThumbOnColor="#FFFFFFFF"    <!-- White when on -->
    ThumbOffColor="#FFE0E0E0"/> <!-- Light gray when off -->
```



## License

MIT License © 2025 [Jorge Perales Diaz](https://shaunebu.com/)