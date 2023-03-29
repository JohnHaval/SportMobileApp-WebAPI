namespace SportMobileApp
{
    /// <summary>
    /// Класс результатов соревнований
    /// </summary>
    public partial class Results
    {
        public int TopID { get; set; }
        public int Place { get; set; }
        public int SportsmanID { get; set; }
        public int CompetitionID { get; set; }
        public double Record { get; set; }
        public Sportsmen Sportsmen
        {
            get
            {
                try
                {
                    if (ControlService.Sportsmen == null)
                        return null;
                    return ControlService.Sportsmen.Where(p => p.SportsmanID == SportsmanID).FirstOrDefault();
                }
                catch { return null; }
            }
        }
        public Competitions Competitions
        {
            get
            {
                try
                {
                    if (ControlService.Competitions == null)
                        return null;
                    return ControlService.Competitions.Where(p => p.CompetitionID == CompetitionID).FirstOrDefault();
                }
                catch { return null; }
            }
        }
    }
}
