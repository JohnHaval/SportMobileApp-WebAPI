namespace SportMobileApp.Forms.SportsPages;

public partial class SportsControl : ContentPage
{
	public SportsControl()
	{
		InitializeComponent();
        MeasuresList.ItemsSource = ControlService.Measures;
        MeasuresList.SelectedIndex = 0;
    }
    public SportsControl(Sports sport)
    {
        InitializeComponent();
        Sport = sport;
        MeasuresList.ItemsSource = ControlService.Measures;
        MeasuresList.SelectedItem = sport.Measures;

        Name.Text = sport.Name;
        WorldRecord.Text = sport.WorldRecord.ToString();
        RecordDate.Date = sport.RecordDate;

        Title = "Редактирование вида спорта";
        Act.Text = "Изменить";
        Remove.IsVisible = true;
    }
    Sports Sport;

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
            if (WorldRecord.Text == null) throw new Exception("Значение мирового рекорда не может быть пустым!");
            if (WorldRecord.Text.Length == 0) throw new Exception("Значение мирового рекорда не может быть пустым!");
            double worldRecord = Convert.ToDouble(WorldRecord.Text);
            if (Sport == null)
            {
                Sports sport = new Sports()
                {
                    SportID = ControlService.Sports.Max(p => p.SportID) + 1,
                    Name = Name.Text,
                    MeasureID = ((Measures)MeasuresList.SelectedItem).MeasureID,
                    WorldRecord = worldRecord,
                    RecordDate = RecordDate.Date
                };
                ControlService.Post(sport, "Sports");
                ControlService.Sports.Add(sport);
                await Navigation.PopAsync();
            }
            else
            {                
                Sport.Name = Name.Text;
                Sport.MeasureID = ((Measures)MeasuresList.SelectedItem).MeasureID;
                Sport.WorldRecord = worldRecord;
                Sport.RecordDate = RecordDate.Date;

                ControlService.Put(Sport, Sport.SportID, "Sports");
                await Navigation.PopAsync();
            }
        }
        catch (FormatException)
        {
            await DisplayAlert("Ошибка", "Неверно введено значение 'Дата мирового рекорда'", "OK");
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
            ControlService.Delete(Sport, Sport.SportID, "Sports");
            ControlService.Sports.Remove(Sport);
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("Ошибка", "Нельзя удалить выбранную запись, так как она используется!", "OK");
        }
    }
}