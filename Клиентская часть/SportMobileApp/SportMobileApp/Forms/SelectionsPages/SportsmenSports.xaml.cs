using SportMobileApp.Models;

namespace SportMobileApp.Forms.SelectionsPages;

public partial class SportsmenSports : ContentPage
{
	public SportsmenSports()
	{
		InitializeComponent();
	}
    private void GetList_Clicked(object sender, EventArgs e)
    {
        try
        {
            ObjectsList.ItemsSource = null;
            if (ActivitiesCount.Text == null) throw new Exception("Значение количества занятий не может быть пустым!");
            if (ActivitiesCount.Text.Length == 0) throw new Exception("Значение количества занятий не может быть пустым!");                  

            var sportsmenResult = Queries.GetSportsmenSports(Convert.ToInt32(ActivitiesCount.Text));
            
            if (sportsmenResult.Count() != 0)
                ObjectsList.ItemsSource = sportsmenResult;

            else DisplayAlert("Вывод", "Спортсменов с таким количеством занятий не найдено", "OK");
        }
        catch (FormatException)
        {
            DisplayAlert("Ошибка", "Некорректно введено значение количества занятий!", "OK");
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