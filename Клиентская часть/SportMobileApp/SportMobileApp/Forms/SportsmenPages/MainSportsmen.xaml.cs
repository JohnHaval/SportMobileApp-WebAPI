namespace SportMobileApp.Forms.SportsmenPages;

public partial class MainSportsmen : ContentPage
{
	public MainSportsmen()
	{
		InitializeComponent();
        RefreshItems();
    }
    private async void ObjectsList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        await Navigation.PushAsync(new SportsmenControl((Sportsmen)ObjectsList.SelectedItem));
    }

    private async void AddObject_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SportsmenControl());
    }

    private void Refresh_Clicked(object sender, EventArgs e)
    {
        try
        {
            ControlService.Sportsmen = ControlService.GetItems<Sportsmen>("Sportsmen");
            RefreshItems();
        }
        catch
        {
            DisplayAlert("Ошибка сервера", "Невозможно получить доступ к данным! Сервер недоступен!", "OK");
            Navigation.PopAsync();
        }
    }

    private void Clear_Clicked(object sender, EventArgs e)
    {
        ObjectsList.ItemsSource = null;
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        RefreshItems();
    }
    private void RefreshItems()
    {
        if (ObjectsList.ItemsSource != null) ObjectsList.ItemsSource = null;
        ObjectsList.ItemsSource = ControlService.Sportsmen;
    }
    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}