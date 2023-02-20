using Program;
using Program.Entities;

namespace UI.Pages;

public partial class Menu : ContentPage
{
	public Controller Controller = new();
	public List<Racer> Subjects;
	public List<string> Observers;
	public Menu(string racersFilePath, string groupsFilePath, string sensorsFilePath)
	{
		InitializeComponent();

		Controller.Setup(racersFilePath, groupsFilePath, sensorsFilePath);
		Observers = Controller.GetObservers();
    }

	public void OnMoveRacers(object sender, EventArgs e)
	{

	}

	public void OnSelectObserver(object sender, EventArgs e)
	{
		Subjects = Controller.GetSubjects("screen here");
	}

	public void OnCreateScreen(object sender, EventArgs e)
	{
		string title = "title";
		// Controller.CreateScreen(title);
		Application.Current.OpenWindow(new Window(new ContentPage()));
	}
}