using SportMobileApp.Models;

namespace SportMobileApp.Forms.SelectionsPages;

public partial class CompetitionTop : ContentPage
{
	public CompetitionTop()
	{
		InitializeComponent();
        SportsList.ItemsSource = ControlService.Sports;
        SportsList.SelectedIndex = 0;
	}
    private void GetList_Clicked(object sender, EventArgs e)
    {
        try
        {
            ObjectsList.ItemsSource = null;            
            if (SportsList.SelectedItem == null) throw new Exception("��� ������ �� ������!");

            var results = Queries.GetCompetitionTop(((Sports)SportsList.SelectedItem).SportID, 
                Venue.Text, CompetitionName.Text, EventYear.Text);

            if (results.Count() != 0)
                ObjectsList.ItemsSource = results;

            else DisplayAlert("�����", "������� �� �������", "OK");
        }
        catch (FormatException)
        {
            DisplayAlert("������", "����������� ������� �������� ���� ����������!", "OK");
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