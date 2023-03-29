namespace SportMobileApp.Forms.SportCategoriesPages;

public partial class SportCategoriesControl : ContentPage
{
	public SportCategoriesControl()
	{
		InitializeComponent();
	}
    public SportCategoriesControl(SportCategories sportCategory)
    {
        InitializeComponent();
        SportCategory = sportCategory;

        Name.Text = sportCategory.Name;

        Title = "Редактирование разряда";
        Act.Text = "Изменить";
        Remove.IsVisible = true;
    }
    SportCategories SportCategory;

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void Act_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (Name.Text == null) throw new Exception("Значение имени не может быть пустым!");
            if (Name.Text.Length == 0 || Name.Text.Length > 50) throw new Exception("Имя не может содержать количество символов больше 50 или равное 0!");
            if (SportCategory == null)
            {
                SportCategories sportCategory = new SportCategories()
                {
                    SportCategoryID = ControlService.SportCategories.Max(p => p.SportCategoryID) + 1,
                    Name = Name.Text,
                };
                ControlService.Post(sportCategory, "SportCategories");
                ControlService.SportCategories.Add(sportCategory);
                await Navigation.PopAsync();
            }
            else
            {
                SportCategory.Name = Name.Text;
                ControlService.Put(SportCategory, SportCategory.SportCategoryID, "SportCategories");
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
            ControlService.Delete(SportCategory, SportCategory.SportCategoryID, "SportCategories");
            ControlService.SportCategories.Remove(SportCategory);
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("Ошибка", "Нельзя удалить выбранную запись, так как она используется!", "OK");
        }
    }
}