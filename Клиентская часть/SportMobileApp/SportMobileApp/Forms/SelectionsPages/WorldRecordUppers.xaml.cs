using SportMobileApp.Models;

namespace SportMobileApp.Forms.SelectionsPages;

public partial class WorldRecordUppers : ContentPage
{
	public WorldRecordUppers()
	{
		InitializeComponent();
        MeasuresList.ItemsSource = ControlService.Measures;
        MeasuresList.SelectedIndex = 0;
        RecordSet.SelectedIndex = 0;
    }
    private void GetList_Clicked(object sender, EventArgs e)
    {
        try
        {
            ObjectsList.ItemsSource = null;

            List<Results> sportsmenResult;
            if (RecordSet.SelectedIndex == 0)
                sportsmenResult = Queries.GetWorldRecordUppers(((Measures)MeasuresList.SelectedItem).MeasureID, true);
            else sportsmenResult = Queries.GetWorldRecordUppers(((Measures)MeasuresList.SelectedItem).MeasureID, false);

            if (sportsmenResult.Count() != 0)
                ObjectsList.ItemsSource = sportsmenResult;

            else DisplayAlert("Вывод", "Побивших мировой рекорд не найдено", "OK");
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