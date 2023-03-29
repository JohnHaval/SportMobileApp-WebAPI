namespace SportMobileApp.Forms.SportsPages;

public partial class MainSports : ContentPage
{
	public MainSports()
	{
		InitializeComponent();
        RefreshItems();
    }
    private async void ObjectsList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        await Navigation.PushAsync(new SportsControl((Sports)ObjectsList.SelectedItem));
    }

    private async void AddObject_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SportsControl());
    }

    private void Refresh_Clicked(object sender, EventArgs e)
    {
        try
        {
            ControlService.Sports = ControlService.GetItems<Sports>("Sports");
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
        ObjectsList.ItemsSource = ControlService.Sports;
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}