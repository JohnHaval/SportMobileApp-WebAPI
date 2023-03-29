namespace SportMobileApp.Forms.CompetitionsPages;

public partial class CompetitionsControl : ContentPage
{
	public CompetitionsControl()
	{
		InitializeComponent();
        SportsList.ItemsSource = ControlService.Sports;
        SportsList.SelectedIndex = 0;
    }
    public CompetitionsControl(Competitions competition)
    {
        InitializeComponent();
        Competition = competition;
        SportsList.ItemsSource = ControlService.Sports;
        SportsList.SelectedItem = competition.Sports;

        Name.Text = competition.Name;
        Venue.Text = competition.Venue;
        EventDate.Date = competition.EventDate;

        Title = "Редактирование соревнования";
        Act.Text = "Изменить";
        Remove.IsVisible = true;
    }
    Competitions Competition;

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void Act_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (Name.Text == null) throw new Exception("Значение имени не может быть пустым!");
            if (Name.Text.Length == 0 || Name.Text.Length > 50) throw new Exception("Имя не может содержать количество символов больше 50 или равное 0!");
            if (Venue.Text == null) throw new Exception("Значение места проведения не может быть пустым!");
            if (Venue.Text.Length == 0) throw new Exception("Значение места проведения не может быть пустым!");
            if (Competition == null)
            {
                Competitions competition = new Competitions()
                {
                    CompetitionID = ControlService.Competitions.Max(p => p.CompetitionID) + 1,
                    Name = Name.Text,
                    SportID = ((Sports)SportsList.SelectedItem).SportID,
                    Venue = Venue.Text,
                    EventDate = EventDate.Date
                };
                ControlService.Post(competition, "Competitions");
                ControlService.Competitions.Add(competition);
                await Navigation.PopAsync();
            }
            else
            {
                Competition.Name = Name.Text;
                Competition.SportID = ((Sports)SportsList.SelectedItem).SportID;
                Competition.Venue = Venue.Text;
                Competition.EventDate = EventDate.Date;

                ControlService.Put(Competition, Competition.CompetitionID, "Competitions");
                await Navigation.PopAsync();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }

    private async void Remove_Clicked(object sender, EventArgs e)
    {
        try
        {
            ControlService.Delete(Competition, Competition.CompetitionID, "Competitions");
            ControlService.Competitions.Remove(Competition);
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("Ошибка", "Нельзя удалить выбранную запись, так как она используется!", "OK");
        }
    }
}