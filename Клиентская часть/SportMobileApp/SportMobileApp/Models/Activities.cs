namespace SportMobileApp
{
    /// <summary>
    /// Класс занятий спортсменов
    /// </summary>
    public partial class Activities
    {
        public int ActivityID { get; set; }
        public int SportID { get; set; }
        public int SportsmanID { get; set; }
        public int SportCategoryID { get; set; }
        public int? TeamID { get; set; }
        public Sports Sports
        {
            get
            {
                try
                {
                    if (ControlService.Sports == null) return null;
                    return ControlService.Sports.Where(p => p.SportID == SportID).FirstOrDefault();
                }
                catch { return null; }
            }
        }
        public Sportsmen Sportsmen
        {
            get
            {
                try
                {
                    if (ControlService.Sportsmen == null) return null;
                    return ControlService.Sportsmen.Where(p => p.SportsmanID == SportsmanID).FirstOrDefault();
                }
                catch { return null; }
            }
        }
        public SportCategories SportCategories
        {
            get
            {
                try
                {
                    if (ControlService.SportCategories == null) return null;
                    return ControlService.SportCategories.Where(p => p.SportCategoryID == SportCategoryID).FirstOrDefault();
                }
                catch { return null; }
            }
        }
        public Teams Teams
        {
            get
            {
                try
                {
                    if (ControlService.Teams == null) return null;
                    if (TeamID == null) throw new Exception();
                    return ControlService.Teams.Where(p => p.TeamID == TeamID).FirstOrDefault();
                }
                catch { return null; }
            }
        }
    }
}
