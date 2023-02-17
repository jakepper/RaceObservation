namespace UI;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnBrowseClicked(object sender, EventArgs e)
	{
		var supportedFileTypes = new[] { ".csv" };

		var fileTypes = new FilePickerFileType(
			new Dictionary<DevicePlatform, IEnumerable<string>>
			{
				{ DevicePlatform.WinUI, supportedFileTypes},
				{ DevicePlatform.MacCatalyst, supportedFileTypes}
			}
		);

		var result = await FilePicker.PickAsync(
			new PickOptions { PickerTitle = "CSV Selector", FileTypes = fileTypes }
		);

		if (result == null) return;

		await DisplayAlert("You Selected...", $"{result.FileName} from,\n{result.FullPath}", "done");
	}
}

