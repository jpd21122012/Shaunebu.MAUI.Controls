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
| [**Shimmer**](#shimmer) | A lightweight shimmer/skeleton loader control for .NET MAUI | [Details](Docs/Shimmer.md) |
| [**MaterialTextField**](#materialtextfield) |A fully customizable Material Design-inspired text field for .NET MAUI with validation, floating labels, password visibility toggle, and trailing content support. | [Details](Docs/MaterialTextField.md) |
| [**PinView**](#pinview) |A fully customizable PIN entry control for .NET MAUI with secure input, animated PIN boxes, focus animations, and commands/events for PIN completion. | [Details](Docs/PinView.md) |
| [**RatingView**](#ratingview) |A fully customizable, graphics-based rating control for .NET MAUI. Supports fractional ratings, custom shapes, colors, spacing, and stroke styles. Ideal for star ratings, hearts, or any custom SVG path shape. | [Details](Docs/RatingView.md) |


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


## ✨ Shimmer
`Shaunebu.Controls.ShimmerWrapper`

### Features
*   **Any View Support** – Wrap `Image`, `Label`, `Layouts`, or custom content
*   **Shapes** – Built-in `Rectangle`, `RoundedRectangle`, `Circle` placeholders
*   **CornerRadius** – Fully adjustable corner radius
*   **Gradient Animation** – Smooth shimmer animation over content
*   **Animation Direction** – LeftToRight, RightToLeft, TopToBottom, BottomToTop
*   **Shimmer Customization** – Control `ShimmerSpeed`, `ShimmerWidth`, `ShimmerColor`, `BaseColor`
*   **Placeholder Template** – Provide custom skeleton layout
*   **ShimmerOverlay** – Optional overlay for custom shimmer visuals

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

```xml
<controls:ShimmerWrapper IsLoading="True">
    <Image
        Source="dotnet_bot.png"
        Aspect="AspectFill"
        HeightRequest="100"
        WidthRequest="100"/>
</controls:ShimmerWrapper>
```

```xml
<controls:ShimmerWrapper IsLoading="True">
            <controls:ShimmerWrapper.PlaceholderTemplate>
                <DataTemplate>
                    <HorizontalStackLayout Spacing="15">

                        <BoxView
                            CornerRadius="25"
                            HeightRequest="50"
                            WidthRequest="50" />


                        <VerticalStackLayout Spacing="8">
                            <BoxView
                                CornerRadius="8"
                                HeightRequest="15"
                                WidthRequest="120" />
                            <BoxView
                                CornerRadius="8"
                                HeightRequest="15"
                                WidthRequest="180" />
                        </VerticalStackLayout>
                    </HorizontalStackLayout>
                </DataTemplate>
            </controls:ShimmerWrapper.PlaceholderTemplate>
        </controls:ShimmerWrapper>
```

```xml
<VerticalStackLayout Padding="20" Spacing="15">
    <controls:ShimmerWrapper IsLoading="True" Shape="Circle" />
    <controls:ShimmerWrapper IsLoading="True" Shape="RoundedRectangle" />
    <controls:ShimmerWrapper IsLoading="True" Shape="Rectangle" />
</VerticalStackLayout>
```


## ✨ MaterialTextField
`Shaunebu.Controls.MaterialTextField`

### Features
*   **Floating Label** – Animates above when entry is focused or has text
*   **Validation** – Synchronous and asynchronous validation support
*   **Password Support** – Toggle visibility with image or text
*   **Trailing Content** – Custom trailing view (icon, button, etc.)
*   **Helper / Error Text** – Displays validation messages or helper text
*   **Visual States** – Customizable border, filled or outlined variants
*   **Password Strength Bar** – Optional visual strength indicator
*   **Platform-specific UI tweaks** – Bottom line removed on Android/iOS

```xml
<controls:MaterialTextField
    Title="Username"
    AccentColor="DeepSkyBlue"
    AllowClear="True"
    BorderThickness="1.5"
    CornerRadius="10"
    HelperText="Your unique username"
    Icon="icon_chip_close.png"
    KeyboardType="Text"
    Placeholder="Enter your username"
    ValidationErrorMessage="Username must be 4-12 alphanumeric characters"
    ValidationPattern="^[a-zA-Z0-9]{4,12}$"
    Variant="Outlined" />

<!--  Password Field with toggle images + strength  -->
<controls:MaterialTextField
    Title="Password"
    AccentColor="DarkViolet"
    AllowClear="False"
    CornerRadius="12"
    Icon="dotnet_bot.png"
    IsPassword="True"
    PasswordHiddenImage="icon_chip_close.png"
    PasswordStrengthEnabled="True"
    PasswordVisibleImage="dotnet_bot.png"
    Placeholder="Enter your password"
    Variant="Filled" />

<!--  Field with async validation  -->
<controls:MaterialTextField
    Title="Email"
    AccentColor="Teal"
    BorderThickness="2"
    CornerRadius="8"
    HelperText="We'll send you a verification email"
    Icon="dotnet_bot.png"
    Placeholder="Enter your email"
    ValidationErrorMessage="Please enter a valid email"
    ValidationPattern="^[^@\s]+@[^@\s]+\.[^@\s]+$"
    Variant="Outlined" />

<!--  Field with leading content (prefix)  -->
<controls:MaterialTextField
    Title="Phone"
    AccentColor="DarkBlue"
    AllowClear="True"
    KeyboardType=""
    Placeholder="Enter your phone number"
    Variant="Outlined">
    <controls:MaterialTextField.Validations>
        <validations:RegexValidation Pattern="^\d{10}$" Message="Must be 10 digits"/>
    </controls:MaterialTextField.Validations>
</controls:MaterialTextField>


<controls:MaterialTextField
    Title="Username"
    AccentColor="DeepSkyBlue"
    AllowClear="True"
    CornerRadius="8"
    FilledColor="#FFF0F0F0"
    HelperText="Your unique username"
    Icon="icon_chip_close.png"
    KeyboardType="Text"
    Placeholder="Enter your username"
    PlaceholderColor="LightGray"
    TitleColor="Gray"
    ValidationErrorMessage="Username must be 4-12 alphanumeric characters"
    ValidationPattern="^[a-zA-Z0-9]{4,12}$"
    Variant="Outlined" />
```


## ✨ PinView
`Shaunebu.Controls.PinView`

### Features
*   **PIN Entry** – Secure numeric or alphanumeric PIN input
*   **Custom PIN Length** – Supports any number of boxes
*   **Secure Mode** – Show dots instead of actual characters
*   **Animated Boxes** – Focus and value animations
*   **Auto-dismiss Keyboard** – Optionally hides keyboard on completion
*   **Customizable Box UI** – Size, spacing, shape, color, and font
*   **Tap to Focus** – Tap anywhere on the box to focus the input
*   **Command & Event** – Execute code when PIN entry is complete

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


## ✨ RatingView
`Shaunebu.Controls.RatingView`

### Features
*   **Fractional Ratings** – Supports partial fills for half or decimal ratings.
    
*   **Custom Shapes** – Render any SVG path as rating items.
    
*   **Adjustable Item Count** – Default 5 items, can be increased or decreased.
    
*   **Customizable Colors** – Separate fill colors for rated and unrated items.
    
*   **Stroke Options** – Set stroke color and width for outline effect.
    
*   **Spacing & Size** – Control individual item size and spacing.
    
*   **Read-only Mode** – Disable user interaction for display-only ratings.
    
*   **Custom Templates** – Provide any UI per rating item via `ItemTemplate`.
    
*   **Graphics-based Rendering** – Smooth vector rendering on all platforms.

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










## 🚀 Getting Started
### Installation
```
dotnet add package Shaunebu.MAUI.Controls
```

Basic Usage
Add the namespace:

```xml
xmlns:controls="clr-namespace:Shaunebu.MAUI.Controls;assembly=Shaunebu.MAUI.Controls"
```
Use any control:


```xml
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

📧 [jorge.p@shaunebu.com](https://mailto:support@shaunebu.com)  
🐛 [GitHub Issues](https://github.com/jpd21122012/Shaunebu.MAUI.Controls/issues)

----------
### 📄 License


MIT License © 2025 [Jorge Perales Diaz](https://jpdblog.com/)