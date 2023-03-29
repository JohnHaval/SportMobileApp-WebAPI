using System.Xml.Linq;

namespace SportMobileApp.Forms.SportsmenPages;

public partial class SportsmenControl : ContentPage
{
	public SportsmenControl()
	{
		InitializeComponent();
	}
    public SportsmenControl(Sportsmen sportsman)
    {
        InitializeComponent();
        Sportsman = sportsman;

        FirstName.Text = sportsman.FirstName;
        SecondName.Text = sportsman.SecondName;
        ThirdName.Text = sportsman.ThirdName;
        BornDate.Date = sportsman.BornDate;

        Title = "Редактирование спортсмена";
        Act.Text = "Изменить";
        Remove.IsVisible = true;
    }
    Sportsmen Sportsman;

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void Act_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (SecondName.Text == null) throw new Exception("Значение фамилии не может быть пустым!");
            if (SecondName.Text.Length == 0 || SecondName.Text.Length > 50) throw new Exception("Фамилия не может содержать количество символов больше 50 или равное 0!");
            if (FirstName.Text == null) throw new Exception("Значение имени не может быть пустым!");
            if (FirstName.Text.Length == 0 || FirstName.Text.Length > 50) throw new Exception("Имя не может содержать количество символов больше 50 или равное 0!");

            string thirdName;
            if (ThirdName.Text != null)

                if (ThirdName.Text.Length == 0) thirdName = null;
                else if (ThirdName.Text.Length > 50) throw new Exception("Отчество не может содержать количество символов больше 50!");
                else thirdName = ThirdName.Text;

            else thirdName = ThirdName.Text;

            if (Sportsman == null)
            {
                Sportsmen sportsman = new Sportsmen()
                {
                    SportsmanID = ControlService.Sportsmen.Max(p => p.SportsmanID) + 1,
                    FirstName = FirstName.Text,
                    SecondName = SecondName.Text,
                    ThirdName = thirdName,
                    BornDate = BornDate.Date
                };
                ControlService.Post(sportsman, "Sportsmen");
                ControlService.Sportsmen.Add(sportsman);
                await Navigation.PopAsync();
            }
            else
            {
                Sportsman.FirstName = FirstName.Text;
                Sportsman.SecondName = SecondName.Text;
                Sportsman.ThirdName = thirdName;
                Sportsman.BornDate = BornDate.Date;
                ControlService.Put(Sportsman, Sportsman.SportsmanID, "Sportsmen");
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
            ControlService.Delete(Sportsman, Sportsman.SportsmanID, "Sportsmen");
            ControlService.Sportsmen.Remove(Sportsman);
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("Ошибка", "Нельзя удалить выбранную запись, так как она используется!", "OK");
        }
    }
}