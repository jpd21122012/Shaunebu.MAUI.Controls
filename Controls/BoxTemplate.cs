using Microsoft.Maui.Controls.Shapes;
using Shaunebu.Controls.Enums;

namespace Shaunebu.Controls.Controls;

public class BoxTemplate : ContentView
{
    #region Fields        
    /// <summary>
    /// The input character
    /// </summary>
    private string _inputChar;

    /// <summary>
    /// The color
    /// </summary>
    private Color _color;

    /// <summary>
    /// The box border color
    /// </summary>
    private Color _boxBorderColor;

    /// <summary>
    /// The box border
    /// </summary>
    private Border boxBorder;

    /// <summary>
    /// The value container
    /// </summary>
    private Grid valueContainer;

    /// <summary>
    /// The dot
    /// </summary>
    private Ellipse dot;

    /// <summary>
    /// The character label
    /// </summary>
    private Label charLabel;
    #endregion

    #region Services
    #endregion

    #region Validators
    #endregion

    #region Properties        
    /// <summary>
    /// Gets or sets the type of the focus animation.
    /// </summary>
    /// <value>
    /// The type of the focus animation.
    /// </value>
    public FocusAnimationType FocusAnimationType { get; set; }

    /// <summary>
    /// Gets or sets the color of the box focus.
    /// </summary>
    /// <value>
    /// The color of the box focus.
    /// </value>
    public Color BoxFocusColor { get; set; }

    /// <summary>
    /// Gets the box border.
    /// </summary>
    /// <value>
    /// The box border.
    /// </value>
    public Border BoxBorder => this.boxBorder;

    /// <summary>
    /// Gets the value container.
    /// </summary>
    /// <value>
    /// The value container.
    /// </value>
    public Grid ValueContainer => this.valueContainer;

    /// <summary>
    /// Gets the dot.
    /// </summary>
    /// <value>
    /// The dot.
    /// </value>
    public Ellipse Dot => this.dot;

    /// <summary>
    /// Gets the character label.
    /// </summary>
    /// <value>
    /// The character label.
    /// </value>
    public Label CharLabel => this.charLabel;

    #endregion

    #region Commands
    #endregion

    #region Constructor        
    /// <summary>
    /// Initializes a new instance of the <see cref="BoxTemplate"/> class.
    /// </summary>
    public BoxTemplate()
    {
        boxBorder = new Border()
        {
            Padding = new Thickness(0),
            BackgroundColor = Colors.Transparent,
            Stroke = Colors.Black,
            StrokeThickness = 1,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(50 / 2),
            },
            HeightRequest = 50,
            WidthRequest = 50,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };

        valueContainer = new Grid()
        {
            // In windows it looks little off from left and top so corrected by this
            Margin = DeviceInfo.Platform == DevicePlatform.WinUI ? new Thickness(0, 0, 1, 1) : new Thickness(0),
        };

        dot = new Ellipse()
        {
            HeightRequest = 20,
            WidthRequest = 20,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Fill = Colors.Black,
        };

        charLabel = new Label()
        {
            Margin = DeviceInfo.Platform == DevicePlatform.WinUI ? new Thickness(0, 0, 0, 2) : new Thickness(0),
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            //HorizontalOptions = LayoutOptions.Center,
            //VerticalOptions = LayoutOptions.Center,
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center,
        };

        valueContainer.Children.Add(dot);
        valueContainer.Children.Add(charLabel);

        boxBorder.Content = valueContainer;

        Content = boxBorder;

        // By default Shrink the Dot or Text label to get it hidden
        ShrinkAnimation();
    }

    #endregion

    #region Methods        
    /// <summary>
    /// Grows the animation.
    /// </summary>
    private void GrowAnimation()
    {
        valueContainer.ScaleTo(1.0, 50);
    }

    /// <summary>
    /// Shrinks the animation.
    /// </summary>
    public void ShrinkAnimation()
    {
        try
        {
            // TODO: This does not work
            //valueContainer.ScaleTo(0, 100);
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Task.Delay(500);
                // This works if we wrap shrink animation in Task and then run on ui thread
                Task.Run(() =>
                {

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        try
                        {
                            valueContainer.ScaleTo(0, 50);
                        }
                        catch (Exception ex)
                        {
                            //Ignore this
                        }
                    });
                });
            }
            else
            {
                valueContainer.ScaleTo(0, 50);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Sets the Color of Border, Dot, Input CharLabel
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color, Color boxBorderColor)
    {
        _color = color;
        _boxBorderColor = boxBorderColor;

        SetBorderColor();

        Dot.Fill = color;
        CharLabel.TextColor = color;
    }

    /// <summary>
    /// Applies the Corner Radius to the PIN Box based on the ShapeType
    /// </summary>
    /// <param name="shapeType"></param>
    public void SetRadius(BoxShapeType shapeType)
    {
        if (shapeType == BoxShapeType.Circle)
        {
            BoxBorder.StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(BoxBorder.HeightRequest / 2),
            };
        }
        else if (shapeType == BoxShapeType.Squere)
        {
            BoxBorder.StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(0),
            };
        }
        else if (shapeType == BoxShapeType.RoundCorner)
        {
            BoxBorder.StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(10),
            };
        }
    }

    /// <summary>
    /// Method sets the visibility of Input Characters or Dots. IsPassword =
    /// True : Displays Dots IsPassword = False : Displays Chars
    /// </summary>
    /// <param name="isPassword"></param>
    public void SecureMode(bool isPassword)
    {
        valueContainer.Children.Clear();

        if (isPassword)
        {
            valueContainer.Children.Add(Dot);
        }
        else
        {
            valueContainer.Children.Add(CharLabel);
        }

        if (!string.IsNullOrEmpty(_inputChar))
        {
            GrowAnimation();
        }
        else
        {
            ShrinkAnimation();
        }
    }

    /// <summary>
    /// Clears the input value along with showing the Clear value Animation
    /// </summary>
    /// <returns></returns>
    public void ClearValueWithAnimation()
    {
        _inputChar = null;
        ShrinkAnimation();
    }

    /// <summary>
    /// Sets the input value along with showing the Set value animation
    /// </summary>
    /// <param name="inputChar"></param>
    /// <returns></returns>
    public void SetValueWithAnimation(char inputChar)
    {
        UnFocusAnimation();

        CharLabel.Text = inputChar.ToString();
        _inputChar = inputChar.ToString();
        GrowAnimation();
    }

    /// <summary>
    /// Focuses the animation.
    /// </summary>
    public async void FocusAnimation()
    {
        //Box.BorderColor = BoxFocusColor;
        BoxBorder.Stroke = BoxFocusColor;

        if (FocusAnimationType == FocusAnimationType.ZoomInOut)
        {
            await this.ScaleTo(1.2, 100);
            await this.ScaleTo(1, 100);
        }
        else if (FocusAnimationType == FocusAnimationType.ScaleUp)
        {
            await this.ScaleTo(1.2, 100);
        }
    }

    /// <summary>
    /// Uns the focus animation.
    /// </summary>
    public void UnFocusAnimation()
    {
        SetBorderColor();
        this.ScaleTo(1, 100);
    }

    /// <summary>
    /// Sets the color of the border.
    /// </summary>
    private void SetBorderColor()
    {
        BoxBorder.Stroke = _boxBorderColor == Colors.Black ? _color : _boxBorderColor;
    }
    #endregion
}
