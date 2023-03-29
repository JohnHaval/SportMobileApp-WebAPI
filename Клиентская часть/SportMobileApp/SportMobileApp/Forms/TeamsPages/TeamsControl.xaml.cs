namespace SportMobileApp.Forms.TeamsPages;

public partial class TeamsControl : ContentPage
{
	public TeamsControl()
	{
		InitializeComponent();
	}
    public TeamsControl(Teams team)
    {
        InitializeComponent();
        Team = team;

        Name.Text = team.Name;

        Title = "�������������� �������";
        Act.Text = "��������";
        Remove.IsVisible = true;
    }
    Teams Team;

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void Act_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (Name.Text == null) throw new Exception("�������� ����� �� ����� ���� ������!");
            if (Name.Text.Length == 0 || Name.Text.Length > 50) throw new Exception("��� �� ����� ��������� ���������� �������� ������ 50 ��� ������ 0!");
            if (Team == null)
            {
                Teams team = new Teams()
                {
                    TeamID = ControlService.Teams.Max(p => p.TeamID) + 1,
                    Name = Name.Text,
                };
                ControlService.Post(team, "Teams");
                ControlService.Teams.Add(team);
                await Navigation.PopAsync();
            }
            else
            {
                Team.Name = Name.Text;
                ControlService.Put(Team, Team.TeamID, "Teams");
                await Navigation.PopAsync();
            }
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
            ControlService.Delete(Team, Team.TeamID, "Teams");
            ControlService.Teams.Remove(Team);
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("������", "������ ������� ��������� ������, ��� ��� ��� ������������!", "OK");
        }
    }
}