using SportMobileApp.Forms.ActivitiesPages;
using SportMobileApp.Forms.CompetitionsPages;
using SportMobileApp.Forms.MeasuresPages;
using SportMobileApp.Forms.ResultsPages;
using SportMobileApp.Forms.SelectionsPages;
using SportMobileApp.Forms.SportCategoriesPages;
using SportMobileApp.Forms.SportsmenPages;
using SportMobileApp.Forms.SportsPages;
using SportMobileApp.Forms.TeamsPages;

namespace SportMobileApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void SportsPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainSports());
    }

    private async void MeasuresPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainMeasures());
    }

    private async void SportsmenPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainSportsmen());
    }

    private async void ActivitiesPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainActivities());
    }

    private async void SportCategoriesPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainSportCategories());
    }

    private async void TeamsPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainTeams());
    }

    private async void CompetitionsPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainCompetitions());
    }

    private async void ResultsPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainResults());
    }

    private void Exit_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    private async void CompetitionResults_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CompetitionTop());
    }

    private async void ActivitiesCount_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SportsmenSports());
    }

    private async void WorldRecordUppers_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WorldRecordUppers());
    }

    private async void BestRecords_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BestRecords());
    }
}

