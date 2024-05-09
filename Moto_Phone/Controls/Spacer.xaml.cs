namespace Moto_Phone.Controls;

public partial class Spacer : ContentView
{
	public Spacer()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty SpacePropert = 
		BindableProperty.Create(nameof(Space), typeof(int), typeof(Spacer), defaultValue: 10);

	private int myVar;

	public int Space
	{
		get => (int)GetValue(SpacePropert);
		set => SetValue(SpacePropert, value);
	}
}