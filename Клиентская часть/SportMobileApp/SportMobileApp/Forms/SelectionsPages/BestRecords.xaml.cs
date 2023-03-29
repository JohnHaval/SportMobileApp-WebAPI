using SportMobileApp.Models;

namespace SportMobileApp.Forms.SelectionsPages;

public partial class BestRecords : ContentPage
{
	public BestRecords()
	{
		InitializeComponent();
        SportsList.ItemsSource = ControlService.Sports;
        SportsList.SelectedIndex = 0;
        MeasuresList.ItemsSource = ControlService.Measures;
        MeasuresList.SelectedIndex = 0;
        RecordSet.SelectedIndex = 0;

    }
    private void GetList_Clicked(object sender, EventArgs e)
    {
        try
        {
            List<Results> sportsmenResult;
            if (RecordSet.SelectedIndex == 0)
                sportsmenResult = Queries.GetBestRecords(((Sports)SportsList.SelectedItem).SportID,
                    ((Measures)MeasuresList.SelectedItem).MeasureID, SecondName.Text, true);
            else sportsmenResult = Queries.GetBestRecords(((Sports)SportsList.SelectedItem).SportID,
                    ((Measures)MeasuresList.SelectedItem).MeasureID, SecondName.Text, false);

            if (sportsmenResult.Count() != 0)
                ObjectsList.ItemsSource = sportsmenResult;

            else DisplayAlert("Вывод", "Записей не найдено", "OK");
        }
        catch (FormatException)
        {
            DisplayAlert("Ошибка", "Некорректно введено значение года проведения!", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }

    private void Clear_Clicked(object sender, EventArgs e)
    {
        ObjectsList.ItemsSource = null;
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}