using System.Xml.Linq;

namespace SportMobileApp.Forms.ActivitiesPages;

public partial class ActivitiesControl : ContentPage
{
	public ActivitiesControl()
	{
		InitializeComponent();
        SportsmenList.ItemsSource = ControlService.Sportsmen;
        SportsmenList.SelectedIndex = 0;
        SportsList.ItemsSource = ControlService.Sports;
        SportsList.SelectedIndex = 0;
        SportCategoriesList.ItemsSource = ControlService.SportCategories;
        SportCategoriesList.SelectedIndex = 0;
        TeamsList.ItemsSource = ControlService.Teams;
        TeamsList.SelectedIndex = 0;
    }
    public ActivitiesControl(Activities activity)
    {
        InitializeComponent();
        Activity = activity;
        SportsmenList.ItemsSource = ControlService.Sportsmen;
        SportsmenList.SelectedItem = activity.Sportsmen;
        SportsList.ItemsSource = ControlService.Sports;
        SportsList.SelectedItem = activity.Sports;
        SportCategoriesList.ItemsSource = ControlService.SportCategories;
        SportCategoriesList.SelectedItem = activity.SportCategories;
        TeamsList.ItemsSource = ControlService.Teams;
        if (activity.Teams != null) TeamsList.SelectedItem = activity.Teams;
        else
        {
            HasTeam.IsChecked = true;
            TeamsList.SelectedIndex = 0;
        }

        Title = "Редактирование занятия";
        Act.Text = "Изменить";
        Remove.IsVisible = true;
    }
    Activities Activity;

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void Act_Clicked(object sender, EventArgs e)
    {
        try
        {
            int sportmanID = ((Sportsmen)SportsmenList.SelectedItem).SportsmanID;
            int sportID = ((Sports)SportsList.SelectedItem).SportID;            

            int? teamID;
            if (HasTeam.IsChecked != true) teamID = ((Teams)TeamsList.SelectedItem).TeamID;
            else teamID = null;
            if (Activity == null)
            {
                if (ControlService.Activities != null)
                if (ControlService.Activities.Where(p => p.SportID == sportID && p.SportsmanID == sportmanID).Count() != 0)
                    throw new Exception("Данный спортсмен уже занимается выбранным спортом!");

                Activities activity = new Activities()
                {
                    ActivityID = ControlService.Activities.Max(p => p.ActivityID) + 1,
                    SportID = sportID,
                    SportsmanID = sportmanID,
                    SportCategoryID = ((SportCategories)SportCategoriesList.SelectedItem).SportCategoryID,
                    TeamID = teamID
                };
                ControlService.Post(activity, "Activities");
                ControlService.Activities.Add(activity);
                await Navigation.PopAsync();
            }
            else
            {
                if (Activity.SportID != sportID || Activity.SportsmanID != sportmanID)
                {
                    if (ControlService.Activities != null)
                        if (ControlService.Activities.Where(p => p.SportID == sportID && p.SportsmanID == sportmanID).Count() != 0)
                        throw new Exception("Данный спортсмен уже занимается выбранным спортом!");
                }
                Activity.SportID = sportID;
                Activity.SportsmanID = sportmanID;
                Activity.SportCategoryID = ((SportCategories)SportCategoriesList.SelectedItem).SportCategoryID;
                Activity.TeamID = teamID;

                ControlService.Put(Activity, Activity.ActivityID, "Activities");
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
            ControlService.Delete(Activity, Activity.ActivityID, "Activities");
            ControlService.Activities.Remove(Activity);
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("Ошибка", "Нельзя удалить выбранную запись, так как она используется!", "OK");
        }
    }

    private void HasTeam_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) TeamStack.IsVisible = false;
        else TeamStack.IsVisible = true;
    }
}