namespace PWApp.Views.Templates;

public partial class ScannedCardTemplate : ContentView
{
	public ScannedCardTemplate()
	{
		InitializeComponent();
	}
    public static readonly BindableProperty CustomerNameProperty = BindableProperty.Create(
    propertyName: nameof(CustomerName),
    returnType: typeof(string),
    declaringType: typeof(GeneratedCardTempalte));
    public string CustomerName
    {
        get => (string)GetValue(CustomerNameProperty);
        set => SetValue(CustomerNameProperty, value);
    }

    public static readonly BindableProperty CustomerEmailProperty = BindableProperty.Create(
        propertyName: nameof(CustomerEmail),
        returnType: typeof(string),
        declaringType: typeof(GeneratedCardTempalte));
    public string CustomerEmail
    {
        get => (string)GetValue(CustomerEmailProperty);
        set => SetValue(CustomerEmailProperty, value);
    }

    public static readonly BindableProperty JobTitleProperty = BindableProperty.Create(
        propertyName: nameof(JobTitle),
        returnType: typeof(string),
        declaringType: typeof(GeneratedCardTempalte));
    public string JobTitle
    {
        get => (string)GetValue(JobTitleProperty);
        set => SetValue(JobTitleProperty, value);
    }

    public static readonly BindableProperty PhoneNumberProperty = BindableProperty.Create(
        propertyName: nameof(PhoneNumber),
        returnType: typeof(string),
        declaringType: typeof(GeneratedCardTempalte));
    public string PhoneNumber
    {
        get => (string)GetValue(PhoneNumberProperty);
        set => SetValue(PhoneNumberProperty, value);
    }

    public static readonly BindableProperty CompanyNameProperty = BindableProperty.Create(
        propertyName: nameof(CompanyName),
        returnType: typeof(string),
        declaringType: typeof(GeneratedCardTempalte));
    public string CompanyName
    {
        get => (string)GetValue(CompanyNameProperty);
        set => SetValue(CompanyNameProperty, value);
    }
}