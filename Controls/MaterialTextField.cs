using Microsoft.Maui.Controls.Shapes;
using Shaunebu.Controls.Abstractions;
using Shaunebu.Controls.Enums;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Shaunebu.Controls.Controls
{
    [ContentProperty(nameof(TrailingContent))]
    public class MaterialTextField : ContentView
    {
        #region Bindable Properties        
        /// <summary>
        /// The is valid property
        /// </summary>
        public static readonly BindableProperty IsValidProperty =
          BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(MaterialTextField), defaultValue: true, defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// The leading content property
        /// </summary>
        public static readonly BindableProperty LeadingContentProperty =
              BindableProperty.Create(nameof(LeadingContent), typeof(View), typeof(MaterialTextField), default(View), propertyChanged: OnVisualPropertyChanged);

        /// <summary>
        /// The password strength enabled property
        /// </summary>
        public static readonly BindableProperty PasswordStrengthEnabledProperty =
            BindableProperty.Create(nameof(PasswordStrengthEnabled), typeof(bool), typeof(MaterialTextField), false, propertyChanged: OnVisualPropertyChanged);

        /// <summary>
        /// The asynchronous validator property
        /// </summary>
        public static readonly BindableProperty AsyncValidatorProperty =
            BindableProperty.Create(nameof(AsyncValidator), typeof(Func<string, Task<(bool, string)>>), typeof(MaterialTextField), null);

        /// <summary>
        /// The error icon property
        /// </summary>
        public static readonly BindableProperty ErrorIconProperty =
            BindableProperty.Create(nameof(ErrorIcon), typeof(ImageSource), typeof(MaterialTextField), default(ImageSource));

        /// <summary>
        /// The title color property
        /// </summary>
        public static readonly BindableProperty TitleColorProperty =
            BindableProperty.Create(nameof(TitleColor), typeof(Color), typeof(MaterialTextField), Colors.Gray, propertyChanged: OnVisualPropertyChanged);

        /// <summary>
        /// The placeholder color property
        /// </summary>
        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialTextField), Colors.Gray.WithAlpha(0.5f), propertyChanged: OnVisualPropertyChanged);

        /// <summary>
        /// The filled color property
        /// </summary>
        public static readonly BindableProperty FilledColorProperty =
            BindableProperty.Create(nameof(FilledColor), typeof(Color), typeof(MaterialTextField), Colors.Black.WithAlpha(0.04f), propertyChanged: OnVisualPropertyChanged);

        /// <summary>
        /// The is borderless property
        /// </summary>
        public static readonly BindableProperty IsBorderlessProperty =
            BindableProperty.Create(nameof(IsBorderless), typeof(bool), typeof(MaterialTextField), false, propertyChanged: OnVisualPropertyChanged);

        /// <summary>
        /// The keyboard type property
        /// </summary>
        public static readonly BindableProperty KeyboardTypeProperty =
            BindableProperty.Create(nameof(KeyboardType), typeof(Keyboard), typeof(MaterialTextField), Keyboard.Default);

        /// <summary>
        /// The title property
        /// </summary>
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(MaterialTextField), string.Empty, propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// The text property
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialTextField), string.Empty, BindingMode.TwoWay, propertyChanged: OnTextPropertyChanged);

        /// <summary>
        /// The placeholder property
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialTextField), string.Empty, propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// The icon property
        /// </summary>
        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(MaterialTextField), default(ImageSource), propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// The trailing content property
        /// </summary>
        public static readonly BindableProperty TrailingContentProperty =
            BindableProperty.Create(nameof(TrailingContent), typeof(View), typeof(MaterialTextField), default(View), propertyChanged: OnTrailingChanged);

        /// <summary>
        /// The allow clear property
        /// </summary>
        public static readonly BindableProperty AllowClearProperty =
            BindableProperty.Create(nameof(AllowClear), typeof(bool), typeof(MaterialTextField), true, propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// The is password property
        /// </summary>
        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialTextField), false, propertyChanged: OnIsPasswordChanged);

        /// <summary>
        /// The is password visible property
        /// </summary>
        public static readonly BindableProperty IsPasswordVisibleProperty =
            BindableProperty.Create(nameof(IsPasswordVisible), typeof(bool), typeof(MaterialTextField), false, BindingMode.TwoWay, propertyChanged: OnPasswordVisibilityChanged);

        /// <summary>
        /// The password visible image property
        /// </summary>
        public static readonly BindableProperty PasswordVisibleImageProperty =
            BindableProperty.Create(nameof(PasswordVisibleImage), typeof(ImageSource), typeof(MaterialTextField), default(ImageSource), propertyChanged: OnPasswordImageChanged);

        /// <summary>
        /// The password hidden image property
        /// </summary>
        public static readonly BindableProperty PasswordHiddenImageProperty =
            BindableProperty.Create(nameof(PasswordHiddenImage), typeof(ImageSource), typeof(MaterialTextField), default(ImageSource), propertyChanged: OnPasswordImageChanged);

        /// <summary>
        /// The accent color property
        /// </summary>
        public static readonly BindableProperty AccentColorProperty =
            BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialTextField), Colors.DeepSkyBlue, propertyChanged: OnVisualPropertyChanged);

        /// <summary>
        /// The variant property
        /// </summary>
        public static readonly BindableProperty VariantProperty =
            BindableProperty.Create(nameof(Variant), typeof(MaterialTextFieldVariant), typeof(MaterialTextField), MaterialTextFieldVariant.Outlined, propertyChanged: OnVisualPropertyChanged);

        /// <summary>
        /// The corner radius property
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(MaterialTextField), 12f, propertyChanged: OnVisualPropertyChanged);

        /// <summary>
        /// The border thickness property
        /// </summary>
        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(double), typeof(MaterialTextField), 1.5, propertyChanged: OnVisualPropertyChanged);

        /// <summary>
        /// The helper text property
        /// </summary>
        public static readonly BindableProperty HelperTextProperty =
            BindableProperty.Create(nameof(HelperText), typeof(string), typeof(MaterialTextField), string.Empty, propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// The error text property
        /// </summary>
        public static readonly BindableProperty ErrorTextProperty =
            BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(MaterialTextField), string.Empty, propertyChanged: OnErrorTextChanged);

        /// <summary>
        /// The is error property
        /// </summary>
        public static readonly BindableProperty IsErrorProperty =
            BindableProperty.Create(nameof(IsError), typeof(bool), typeof(MaterialTextField), false, propertyChanged: OnVisualPropertyChanged);

        /// <summary>
        /// The validation pattern property
        /// </summary>
        public static readonly BindableProperty ValidationPatternProperty =
            BindableProperty.Create(nameof(ValidationPattern), typeof(string), typeof(MaterialTextField), string.Empty);

        /// <summary>
        /// The validation error message property
        /// </summary>
        public static readonly BindableProperty ValidationErrorMessageProperty =
            BindableProperty.Create(nameof(ValidationErrorMessage), typeof(string), typeof(MaterialTextField), "Invalid input");

        #endregion

        #region Internal State        
        /// <summary>
        /// Gets the validations.
        /// </summary>
        /// <value>
        /// The validations.
        /// </value>
        public List<IValidation> Validations { get; } = new List<IValidation>();

        /// <summary>
        /// The last validation state
        /// </summary>
        private bool? _lastValidationState;
        #endregion

        #region Public API        
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get => (bool)GetValue(IsValidProperty); private set => SetValue(IsValidProperty, value); }

        /// <summary>
        /// Gets or sets the content of the leading.
        /// </summary>
        /// <value>
        /// The content of the leading.
        /// </value>
        public View LeadingContent { get => (View)GetValue(LeadingContentProperty); set => SetValue(LeadingContentProperty, value); }

        /// <summary>
        /// Gets or sets a value indicating whether [password strength enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [password strength enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool PasswordStrengthEnabled { get => (bool)GetValue(PasswordStrengthEnabledProperty); set => SetValue(PasswordStrengthEnabledProperty, value); }

        /// <summary>
        /// Gets or sets the asynchronous validator.
        /// </summary>
        /// <value>
        /// The asynchronous validator.
        /// </value>
        public Func<string, Task<(bool, string)>> AsyncValidator { get => (Func<string, Task<(bool, string)>>)GetValue(AsyncValidatorProperty); set => SetValue(AsyncValidatorProperty, value); }

        /// <summary>
        /// Gets or sets the error icon.
        /// </summary>
        /// <value>
        /// The error icon.
        /// </value>
        public ImageSource ErrorIcon { get => (ImageSource)GetValue(ErrorIconProperty); set => SetValue(ErrorIconProperty, value); }

        /// <summary>
        /// Gets or sets the type of the keyboard.
        /// </summary>
        /// <value>
        /// The type of the keyboard.
        /// </value>
        public Keyboard KeyboardType { get => (Keyboard)GetValue(KeyboardTypeProperty); set => SetValue(KeyboardTypeProperty, value); }

        /// <summary>
        /// Gets or sets the color of the title.
        /// </summary>
        /// <value>
        /// The color of the title.
        /// </value>
        public Color TitleColor { get => (Color)GetValue(TitleColorProperty); set => SetValue(TitleColorProperty, value); }

        /// <summary>
        /// Gets or sets the color of the placeholder.
        /// </summary>
        /// <value>
        /// The color of the placeholder.
        /// </value>
        public Color PlaceholderColor { get => (Color)GetValue(PlaceholderColorProperty); set => SetValue(PlaceholderColorProperty, value); }

        /// <summary>
        /// Gets or sets the color of the filled.
        /// </summary>
        /// <value>
        /// The color of the filled.
        /// </value>
        public Color FilledColor { get => (Color)GetValue(FilledColorProperty); set => SetValue(FilledColorProperty, value); }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is borderless.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is borderless; otherwise, <c>false</c>.
        /// </value>
        public bool IsBorderless { get => (bool)GetValue(IsBorderlessProperty); set => SetValue(IsBorderlessProperty, value); }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        /// <value>
        /// The placeholder.
        /// </value>
        public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public ImageSource Icon { get => (ImageSource)GetValue(IconProperty); set => SetValue(IconProperty, value); }

        /// <summary>
        /// Gets or sets the content of the trailing.
        /// </summary>
        /// <value>
        /// The content of the trailing.
        /// </value>
        public View TrailingContent { get => (View)GetValue(TrailingContentProperty); set => SetValue(TrailingContentProperty, value); }

        /// <summary>
        /// Gets or sets a value indicating whether [allow clear].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow clear]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowClear { get => (bool)GetValue(AllowClearProperty); set => SetValue(AllowClearProperty, value); }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is password.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is password; otherwise, <c>false</c>.
        /// </value>
        public bool IsPassword { get => (bool)GetValue(IsPasswordProperty); set => SetValue(IsPasswordProperty, value); }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is password visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is password visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsPasswordVisible { get => (bool)GetValue(IsPasswordVisibleProperty); set => SetValue(IsPasswordVisibleProperty, value); }

        /// <summary>
        /// Gets or sets the password visible image.
        /// </summary>
        /// <value>
        /// The password visible image.
        /// </value>
        public ImageSource PasswordVisibleImage { get => (ImageSource)GetValue(PasswordVisibleImageProperty); set => SetValue(PasswordVisibleImageProperty, value); }

        /// <summary>
        /// Gets or sets the password hidden image.
        /// </summary>
        /// <value>
        /// The password hidden image.
        /// </value>
        public ImageSource PasswordHiddenImage { get => (ImageSource)GetValue(PasswordHiddenImageProperty); set => SetValue(PasswordHiddenImageProperty, value); }

        /// <summary>
        /// Gets or sets the color of the accent.
        /// </summary>
        /// <value>
        /// The color of the accent.
        /// </value>
        public Color AccentColor { get => (Color)GetValue(AccentColorProperty); set => SetValue(AccentColorProperty, value); }

        /// <summary>
        /// Gets or sets the variant.
        /// </summary>
        /// <value>
        /// The variant.
        /// </value>
        public MaterialTextFieldVariant Variant { get => (MaterialTextFieldVariant)GetValue(VariantProperty); set => SetValue(VariantProperty, value); }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>
        /// The corner radius.
        /// </value>
        public float CornerRadius { get => (float)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }

        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        /// <value>
        /// The border thickness.
        /// </value>
        public double BorderThickness { get => (double)GetValue(BorderThicknessProperty); set => SetValue(BorderThicknessProperty, value); }

        /// <summary>
        /// Gets or sets the helper text.
        /// </summary>
        /// <value>
        /// The helper text.
        /// </value>
        public string HelperText { get => (string)GetValue(HelperTextProperty); set => SetValue(HelperTextProperty, value); }

        /// <summary>
        /// Gets or sets the error text.
        /// </summary>
        /// <value>
        /// The error text.
        /// </value>
        public string ErrorText { get => (string)GetValue(ErrorTextProperty); set => SetValue(ErrorTextProperty, value); }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
        public bool IsError { get => (bool)GetValue(IsErrorProperty); set => SetValue(IsErrorProperty, value); }

        /// <summary>
        /// Gets or sets the validation pattern.
        /// </summary>
        /// <value>
        /// The validation pattern.
        /// </value>
        public string ValidationPattern { get => (string)GetValue(ValidationPatternProperty); set => SetValue(ValidationPatternProperty, value); }

        /// <summary>
        /// Gets or sets the validation error message.
        /// </summary>
        /// <value>
        /// The validation error message.
        /// </value>
        public string ValidationErrorMessage { get => (string)GetValue(ValidationErrorMessageProperty); set => SetValue(ValidationErrorMessageProperty, value); }
        #endregion

        #region Visual Tree Elements        
        /// <summary>
        /// The helper or error
        /// </summary>
        private readonly Label _helperOrError;

        /// <summary>
        /// The password strength bar
        /// </summary>
        private readonly BoxView _passwordStrengthBar;

        /// <summary>
        /// The error icon image
        /// </summary>
        private readonly Image _errorIconImage;

        /// <summary>
        /// The root
        /// </summary>
        private readonly Grid _root;

        /// <summary>
        /// The floating label
        /// </summary>
        private readonly Label _floatingLabel;

        /// <summary>
        /// The field border
        /// </summary>
        private readonly Border _fieldBorder;

        /// <summary>
        /// The field grid
        /// </summary>
        private readonly Grid _fieldGrid;

        /// <summary>
        /// The leading icon
        /// </summary>
        private readonly Image _leadingIcon;

        /// <summary>
        /// The entry
        /// </summary>
        private readonly Entry _entry;

        /// <summary>
        /// The trailing host
        /// </summary>
        private readonly Grid _trailingHost;

        /// <summary>
        /// The clear button
        /// </summary>
        private readonly Button _clearButton;

        /// <summary>
        /// The toggle image button
        /// </summary>
        private readonly ImageButton _toggleImageButton;

        /// <summary>
        /// The toggle text button
        /// </summary>
        private readonly Button _toggleTextButton;

        /// <summary>
        /// The is animating
        /// </summary>
        private bool _isAnimating;
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialTextField"/> class.
        /// </summary>
        public MaterialTextField()
        {
            _root = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto)
                }
            };

            // Floating label
            _floatingLabel = new Label
            {
                FontSize = 14,
                TextColor = Colors.Gray,
                TranslationY = 18,
                Opacity = 0.0,
                Margin = new Thickness(12, 0, 12, 0)
            };
            _floatingLabel.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
            _root.Add(_floatingLabel);
            Grid.SetRow(_floatingLabel, 0);

            // Leading icon & LeadingContent
            _leadingIcon = new Image { WidthRequest = 20, HeightRequest = 20, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Opacity = 0.85, Margin = new Thickness(6, 0) };

            _entry = new Entry
            {
                BackgroundColor = Colors.Transparent,
                ClearButtonVisibility = ClearButtonVisibility.Never,
                Margin = new Thickness(0),
                VerticalOptions = LayoutOptions.Center
            };
            _entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), source: this, mode: BindingMode.TwoWay));
            _entry.SetBinding(Entry.PlaceholderProperty, new Binding(nameof(Placeholder), source: this));
            _entry.SetBinding(Entry.IsPasswordProperty, new Binding(nameof(IsPassword), source: this));
            _entry.Keyboard = KeyboardType;

            _entry.Focused += (_, __) => UpdateVisualState(true);
            _entry.Unfocused += async (_, __) =>
            {
                UpdateVisualState(false);
                await RunAsyncValidation();
                RunValidations(_entry.Text);
            };
            _entry.TextChanged += Entry_TextChanged;

            // Clear button
            _clearButton = new Button { Text = "×", TextColor = Colors.Black, FontSize = 25, BackgroundColor = Colors.Transparent, WidthRequest = 36, HeightRequest = 36, Padding = 0, IsVisible = false };
            _clearButton.Clicked += (_, __) => Text = string.Empty;

            // Toggle button (image)
            _toggleImageButton = new ImageButton { BackgroundColor = Colors.Transparent, WidthRequest = 36, HeightRequest = 36, Padding = 0, IsVisible = false };
            _toggleImageButton.Clicked += (_, __) => IsPasswordVisible = !IsPasswordVisible;

            _toggleTextButton = new Button { BackgroundColor = Colors.Transparent, WidthRequest = 36, HeightRequest = 36, Padding = 0, IsVisible = false };
            _toggleTextButton.Clicked += (_, __) => IsPasswordVisible = !IsPasswordVisible;

            _trailingHost = new Grid { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End, Margin = new Thickness(6, 0, 6, 0) };

            // Field grid: LeadingContent / Icon | Entry | Trailing
            _fieldGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(GridLength.Auto),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Auto)
                },
                Padding = new Thickness(12, 10)
            };

            _fieldGrid.Add(_leadingIcon);
            Grid.SetColumn(_leadingIcon, 0);

            _fieldGrid.Add(_entry);
            Grid.SetColumn(_entry, 1);

            _fieldGrid.Add(_trailingHost);
            Grid.SetColumn(_trailingHost, 2);

            // Border wrapper
            _fieldBorder = new Border
            {
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(CornerRadius) },
                StrokeThickness = BorderThickness,
                Padding = 0,
                Content = _fieldGrid
            };
            _root.Add(_fieldBorder);
            Grid.SetRow(_fieldBorder, 1);

            // Helper/Error label
            _helperOrError = new Label { FontSize = 12, Margin = new Thickness(12, 4, 12, 0) };
            _root.Add(_helperOrError);
            Grid.SetRow(_helperOrError, 2);

            // Password strength bar
            _passwordStrengthBar = new BoxView { HeightRequest = 3, HorizontalOptions = LayoutOptions.Fill, BackgroundColor = Colors.Transparent, IsVisible = false };
            _root.Add(_passwordStrengthBar);
            Grid.SetRow(_passwordStrengthBar, 2);

            // Error icon
            _errorIconImage = new Image { WidthRequest = 16, HeightRequest = 16, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End, IsVisible = false, Margin = new Thickness(6, 0) };
            _trailingHost.Add(_errorIconImage);

            Content = _root;

            PropertyChanged += OnSelfPropertyChanged;

            UpdateAll();

            // Ensure bottom line is updated when the native handler is ready
            this.HandlerChanged += (s, e) =>
            {
                UpdateBottomLine();
            };
        }
        #endregion

        #region EventHandlers        
        /// <summary>
        /// Entries the text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void Entry_TextChanged(object? sender, TextChangedEventArgs e)
        {
            string newText = e.NewTextValue ?? string.Empty;

            RunValidations(newText);

            // keep existing visual updates
            UpdateFloatingLabel(animate: true);
            UpdateTrailing();
            ApplyVisuals();

        }
        #endregion

        #region PropertyChanged Callbacks        
        /// <summary>
        /// Called when [self property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs" /> instance containing the event data.</param>
        private void OnSelfPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(Text) or nameof(AllowClear) or nameof(IsPassword) or nameof(IsPasswordVisible))
                UpdateTrailing();

            if (e.PropertyName == nameof(IsPasswordVisible))
                ApplyPasswordVisibility();
        }

        /// <summary>
        /// Called when [any property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnAnyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
                    => (bindable as MaterialTextField)?.UpdateAll();

        /// <summary>
        /// Called when [text property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (MaterialTextField)bindable;
            // Entry is two-way bound; ensure Entry.Text matches
            if (ctrl._entry.Text != (string)newValue)
                ctrl._entry.Text = (string)newValue ?? string.Empty;

            // run validation when Text set programmatically
            if (!string.IsNullOrWhiteSpace(ctrl.ValidationPattern))
            {
                try
                {
                    bool isValid = Regex.IsMatch((string)newValue ?? string.Empty, ctrl.ValidationPattern);
                    if (!isValid)
                    {
                        ctrl.SetValue(ErrorTextProperty, ctrl.ValidationErrorMessage);
                        ctrl.SetValue(IsErrorProperty, true);
                    }
                    else
                    {
                        if (ctrl.ErrorText == ctrl.ValidationErrorMessage)
                            ctrl.SetValue(ErrorTextProperty, string.Empty);
                        ctrl.SetValue(IsErrorProperty, false);
                    }
                }
                catch { }
            }

            ctrl.UpdateFloatingLabel(animate: true);
            ctrl.UpdateTrailing();
            ctrl.ApplyVisuals();
        }

        /// <summary>
        /// Called when [trailing changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnTrailingChanged(BindableObject bindable, object oldValue, object newValue)
                    => (bindable as MaterialTextField)?.ApplyTrailing();

        /// <summary>
        /// Called when [is password changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnIsPasswordChanged(BindableObject bindable, object oldValue, object newValue)
                    => (bindable as MaterialTextField)?.UpdateAll();

        /// <summary>
        /// Called when [password visibility changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnPasswordVisibilityChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (MaterialTextField)bindable;
            ctrl.ApplyPasswordVisibility();
            ctrl.UpdateTrailing();
            ctrl.ApplyVisuals();
        }

        /// <summary>
        /// Called when [password image changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnPasswordImageChanged(BindableObject bindable, object oldValue, object newValue)
                    => (bindable as MaterialTextField)?.UpdateToggleImage();

        /// <summary>
        /// Called when [error text changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnErrorTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (MaterialTextField)bindable;
            var text = (string)newValue;
            ctrl._helperOrError.Text = text;
            ctrl._helperOrError.IsVisible = !string.IsNullOrWhiteSpace(text);
            ctrl.ApplyVisuals();
        }

        /// <summary>
        /// Called when [visual property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnVisualPropertyChanged(BindableObject bindable, object oldValue, object newValue)
                     => (bindable as MaterialTextField)?.ApplyVisuals();
        #endregion

        #region Validation        
        /// <summary>
        /// Runs the validations.
        /// </summary>
        /// <param name="text">The text.</param>
        private void RunValidations(string text)
        {
            if (Validations.Count == 0)
                return;

            var results = Validations.Select(v => (isValid: v.Validate(text), message: v.Message)).ToArray();
            bool isValid = results.All(r => r.isValid);
            IsValid = isValid;

            if (isValid)
            {
                _helperOrError.Text = HelperText;
                _helperOrError.IsVisible = !string.IsNullOrWhiteSpace(HelperText);
                IsError = false;
            }
            else
            {
                var messages = string.Join("\n", results.Where(r => !r.isValid).Select(r => r.message));
                _helperOrError.Text = messages;
                _helperOrError.IsVisible = true;
                IsError = true;
            }

            _lastValidationState = isValid;
        }

        /// <summary>
        /// Runs the asynchronous validation.
        /// </summary>
        /// <returns></returns>
        private async Task RunAsyncValidation()
        {
            if (AsyncValidator != null)
            {
                try
                {
                    var (isValid, errorMsg) = await AsyncValidator(Text ?? string.Empty);
                    IsError = !isValid;
                    ErrorText = !isValid ? errorMsg : string.Empty;
                    UpdateHelperOrError();
                    ApplyVisuals();
                }
                catch { }
            }
        }
        #endregion

        #region Visuals        
        /// <summary>
        /// Applies the icon visibility.
        /// </summary>
        private void ApplyIconVisibility()
        {
            if (Icon != null)
            {
                _leadingIcon.Source = Icon;
                _leadingIcon.IsVisible = true;
                _leadingIcon.IsEnabled = true;
            }
            else
            {
                _leadingIcon.IsVisible = false;
                _leadingIcon.IsEnabled = false;
            }
        }

        /// <summary>
        /// Applies the password strength.
        /// </summary>
        private void ApplyPasswordStrength()
        {
            if (!PasswordStrengthEnabled || string.IsNullOrEmpty(Text))
            {
                _passwordStrengthBar.IsVisible = false;
                return;
            }

            _passwordStrengthBar.IsVisible = true;
            var score = Math.Min(Text.Length / 4.0, 1.0);
            if (score < 0.3) _passwordStrengthBar.BackgroundColor = Colors.Red;
            else if (score < 0.6) _passwordStrengthBar.BackgroundColor = Colors.Orange;
            else _passwordStrengthBar.BackgroundColor = Colors.Green;
        }

        /// <summary>
        /// Updates all.
        /// </summary>
        private void UpdateAll()
        {
            ApplyVisuals();
            ApplyIconVisibility();
            ApplyTrailing();
            UpdateFloatingLabel(animate: false);
            ApplyPasswordStrength();
            ApplyPasswordVisibility();
        }

        /// <summary>
        /// Applies the visuals.
        /// </summary>
        private void ApplyVisuals()
        {
            // 1. Border shape and stroke
            _fieldBorder.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(CornerRadius) };
            _fieldBorder.StrokeThickness = IsBorderless ? 0 : BorderThickness;

            if (Variant == MaterialTextFieldVariant.Outlined)
            {
                _fieldBorder.BackgroundColor = Colors.Transparent;
                _fieldBorder.Stroke = IsError
                    ? Colors.Red
                    : (IsBorderless ? Colors.Transparent : Colors.Gray.WithAlpha(0.6f));
            }
            else // Filled
            {
                _fieldBorder.BackgroundColor = IsError
                    ? Colors.Red.WithAlpha(0.06f)
                    : FilledColor;
                _fieldBorder.Stroke = Colors.Transparent;
            }

            // 2. Focus highlight
            if (_entry.IsFocused)
            {
                if (Variant == MaterialTextFieldVariant.Outlined)
                    _fieldBorder.Stroke = IsError ? Colors.Red : AccentColor;
                else
                    _fieldBorder.BackgroundColor = (IsError ? Colors.Red : AccentColor).WithAlpha(0.10f);
            }

            // 3. Floating label and helper/error colors
            _floatingLabel.TextColor = _entry.IsFocused ? (IsError ? Colors.Red : AccentColor) : (IsError ? Colors.Red : TitleColor);
            _helperOrError.TextColor = IsError ? Colors.Red : Colors.Gray;

            // 4. Placeholder color
            _entry.PlaceholderColor = PlaceholderColor;

            // 5. Bottom line
            UpdateBottomLine();
        }

        /// <summary>
        /// Updates the bottom line.
        /// </summary>
        private void UpdateBottomLine()
        {
#if ANDROID
            if (_entry.Handler?.PlatformView is Android.Widget.EditText nativeEditText)
            {
                // Always hide bottom line
                nativeEditText.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            }
#elif IOS || MACCATALYST
    // Always hide bottom line
    _entry.BackgroundColor = Colors.Transparent;
    //_entry.BorderColor = Colors.Transparent; // optional if you were using BorderColor
#endif
        }

        /// <summary>
        /// Applies the trailing.
        /// </summary>
        private void ApplyTrailing()
        {
            _trailingHost.Children.Clear();

            // Priority: TrailingContent -> password toggle -> clear button
            if (TrailingContent != null)
            {
                _trailingHost.Children.Add(TrailingContent);
                _clearButton.IsVisible = false;
                _toggleImageButton.IsVisible = false;
                _toggleTextButton.IsVisible = false;
                return;
            }

            if (IsPassword)
            {
                // Show image toggle if images provided; otherwise show text toggle fallback
                if (PasswordVisibleImage != null || PasswordHiddenImage != null)
                {
                    _trailingHost.Children.Add(_toggleImageButton);
                    UpdateToggleImage();
                    _toggleImageButton.IsVisible = true;
                    _toggleTextButton.IsVisible = false;
                    _clearButton.IsVisible = false;
                }
                else
                {
                    _trailingHost.Children.Add(_toggleTextButton);
                    _toggleTextButton.IsVisible = true;
                    _toggleImageButton.IsVisible = false;
                    _clearButton.IsVisible = false;
                    UpdateToggleText();
                }

                return;
            }

            // Not password - show clear button if allowed & text present
            _trailingHost.Children.Add(_clearButton);
            UpdateTrailing();
        }

        /// <summary>
        /// Updates the trailing.
        /// </summary>
        private void UpdateTrailing()
        {
            _trailingHost.Children.Clear();

            if (TrailingContent != null)
            {
                _trailingHost.Children.Add(TrailingContent);
                return;
            }

            if (IsError && ErrorIcon != null)
            {
                _errorIconImage.Source = ErrorIcon;
                _errorIconImage.IsVisible = true;
                _trailingHost.Children.Add(_errorIconImage);
            }

            if (IsPassword)
            {
                if (PasswordVisibleImage != null || PasswordHiddenImage != null)
                {
                    _trailingHost.Children.Add(_toggleImageButton);
                    _toggleImageButton.IsVisible = true;
                }
                else
                {
                    _trailingHost.Children.Add(_toggleTextButton);
                    _toggleTextButton.IsVisible = true;
                }
            }

            if (AllowClear && !IsPassword && !string.IsNullOrEmpty(Text))
            {
                _trailingHost.Children.Add(_clearButton);
                _clearButton.IsVisible = true;
            }
        }

        /// <summary>
        /// Updates the toggle image.
        /// </summary>
        private void UpdateToggleImage()
        {
            if (!IsPassword)
            {
                _toggleImageButton.IsVisible = false;
                _toggleTextButton.IsVisible = false;
                return;
            }

            var src = IsPasswordVisible ? PasswordVisibleImage : PasswordHiddenImage;
            if (src != null)
            {
                _toggleImageButton.Source = src;
                _toggleImageButton.IsVisible = true;
                _toggleTextButton.IsVisible = false;
            }
            else
            {
                // fallback to text toggle
                _toggleImageButton.IsVisible = false;
                _toggleTextButton.IsVisible = true;
                UpdateToggleText();
            }
        }

        /// <summary>
        /// Updates the toggle text.
        /// </summary>
        private void UpdateToggleText()
        {
            // fallback text if no images provided
            _toggleTextButton.Text = IsPasswordVisible ? "Hide" : "Show";
        }

        /// <summary>
        /// Applies the password visibility.
        /// </summary>
        private void ApplyPasswordVisibility()
        {
            if (!IsPassword)
            {
                _entry.IsPassword = false;
                _toggleImageButton.IsVisible = false;
                _toggleTextButton.IsVisible = false;
                return;
            }

            // Entry shows actual characters when IsPasswordVisible == true
            _entry.IsPassword = !IsPasswordVisible;
            UpdateToggleImage();
        }

        /// <summary>
        /// Updates the floating label.
        /// </summary>
        /// <param name="animate">if set to <c>true</c> [animate].</param>
        private void UpdateFloatingLabel(bool animate)
        {
            bool shouldFloat = _entry.IsFocused || !string.IsNullOrWhiteSpace(Text);

            if (string.IsNullOrWhiteSpace(Title))
            {
                _floatingLabel.Opacity = 0;
                return;
            }

            if (_isAnimating && animate) return;

            double targetY = shouldFloat ? 0 : 18;
            double targetOpacity = shouldFloat ? 1.0 : 0.0;
            double targetFont = shouldFloat ? 12 : 14;

            // Animate color: from TitleColor (unfocused) to AccentColor (focused)
            Color startColor = _floatingLabel.TextColor;
            Color endColor;
            if (_entry.IsFocused)
                endColor = IsError ? Colors.Red : AccentColor;
            else
                endColor = IsError ? Colors.Red : TitleColor;

            if (!animate)
            {
                _floatingLabel.TranslationY = targetY;
                _floatingLabel.Opacity = targetOpacity;
#if ANDROID || IOS || MACCATALYST || WINDOWS
                _floatingLabel.FontSize = targetFont;
#endif
                _floatingLabel.TextColor = endColor;
                return;
            }

            _isAnimating = true;
            var anim = new Animation();

            // Translation
            anim.Add(0, 1, new Animation(v => _floatingLabel.TranslationY = v, _floatingLabel.TranslationY, targetY, Easing.CubicOut));
            // Opacity
            anim.Add(0, 1, new Animation(v => _floatingLabel.Opacity = v, _floatingLabel.Opacity, targetOpacity, Easing.CubicOut));
            // Font size
            anim.Add(0, 1, new Animation(v =>
            {
#if ANDROID || IOS || MACCATALYST || WINDOWS
                _floatingLabel.FontSize = v;
#endif
            }, _floatingLabel.FontSize, targetFont, Easing.CubicOut));
            // Color animation
            anim.Add(0, 1, new Animation(v =>
            {
                _floatingLabel.TextColor = Color.FromRgba(
                    startColor.Red + (endColor.Red - startColor.Red) * v,
                    startColor.Green + (endColor.Green - startColor.Green) * v,
                    startColor.Blue + (endColor.Blue - startColor.Blue) * v,
                    startColor.Alpha + (endColor.Alpha - startColor.Alpha) * v
                );
            }, 0, 1));

            anim.Commit(this, "FloatLabel", length: 140u, finished: (_, __) => _isAnimating = false);

        }

        /// <summary>
        /// Updates the helper or error.
        /// </summary>
        private void UpdateHelperOrError()
        {
            if (IsError && !string.IsNullOrWhiteSpace(ErrorText))
            {
                _helperOrError.Text = ErrorText;
                _helperOrError.IsVisible = true;
            }
            else
            {
                _helperOrError.Text = HelperText;
                _helperOrError.IsVisible = !string.IsNullOrWhiteSpace(HelperText);
            }
        }

        /// <summary>
        /// Updates the state of the visual.
        /// </summary>
        /// <param name="isFocused">if set to <c>true</c> [is focused].</param>
        private void UpdateVisualState(bool isFocused)
        {
            ApplyVisuals();
            UpdateFloatingLabel(animate: true);
            UpdateTrailing();
            UpdateHelperOrError();
        }
        #endregion
    }
}
