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
            if (ActivitiesCount.Text == null) throw new Exception("�������� ���������� ������� �� ����� ���� ������!");
            if (ActivitiesCount.Text.Length == 0) throw new Exception("�������� ���������� ������� �� ����� ���� ������!");                  

            var sportsmenResult = Queries.GetSportsmenSports(Convert.ToInt32(ActivitiesCount.Text));
            
            if (sportsmenResult.Count() != 0)
                ObjectsList.ItemsSource = sportsmenResult;

            else DisplayAlert("�����", "����������� � ����� ����������� ������� �� �������", "OK");
        }
        catch (FormatException)
        {
            DisplayAlert("������", "����������� ������� �������� ���������� �������!", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("������", ex.Message, "OK");
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