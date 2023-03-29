namespace SportMobileApp
{
    /// <summary>
    /// Класс спортсменов
    /// </summary>
    public partial class Sportsmen
    {
        public int SportsmanID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string SFT
        {
            get
            {
                if (ThirdName == null) return SecondName + " " + FirstName;
                else return SecondName + " " + FirstName + " " + ThirdName;            
            }
        }
        public System.DateTime BornDate { get; set; }
    }
}
