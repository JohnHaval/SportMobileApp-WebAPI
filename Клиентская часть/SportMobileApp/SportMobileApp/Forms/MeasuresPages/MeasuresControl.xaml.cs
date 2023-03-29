namespace SportMobileApp.Forms.MeasuresPages;

public partial class MeasuresControl : ContentPage
{
	public MeasuresControl()
	{
		InitializeComponent();
	}
    public MeasuresControl(Measures measure)
    {
        InitializeComponent();
        Measure = measure;

        Name.Text = measure.Name;

        Title = "Редактирование единицы измерения";
        Act.Text = "Изменить";
        Remove.IsVisible = true;
    }
    Measures Measure;

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
            if (Measure == null)
            {
                Measures measure = new Measures()
                {
                    MeasureID = ControlService.Measures.Max(p => p.MeasureID) + 1,
                    Name = Name.Text,
                };
                ControlService.Post(measure, "Measures");
                ControlService.Measures.Add(measure);
                await Navigation.PopAsync();
            }
            else
            {
                Measure.Name = Name.Text;
                ControlService.Put(Measure, Measure.MeasureID, "Measures");
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
            ControlService.Delete(Measure, Measure.MeasureID, "Measures");
            ControlService.Measures.Remove(Measure);
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("Ошибка", "Нельзя удалить выбранную запись, так как она используется!", "OK");
        }
    }
}