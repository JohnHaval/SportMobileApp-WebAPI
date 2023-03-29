namespace SportMobileApp
{
    /// <summary>
    /// Класс соревнований
    /// </summary>
    public partial class Competitions
    {
        public int CompetitionID { get; set; }
        public string Name { get; set; }
        public System.DateTime EventDate { get; set; }
        public string Venue { get; set; }
        public int SportID { get; set; }
        public Sports Sports
        {
            get
            {
                try
                {
                    if (ControlService.Sports == null)
                        return null;
                    return ControlService.Sports.Where(p => p.SportID == SportID).FirstOrDefault();
                }
                catch { return null; }
            }
        }
    }
}
