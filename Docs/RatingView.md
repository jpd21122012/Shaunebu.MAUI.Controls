🚀 PinView .NET MAUI
===============================

![Platform Support](https://img.shields.io/badge/Platforms-Android%20%7C%20iOS-lightgrey)  
![MAUI Version](https://img.shields.io/badge/.NET%20MAUI-%3E%3D9.0-blueviolet)

A fully customizable, graphics-based rating control for .NET MAUI. Supports fractional ratings, custom shapes, colors, spacing, and stroke styles. Ideal for star ratings, hearts, or any custom SVG path shape.

![Rating Control Screenshot](https://jpdblog.blob.core.windows.net/apps/RatingView.png)

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

*   **Fractional Ratings** – Supports partial fills for half or decimal ratings.
    
*   **Custom Shapes** – Render any SVG path as rating items.
    
*   **Adjustable Item Count** – Default 5 items, can be increased or decreased.
    
*   **Customizable Colors** – Separate fill colors for rated and unrated items.
    
*   **Stroke Options** – Set stroke color and width for outline effect.
    
*   **Spacing & Size** – Control individual item size and spacing.
    
*   **Read-only Mode** – Disable user interaction for display-only ratings.
    
*   **Custom Templates** – Provide any UI per rating item via `ItemTemplate`.
    
*   **Graphics-based Rendering** – Smooth vector rendering on all platforms.
    

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
<VerticalStackLayout Padding="20" Spacing="30">

<!--  Default  -->
<Label FontSize="18" Text="Default (5 stars, editable)" />
<controls:RatingView
MaximumRating="5"
Rating="3"
Shape="Star" />

<!--  Read-only  -->
<Label FontSize="18" Text="ReadOnly (locked)" />
<controls:RatingView
IsReadOnly="True"
MaximumRating="5"
Rating="4.5"
Shape="Star" />

<!--  Different colors  -->
<Label FontSize="18" Text="Custom colors (Red / Gray)" />
<controls:RatingView
EmptyShapeColor="LightGray"
FillColor="Red"
MaximumRating="5"
Rating="2.5"
Shape="Star"
ShapeBorderColor="Black"
ShapeBorderThickness="1"
ShapeDiameter="50" />

<!--  Different shapes  -->
<Label FontSize="18" Text="Hearts!" />
<controls:RatingView
EmptyShapeColor="LightGray"
FillColor="HotPink"
MaximumRating="5"
Rating="3"
Shape="Heart" />

<!--  Many items  -->
<Label FontSize="18" Text="10 Circles" />
<controls:RatingView
FillColor="Orange"
MaximumRating="10"
Rating="7.5"
Shape="Circle"
ShapeDiameter="25"
Spacing="4" />

<!--  Binding Example  -->
<Label FontSize="18" Text="TwoWay Binding Example" />
<controls:RatingView
EmptyShapeColor="LightGray"
FillColor="Gold"
MaximumRating="5"
Rating="{Binding UserRating, Mode=TwoWay}"
Shape="Star" />
<Label
FontSize="16"
Text="{Binding UserRating, StringFormat='User rating: {0}'}"
TextColor="Gray" />

<!--  Compact mini rating  -->
<Label FontSize="18" Text="Compact (small, tight spacing)" />
<controls:RatingView
MaximumRating="5"
Rating="1.5"
Shape="Star"
ShapeDiameter="20"
Spacing="2" />

<!--  Custom Item Template Example  -->
<Label FontSize="18" Text="Custom Item Template Example " />
<controls:RatingView
x:Name="MyRating"
MaximumRating="5"
Rating="3"
Spacing="10">
    <controls:RatingView.ItemTemplate>
        <DataTemplate>
            <Border
            Padding="5"
            BackgroundColor="LightGray"
            StrokeShape="RoundRectangle 10">
                <Label
                HorizontalOptions="Center"
                Text="{Binding}"
                TextColor="DarkBlue"
                VerticalOptions="Center" />
            </Border>
        </DataTemplate>
    </controls:RatingView.ItemTemplate>
</controls:RatingView>

</VerticalStackLayout>
```


Advanced Features
-----------------

*   Fractional Rating Support – `Rating` can be any decimal (e.g., 2.5).
    
*   Custom Shapes – Use the `Shape` or `CustomShapePath` property to define SVG path shapes.
    
*   Styling – Adjust `ShapeDiameter`, `Spacing`, `FillColor`, `EmptyShapeColor`, `ShapeBorderColor`, `ShapeBorderThickness`.
    
*   Read-Only Display – Set `IsReadOnly` to true to prevent interaction.
    
*   Dynamic Updates – Changing any property automatically re-renders the control.
    
*   Custom Templates – Provide `ItemTemplate` to render custom UI per rating item.



Property Reference
------------------

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `MaximumRating` | int | 5 | Number of items in the rating control |
| `Rating` | double | 0 | Current rating value (can be fractional) |
| `Spacing` | double | 10 | Space between items |
| `FillColor` | Color | Yellow | Fill color for rated portion |
| `EmptyShapeColor` | Color | Transparent | Fill color for unrated portion |
| `Shape` | RatingViewShape | Star | Predefined shape type (Star, Heart, Circle, etc.) |
| `CustomShapePath` | string | null | SVG path defining a custom shape |
| `ShapeDiameter` | double | 20 | Diameter / size of each shape |
| `ShapeBorderColor` | Color | Grey | Stroke color of shapes |
| `ShapeBorderThickness` | double | 1 | Width of the stroke |
| `IsReadOnly` | bool | false | Disables interaction when true |
| `ItemTemplate` | DataTemplate | null | Template for custom content per item |

* * *

Methods & Events
----------------

### Methods

*   `UpdateShapeFills(RatingViewFillOption fillOption)` – Updates each item's visual state according to `Rating`.
    
*   `ChangeShape(string shapePath)` – Changes the SVG path of all items.
    

### Events

*   `RatingChanged` – Fired whenever the `Rating` changes.
    
*   If using `ItemTemplate`, the template content can implement `IRatingItem.UpdateState(bool isFilled, double fillPercent)` for visual updates.
    

* * *

Troubleshooting
---------------

**Q: Items not rendering correctly**
*   Ensure `Shape` or `CustomShapePath` is valid.
    
*   If using `ItemTemplate`, ensure your template has a Border or implement `IRatingItem`.
    
**Q: Rating changes but nothing visually happens**
*   Custom templates without `Shape` or `IRatingItem` **cannot automatically change color**. You must handle visual feedback manually inside the template.
    
**Q: Colors not updating**
*   Check `FillColor` and `EmptyShapeColor`.
    
**Q: Control size too small**
*   Adjust `ShapeDiameter`, `ShapeBorderThickness`, and `Spacing`.

* * *

License
-------

MIT License © 2025 [Jorge Perales Diaz](https://shaunebu.com/)