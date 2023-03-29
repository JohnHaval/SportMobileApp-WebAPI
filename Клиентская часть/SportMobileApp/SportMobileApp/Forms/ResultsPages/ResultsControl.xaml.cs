namespace SportMobileApp.Forms.ResultsPages;

public partial class ResultsControl : ContentPage
{
	public ResultsControl()
	{
		InitializeComponent();
        SportsmenList.ItemsSource = ControlService.Sportsmen;
        SportsmenList.SelectedIndex = 0;
        CompetitionsList.ItemsSource = ControlService.Competitions;
        CompetitionsList.SelectedIndex = 0;
    }
    public ResultsControl(Results result)
    {
        InitializeComponent();
        Result = result;
        SportsmenList.ItemsSource = ControlService.Sportsmen;
        SportsmenList.SelectedItem = result.Sportsmen;
        CompetitionsList.ItemsSource = ControlService.Competitions;
        CompetitionsList.SelectedItem = result.Competitions;

        Place.Text = result.Place.ToString();
        Record.Text = result.Record.ToString();

        Title = "�������������� ����������";
        Act.Text = "��������";
        Remove.IsVisible = true;
    }
    Results Result;

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void Act_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (Place.Text == null) throw new Exception("�������� ����� �� ����� ���� ������!");
            if (Place.Text.Length == 0) throw new Exception("����� �� ����� ��������� ���������� �������� ������ 50 ��� ������ 0!");
            if (Record.Text == null) throw new Exception("�������� ������� �� ����� ���� ������!");
            if (Record.Text.Length == 0) throw new Exception("�������� ������� �� ����� ���� ������!");

            int place = Convert.ToInt32(Place.Text);
            double record = Convert.ToDouble(Record.Text);

            if (Result == null)
            {
                Results result = new Results()
                {
                    TopID = ControlService.Results.Max(p => p.TopID) + 1,
                    Place = place,
                    SportsmanID = ((Sportsmen)SportsmenList.SelectedItem).SportsmanID,
                    CompetitionID = ((Competitions)CompetitionsList.SelectedItem).CompetitionID,
                    Record = record,
                };
                ControlService.Post(result, "Results");
                ControlService.Results.Add(result);
                await Navigation.PopAsync();
            }
            else
            {
                Result.Place = place;
                Result.SportsmanID = ((Sportsmen)SportsmenList.SelectedItem).SportsmanID;
                Result.CompetitionID = ((Competitions)CompetitionsList.SelectedItem).CompetitionID;
                Result.Record = record;

                ControlService.Put(Result, Result.TopID, "Results");
                await Navigation.PopAsync();
            }
        }
        catch (FormatException)
        {
            await DisplayAlert("������", "������� ������� �������� '�����' ��� '���������'", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("������", ex.Message, "OK");
        }
    }

    private async void Remove_Clicked(object sender, EventArgs e)
    {
        try
        {
            ControlService.Delete(Result, Result.TopID, "Results");
            ControlService.Results.Remove(Result);
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("������", "������ ������� ��������� ������, ��� ��� ��� ������������!", "OK");
        }
    }
    private void CompetitionsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CompetitionsList.SelectedItem != null)
		SportName.Text = ((Competitions)CompetitionsList.SelectedItem).Sports.Name;
    }
}