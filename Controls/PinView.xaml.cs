using Shaunebu.Controls.Enums;
using Shaunebu.Controls.Events;
using System.Diagnostics;
using System.Windows.Input;

namespace Shaunebu.Controls.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PinView : ContentView
{
    #region Bindable Properties
    /// <summary>
    /// Set true if you want to dismiss the soft keyboard, when PIN entry is completed
    /// </summary>
    public bool AutoDismissKeyboard
    {
        get => (bool)GetValue(AutoDismissKeyboardProperty);
        set => SetValue(AutoDismissKeyboardProperty, value);
    }

    public static readonly BindableProperty AutoDismissKeyboardProperty =
      BindableProperty.Create(
          nameof(AutoDismissKeyboard),
          typeof(bool),
          typeof(PinView),
          false,
          defaultBindingMode: BindingMode.OneWay);

    /// <summary>
    /// Gets or Sets the PIN value.
    /// </summary>
    public string PINValue
    {
        get => (string)GetValue(PINValueProperty);
        set => SetValue(PINValueProperty, value);
    }

    public static readonly BindableProperty PINValueProperty =
       BindableProperty.Create(
           nameof(PINValue),
           typeof(string),
           typeof(PinView),
           string.Empty,
           defaultBindingMode: BindingMode.TwoWay,
           propertyChanged: PINValuePropertyChanged);

    private static async void PINValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            var control = (PinView)bindable;

            string newPIN = Convert.ToString(newValue);
            string oldPIN = Convert.ToString(oldValue);

            int newPINLength = newPIN.Length;
            int oldPINLength = oldPIN.Length;

            // If no any chars entered, return from here
            if (newPINLength == 0 && oldPINLength == 0)
            {
                //_ = Task.Run(async () =>
                //{
                //    //await Task.Delay(1000);

                //    MainThread.BeginInvokeOnMainThread(() =>
                //    {
                //        var pinBoxArray1 = control.PINBoxContainer.Children.Select(x => x as BoxTemplate).ToList();
                //        pinBoxArray1.ForEach(x =>
                //        {
                //            x.ShrinkAnimation();
                //        });
                //    });
                //});

                return;
            }

            char[] newPINChars = newPIN.ToCharArray();

            control.hiddenTextEntry.Text = newPIN;
            var pinBoxArray = control.PINBoxContainer.Children.Select(x => x as BoxTemplate).ToArray();

            bool isFullLengthPINGivenProgramatically =
                (oldPINLength == 0 && newPINLength == control.PINLength) ||
                newPINLength == oldPINLength;

            if (isFullLengthPINGivenProgramatically)
            {
                //Clear all Previous Entries, and then enter new one, to show proper Entry sequence animation
                for (int i = 0; i < control.PINLength; i++)
                {
                    pinBoxArray[i].ClearValueWithAnimation();
                }
            }

            for (int i = 0; i < control.PINLength; i++)
            {
                if (i < newPINLength)
                {
                    // If user sets PIN value programatically show a bit of Entry sequence animation
                    if (isFullLengthPINGivenProgramatically)
                    {
                        await Task.Delay(50);
                    }

                    pinBoxArray[i].SetValueWithAnimation(newPINChars[i]);
                }
                else
                {
                    if (pinBoxArray.Length >= control.PINLength)
                    {
                        pinBoxArray[i].ClearValueWithAnimation();
                        pinBoxArray[i].UnFocusAnimation();
                    }
                }
            }

            // Set or move Current Focus
            if (control.hiddenTextEntry.IsFocused)
            {
                if (newPINLength < control.PINLength)
                {
                    pinBoxArray[newPINLength].FocusAnimation();
                }
                // When while typing, if you reach to the last charecter, keep focus there (on last character Box),
                // If PIN entry is focused
                else if (newPINLength == control.PINLength)
                {
                    pinBoxArray[newPINLength - 1].FocusAnimation();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }

    /// <summary>
    /// Gets or Sets the Length of the PIN. The number of PIN boxes will be layed out based on this Property.
    /// </summary>
    public int PINLength
    {
        get => (int)GetValue(PINLengthProperty);
        set => SetValue(PINLengthProperty, value);
    }

    public static readonly BindableProperty PINLengthProperty =
      BindableProperty.Create(
          nameof(PINLength),
          typeof(int),
          typeof(PinView),
          4,
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: PINLengthPropertyChanged);

    private static void PINLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if ((int)newValue <= 0)
        {
            return;
        }

       ((PinView)bindable).CreateControl();

        ((PinView)bindable).OnPropertyChanged(nameof(PINValue));
    }

