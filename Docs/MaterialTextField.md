🚀 MaterialTextFieldfor .NET MAUI
===============================

![Platform Support](https://img.shields.io/badge/Platforms-Android%20%7C%20iOS-lightgrey)  
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

A fully customizable Material Design-inspired text field for .NET MAUI with validation, floating labels, password visibility toggle, and trailing content support.

![MaterialTextField Screenshot](https://jpdblog.blob.core.windows.net/apps/MaterialTextField.png)

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

*   **Floating Label** – Animates above when entry is focused or has text
*   **Validation** – Synchronous and asynchronous validation support
*   **Password Support** – Toggle visibility with image or text
*   **Trailing Content** – Custom trailing view (icon, button, etc.)
*   **Helper / Error Text** – Displays validation messages or helper text
*   **Visual States** – Customizable border, filled or outlined variants
*   **Password Strength Bar** – Optional visual strength indicator
*   **Platform-specific UI tweaks** – Bottom line removed on Android/iOS
    

* * *

Installation
------------

1.  Add the control to your MAUI project:
    
  ```bash
  dotnet add package Shaunebu.MAUI.Controls
  ```

2.  Include the namespace in your XAML page:
   ```xml
   xmlns:controls="clr-namespace:Shaunebu.MAUI.Controls;assembly=Shaunebu.MAUI.Controls.MaterialTextField"
   ```

* * *

Basic Usage
-----------

```xml
<controls:MaterialTextField
    Title="Email"
    Text="{Binding Email}"
    Placeholder="Enter your email"
    HelperText="We will not share your email"
    IsPassword="False"
    Variant="Outlined"
    BorderThickness="1"
    CornerRadius="8"
    AccentColor="DodgerBlue"
    TrailingContent="{StaticResource SomeIcon}" />
```


* * *
Advanced Features
-----------------
*   **Validation** – Add multiple synchronous or asynchronous validators
*   **Password Toggle** – Images or text toggle for visibility
*   **Trailing Content** – Place any custom view on the trailing side
*   **Custom Colors** – Customize border, fill, error, and title colors
*   **Animation Control** – Smooth floating label animations
    

Property Reference
------------------

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Title` | string | "" | Floating label text |
| `Text` | string | "" | Entry text |
| `Placeholder` | string | "" | Placeholder inside Entry |
| `HelperText` | string | "" | Optional helper text |
| `ErrorText` | string | "" | Error message from validation |
| `IsError` | bool | false | Indicates if field is in error state |
| `IsPassword` | bool | false | Enable password mode |
| `IsPasswordVisible` | bool | false | Toggle for showing password |
| `TrailingContent` | View | null | Custom trailing content |
| `Variant` | enum | Filled | `Outlined` or `Filled` visual style |
| `BorderThickness` | double | 1 | Border thickness for outlined variant |
| `CornerRadius` | double | 4 | Rounded corner radius |
| `AccentColor` | Color | Blue | Color when focused |
| `TitleColor` | Color | Gray | Color of unfocused floating label |
| `FilledColor` | Color | LightGray | Background color for filled variant |
| `PasswordStrengthEnabled` | bool | false | Show password strength bar |

* * *

Methods & Events
----------------

### Event Handlers

*   `Entry_TextChanged` → **Events / Text Changed**
    

### Validation

*   `RunValidations` → **Validation**
    
*   `RunAsyncValidation` → **Validation / Async**
    

### UI Updates / Visuals

*   `ApplyVisuals` → **Visual Updates**
    
*   `UpdateBottomLine` → **Visual Updates**
    
*   `UpdateFloatingLabel(bool animate)` → **Visual Updates / Floating Label**
    
*   `UpdateHelperOrError` → **Visual Updates / Helper/Error**
    
*   `UpdateVisualState(bool isFocused)` → **Visual Updates / State Management**
    
*   `ApplyIconVisibility` → **Visual Updates / Icon**
    
*   `ApplyPasswordStrength` → **Visual Updates / Password Strength**
    
*   `UpdateTrailing` → **Visual Updates / Trailing Elements**
    
*   `ApplyTrailing` → **Visual Updates / Trailing Elements**
    
*   `UpdateToggleImage` → **Visual Updates / Password Toggle**
    
*   `UpdateToggleText` → **Visual Updates / Password Toggle**
    
*   `ApplyPasswordVisibility` → **Visual Updates / Password Visibility**
    

* * *

Troubleshooting
---------------

**Q: Floating label not animating**
*   Ensure `Title` is set and `Text` is bound correctly
    
*   Check `UpdateFloatingLabel` is being called on `TextChanged` and `Focus` events
    
**Q: Error messages not showing**
*   Make sure `Validations` are added and `RunValidations` is triggered
    
*   Use `ErrorText` or helper text appropriately
    
**Q: Trailing content not visible**
*   Confirm `TrailingContent` is assigned
    
*   Ensure `IsPassword` logic does not override trailing view

* * *

License
-------

MIT License © 2025 [Jorge Perales Diaz](https://shaunebu.com/)