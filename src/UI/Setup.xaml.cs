using UI.Pages;

namespace UI;

public partial class Setup : ContentPage
{
	private string racersFilePath;
	public string RacersFilePath
    {
        get {
            return racersFilePath;
        }
        set {
            racersFilePath = value;
			OnPropertyChanged();
        }
    }

	private string groupsFilePath;
	public string GroupsFilePath
    {
        get {
            return groupsFilePath;
        }
        set {
            groupsFilePath = value;
			OnPropertyChanged();
        }
    }

	private string sensorsFilePath;
	public string SensorsFilePath
    {
        get {
            return sensorsFilePath;
        }
        set {
            sensorsFilePath = value;
			OnPropertyChanged();
        }
    }
	public Setup()
	{
		InitializeComponent();
		BindingContext = this;
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

		var button = (Button)sender;
		switch (button.ClassId)
		{
			case "racer":
				{
					RacersFilePath = result.FullPath;
					break;
				}
			case "group":
				{
					GroupsFilePath = result.FullPath;
					break;
				}
			case "sensor":
				{
					SensorsFilePath = result.FullPath;
					break;
				}
		}
	}

    private void OnStartClicked(object sender, EventArgs e)
    {
		Navigation.PushModalAsync(new Menu(RacersFilePath, GroupsFilePath, SensorsFilePath));
		Navigation.RemovePage(this);
    }
}