    /// <summary>
    /// Gets or Sets the Input Type (Keyboard Type) of the PIN Box from InputKeyboardType enum:
    ///
    /// Numeric, AlphaNumeric
    /// </summary>
    public InputKeyboardType PINInputType
    {
        get => (InputKeyboardType)GetValue(PINInputTypeProperty);
        set => SetValue(PINInputTypeProperty, value);
    }

    public static readonly BindableProperty PINInputTypeProperty =
       BindableProperty.Create(
           nameof(PINInputType),
           typeof(InputKeyboardType),
           typeof(PinView),
            InputKeyboardType.Numeric,
           defaultBindingMode: BindingMode.OneWay,
           propertyChanged: PINInputTypePropertyChanged);

    private static void PINInputTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = ((PinView)bindable);
        control.SetInputType((InputKeyboardType)newValue);
    }

    /// <summary>
    /// Sets the Keyboard Type as per the InputKeyboardType Bindable Property
    /// </summary>
    /// <param name="inputKeyboardType"></param>
    public void SetInputType(InputKeyboardType inputKeyboardType)
    {
        if (inputKeyboardType == InputKeyboardType.Numeric)
        {
            hiddenTextEntry.Keyboard = Keyboard.Numeric;
        }
        else if (inputKeyboardType == InputKeyboardType.AlphaNumeric)
        {
            // Keyboard.Create(0); : To remove the Hints on top of Keyboard, while typing
            hiddenTextEntry.Keyboard = Keyboard.Create(0);
        }
    }

    /// <summary>
    /// A Command to Bind and invoked when PIN Entry is completed
    /// </summary>
    public ICommand PINEntryCompletedCommand
    {
        get { return (ICommand)GetValue(PINEntryCompletedCommandProperty); }
        set { SetValue(PINEntryCompletedCommandProperty, value); }
    }

    public static readonly BindableProperty PINEntryCompletedCommandProperty =
       BindableProperty.Create(
          nameof(PINEntryCompletedCommand),
          typeof(ICommand),
          typeof(PinView),
          null);

    /// <summary>
    /// Set true if you dont want to show input value charecters, false otherwise True will show Dots inside box
    /// while typing False will show actual input value
    /// </summary>
    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public static readonly BindableProperty IsPasswordProperty =
      BindableProperty.Create(
          nameof(IsPassword),
          typeof(bool),
          typeof(PinView),
          false,
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: IsPasswordPropertyChanged);

    private static void IsPasswordPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = ((PinView)bindable);

        control.PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;
            boxTemplate.SecureMode((bool)newValue);
        });
    }

    /// <summary>
    /// Gets or Sets the FontSize of each char label.
    /// </summary>
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public static readonly BindableProperty FontSizeProperty =
      BindableProperty.Create(
          nameof(FontSize),
          typeof(double),
          typeof(PinView),
          22.0,
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: FontSizePropertyChanged);

    private static void FontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if ((double)newValue < 0)
        {
            return;
        }

        var control = ((PinView)bindable);

        control.PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;
            boxTemplate.CharLabel.FontSize = (double)newValue;
        });
    }

    /// <summary>
    /// Gets or Sets the Font family of the PIN characters.
    /// Applicable if IsPassword = False
    /// </summary>
    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    public static readonly BindableProperty FontFamilyProperty =
       BindableProperty.Create(
           nameof(FontFamily),
           typeof(string),
           typeof(PinView),
           defaultBindingMode: BindingMode.OneWay,
           propertyChanged: FontFamilyPropertyChanged);

    private static void FontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = ((PinView)bindable);

        control.PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;
            boxTemplate.CharLabel.FontFamily = (string)newValue;
        });
    }

    /// <summary>
    /// Gets or Sets the Font attributes of the PIN characters.
    /// Applicable if IsPassword = False
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    public static readonly BindableProperty FontAttributesProperty =
       BindableProperty.Create(
           nameof(FontAttributes),
           typeof(FontAttributes),
           typeof(PinView),
           FontAttributes.None,
           defaultBindingMode: BindingMode.OneWay,
           propertyChanged: FontAttributesPropertyChanged);

    private static void FontAttributesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = ((PinView)bindable);

        control.PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;
            boxTemplate.CharLabel.FontAttributes = (FontAttributes)newValue;
        });
    }

    /// <summary>
    /// Gets or Sets the Height / Width of Dot in Box. Please try to give Even number size to get the proper UI.
    /// </summary>
    public double DotSize
    {
        get => (double)GetValue(DotSizeProperty);
        set => SetValue(DotSizeProperty, value);
    }

    public static readonly BindableProperty DotSizeProperty =
      BindableProperty.Create(
          nameof(DotSize),
          typeof(double),
          typeof(PinView),
          20.0,
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: DotSizePropertyChanged);

    private static void DotSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if ((double)newValue < 0)
        {
            return;
        }

        var control = ((PinView)bindable);

        control.PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;

            boxTemplate.Dot.HeightRequest = (double)newValue;
            boxTemplate.Dot.WidthRequest = (double)newValue;
        });
    }

    /// <summary>
    /// Gets or Sets the Color of the PIN Boxes. Generally the Color of the Border and Dot inside Box
    /// </summary>
    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly BindableProperty ColorProperty =
      BindableProperty.Create(
          nameof(Color),
          typeof(Color),
          typeof(PinView),
          Colors.Black,
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: ColorPropertyChanged);

    private static void ColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PinView)bindable;

        control.PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;
            boxTemplate.SetColor(color: (Color)newValue, boxBorderColor: control.BoxBorderColor);
        });
    }

    /// <summary>
    /// Gets or Sets the Space among the PIN Boxes
    /// </summary>
    public double BoxSpacing
    {
        get => (double)GetValue(BoxSpacingProperty);
        set => SetValue(BoxSpacingProperty, value);
    }

    public static readonly BindableProperty BoxSpacingProperty =
      BindableProperty.Create(
          nameof(BoxSpacing),
          typeof(double),
          typeof(PinView),
          5.0,
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: BoxSpacingPropertyChanged);

    private static void BoxSpacingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if ((double)newValue < 0)
        {
            return;
        }

        var control = ((PinView)bindable);

        control.PINBoxContainer.Spacing = (double)newValue;
    }

    /// <summary>
    /// Gets or Sets the Height / Width of each PIN Box. Please try to give Even number size to get the proper UI.
    /// Please, try to give such size that all PIN boxes fit properly in your device's screen Providing larger size,
    /// can shrink the Boxes if there is no more room to fit them on screen
    /// </summary>
    public double BoxSize
    {
        get => (double)GetValue(BoxSizeProperty);
        set => SetValue(BoxSizeProperty, value);
    }

    public static readonly BindableProperty BoxSizeProperty =
      BindableProperty.Create(
          nameof(BoxSize),
          typeof(double),
          typeof(PinView),
          50.0,
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: BoxSizePropertyChanged);

    private static void BoxSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if ((double)newValue < 0)
        {
            return;
        }

        var control = ((PinView)bindable);

        control.PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;

            boxTemplate.HeightRequest = (double)newValue;
            boxTemplate.WidthRequest = (double)newValue;

            boxTemplate.BoxBorder.HeightRequest = (double)newValue;
            boxTemplate.BoxBorder.WidthRequest = (double)newValue;

            boxTemplate.CharLabel.FontSize = ((double)newValue / 2);
            boxTemplate.SetRadius(control.BoxShape);
        });
    }

    /// <summary>
    /// Gets or Sets the Shape of the PIN Box from BoxShapeType enum:
    ///
    /// Circle, RoundCorner Squere
    /// </summary>
    public BoxShapeType BoxShape
    {
        get => (BoxShapeType)GetValue(BoxShapeProperty);
        set => SetValue(BoxShapeProperty, value);
    }

    public static readonly BindableProperty BoxShapeProperty =
       BindableProperty.Create(
           nameof(BoxShape),
           typeof(BoxShapeType),
           typeof(PinView),
            BoxShapeType.Circle,
           defaultBindingMode: BindingMode.OneWay,
           propertyChanged: BoxShapePropertyChanged);

    private static void BoxShapePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = ((PinView)bindable);

        control.PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;
            boxTemplate.SetRadius((BoxShapeType)newValue);
        });
    }

    /// <summary>
    /// Gets or Sets the FocusIndicatorColor of the PIN Boxes.
    /// </summary>
    public Color BoxFocusColor
    {
        get => (Color)GetValue(BoxFocusColorProperty);
        set => SetValue(BoxFocusColorProperty, value);
    }

    public static readonly BindableProperty BoxFocusColorProperty =
      BindableProperty.Create(
          nameof(BoxFocusColor),
          typeof(Color),
          typeof(PinView),
          Colors.Black,
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: BoxFocusColorPropertyChanged);

    private static void BoxFocusColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((PinView)bindable).PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;
            boxTemplate.BoxFocusColor = (Color)newValue;
        });
    }

    /// <summary>
    /// Gets or Sets the Focus Animation of the PIN Box from FocusAnimationType enum:
    ///
    /// None (Default), ZoomInOut ScaleUp
    /// </summary>
    public FocusAnimationType BoxFocusAnimation
    {
        get => (FocusAnimationType)GetValue(BoxFocusAnimationProperty);
        set => SetValue(BoxFocusAnimationProperty, value);
    }

    public static readonly BindableProperty BoxFocusAnimationProperty =
       BindableProperty.Create(
           nameof(BoxFocusAnimation),
           typeof(FocusAnimationType),
           typeof(PinView),
            FocusAnimationType.None,
           defaultBindingMode: BindingMode.OneWay,
           propertyChanged: BoxFocusAnimationPropertyChanged);

    private static void BoxFocusAnimationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = ((PinView)bindable);

        control.PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;
            boxTemplate.FocusAnimationType = (FocusAnimationType)newValue;
        });
    }

    /// <summary>
    /// Gets or Sets the Border Thickness of the PIN Box. 
    /// </summary>
    public double BoxStrokeThickness
    {
        get => (double)GetValue(BoxStrokeThicknessProperty);
        set => SetValue(BoxStrokeThicknessProperty, value);
    }

    public static readonly BindableProperty BoxStrokeThicknessProperty =
      BindableProperty.Create(
          nameof(BoxStrokeThickness),
          typeof(double),
          typeof(PinView),
          defaultValue: 1.0,
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: BoxStrokeThicknessPropertyChanged);

    private static void BoxStrokeThicknessPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PinView)bindable;

        // Apply the BoxStrokeThickness only if it is different then the value in "Color" Property
        control.PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;
            boxTemplate.BoxBorder.StrokeThickness = (double)newValue;
        });
    }

    /// <summary>
    /// Gets or Sets the Border color of the PIN Box. If you do not set this Property, By default it will use the
    /// "Color" property for BoxBorderColor
    /// </summary>
    public Color BoxBorderColor
    {
        get => (Color)GetValue(BoxBorderColorProperty);
        set => SetValue(BoxBorderColorProperty, value);
    }

    public static readonly BindableProperty BoxBorderColorProperty =
      BindableProperty.Create(
          nameof(BoxBorderColor),
          typeof(Color),
          typeof(PinView),
          Colors.Black,
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: BoxBorderColorPropertyChanged);

    private static void BoxBorderColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PinView)bindable;

        // Apply the BoxBorderColor only if it is different then the value in "Color" Property
        if (control.Color != (Color)newValue)
        {
            control.PINBoxContainer.Children.ToList().ForEach(x =>
            {
                var boxTemplate = (BoxTemplate)x;
                boxTemplate.SetColor(color: control.Color, boxBorderColor: (Color)newValue);
            });
        }
    }

    /// <summary>
    /// Gets or Sets the Background color of the PIN Box.
    /// </summary>
    public Color BoxBackgroundColor
    {
        get => (Color)GetValue(BoxBackgroundColorProperty);
        set => SetValue(BoxBackgroundColorProperty, value);
    }

    public static readonly BindableProperty BoxBackgroundColorProperty =
      BindableProperty.Create(
          nameof(BoxBackgroundColor),
          typeof(Color),
          typeof(PinView),
          defaultValue: Colors.Transparent,
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: BoxBackgroundColorPropertyChanged);

    private static void BoxBackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((PinView)bindable).PINBoxContainer.Children.ToList().ForEach(x =>
        {
            var boxTemplate = (BoxTemplate)x;
            boxTemplate.BoxBorder.BackgroundColor = (Color)newValue;
        });
    }


    #endregion

    #region Fields

    /// <summary>
    /// A TapGesture Recognizer to invoke when user tap on any PIN box. This will help bring up the soft keyboard
    /// </summary>
    private readonly TapGestureRecognizer boxTapGestureRecognizer;

    /// <summary>
    /// An event which is raised/invoked when PIN entry is completed This will help user to execute any code when
    /// entry completed
    /// </summary>
    public event EventHandler<PINCompletedEventArgs> PINEntryCompleted;

    #endregion Fields

    #region Constructor        

    /// <summary>
    /// Initializes a new instance of the <see cref="PinView"/> class.
    /// </summary>
    public PinView()
    {
        InitializeComponent();

        hiddenTextEntry.TextChanged += PINView_TextChanged;
        hiddenTextEntry.Focused += HiddenTextEntry_Focused;
        hiddenTextEntry.Unfocused += HiddenTextEntry_Unfocused;

        boxTapGestureRecognizer = new TapGestureRecognizer() { NumberOfTapsRequired = 1, Command = new Command(BoxTapCommandExecute) };

        CreateControl();
    }
    #endregion Constructor

    #region Methods
    /// <summary>
    /// Handles the Unfocused event of the HiddenTextEntry control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="FocusEventArgs"/> instance containing the event data.</param>
    private void HiddenTextEntry_Unfocused(object sender, FocusEventArgs e)
    {
        var pinBoxArray = PINBoxContainer.Children.Select(x => x as BoxTemplate).ToArray();

        for (int i = 0; i < PINLength; i++)
        {
            pinBoxArray[i].UnFocusAnimation();
        }

        Debug.WriteLine($"{nameof(PinView)}: PIN Entry UnFocused");
    }

    /// <summary>
    /// Handles the Focused event of the HiddenTextEntry control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="FocusEventArgs"/> instance containing the event data.</param>
    private void HiddenTextEntry_Focused(object sender, FocusEventArgs e)
    {
        var length = PINValue == null ? 0 : PINValue.Length;

        // When textbox is focused, Android brings cursor to the start of value, instead of end To fix this issue,
        // added this programmatic cursor movement to the last when focused
        hiddenTextEntry.CursorPosition = length;

        var pinBoxArray = PINBoxContainer.Children.Select(x => x as BoxTemplate).ToArray();

        if (length == PINLength)
        {
            pinBoxArray[length - 1].FocusAnimation();
        }
        else
        {
            for (int i = 0; i < PINLength; i++)
            {
                if (i == length)
                {
                    pinBoxArray[i].FocusAnimation();
                }
                else
                {
                    pinBoxArray[i].UnFocusAnimation();
                }
            }
        }

        Debug.WriteLine($"{nameof(PinView)}: PIN Entry Focused");
    }

    /// <summary>
    /// Calling this, will bring up the soft keyboard, or will help focus the control
    /// </summary>
    public void FocusBox()
    {
        boxTapGestureRecognizer?.Command?.Execute(null);
    }

    /// <summary>
    /// Initializes the UI for the PinView
    /// </summary>
    public void CreateControl()
    {
        hiddenTextEntry.MaxLength = PINLength;
        SetInputType(PINInputType);

        var count = PINBoxContainer.Children.Count;

        if (count < PINLength)
        {
            int newBoxesToAdd = PINLength - count;
            char[] pinCharsArray = PINValue.ToCharArray();

            for (int i = 1; i <= newBoxesToAdd; i++)
            {
                BoxTemplate boxTemplate = CreateBox();
                PINBoxContainer.Children.Add(boxTemplate);

                // When we assign PINValue in XAML directly, the Boxes outside the default length (4), will not get
                // any value in it, overthought we have assigned it in XAML To correct this behavior, we have
                // programmatically assigned value to those Boxes which are added after the Default Length
                if (PINValue.Length >= PINLength)
                {
                    boxTemplate.SetValueWithAnimation(pinCharsArray[4 + i - 1]);
                }
            }
        }
        else if (count > PINLength)
        {
            int boxesToRemove = count - PINLength;
            for (int i = 1; i <= boxesToRemove; i++)
            {
                PINBoxContainer.Children.RemoveAt(PINBoxContainer.Children.Count - 1);
            }
        }
    }

    /// <summary>
    /// Creates the instance of one single PIN box UI
    /// </summary>
    /// <returns></returns>
    private BoxTemplate CreateBox(char? charValue = null)
    {
        BoxTemplate boxTemplate = new()
        {
            HeightRequest = BoxSize,
            WidthRequest = BoxSize
        };
        boxTemplate.BoxBorder.BackgroundColor = BoxBackgroundColor;
        boxTemplate.CharLabel.FontSize = BoxSize / 2;
        boxTemplate.CharLabel.FontFamily = FontFamily;
        boxTemplate.CharLabel.FontAttributes = FontAttributes;
        boxTemplate.CharLabel.FontSize = FontSize;

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            // Added TapGesture to all components of the Box so that if we tap anywhere
            // It still gets the focus. In Android things are not working as expected otherwise.

            boxTemplate.BoxBorder.GestureRecognizers.Add(boxTapGestureRecognizer);
            boxTemplate.ValueContainer.GestureRecognizers.Add(boxTapGestureRecognizer);
            boxTemplate.Dot.GestureRecognizers.Add(boxTapGestureRecognizer);
            boxTemplate.CharLabel.GestureRecognizers.Add(boxTapGestureRecognizer);
        }
        else
        {
            boxTemplate.GestureRecognizers.Add(boxTapGestureRecognizer);
        }

        boxTemplate.BoxFocusColor = BoxFocusColor;
        boxTemplate.FocusAnimationType = BoxFocusAnimation;
        boxTemplate.SecureMode(IsPassword);
        boxTemplate.SetColor(Color, BoxBorderColor);
        boxTemplate.SetRadius(BoxShape);
        boxTemplate.ShrinkAnimation();

        if (charValue.HasValue)
        {
            boxTemplate.SetValueWithAnimation(charValue.Value);
        }

        return boxTemplate;
    }

    #endregion Methods

    #region Events

    /// <summary>
    /// Invokes when user type the PIN, Assignees value to PINValue property or Text changes in the hidden textbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void PINView_TextChanged(object sender, TextChangedEventArgs e)
    {
        PINValue = e.NewTextValue;

        // To have some delay so that till the next execution all assigned values to the properties in XAML gets
        // sets and we get the right value at the time after this delay Otherwise due to sequence of calls, some
        // properties gets their actual assigned value after the completion of this event Also To have some delay,
        // before invoking any Action, otherwise, (if) while navigation, it will be quick and you won't see your
        // last entry / or animation.
        await Task.Delay(200);

        if (e.NewTextValue.Length >= PINLength)
        {
            // Dismiss the keyboard, once entry is completed up to the defined length and if AutoDismissKeyboard
            // property is true
            if (AutoDismissKeyboard)
            {
                (sender as Entry).Unfocus();
                Debug.WriteLine($"{nameof(PinView)}: PIN Entry UnFocused");
            }

            Debug.WriteLine($"{nameof(PinView)}: PIN Entry Completed");

            PINEntryCompleted?.Invoke(this, new PINCompletedEventArgs(PINValue));
            PINEntryCompletedCommand?.Execute(PINValue);
        }
    }

    #endregion Events

    #region Commands

    /// <summary>
    /// Invokes when user tap on the PinView, this will bring up the soft keyboard
    /// </summary>
    private async void BoxTapCommandExecute()
    {
        Debug.WriteLine($"{nameof(PinView)}: Box Tapped");

        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            await Task.Delay(100);
        }
        hiddenTextEntry.Focus();
    }
    #endregion Commands
}