namespace SportMobileApp
{
    /// <summary>
    /// Класс видов спорта
    /// </summary>
    public partial class Sports
    {
        public int SportID { get; set; }
        public string Name { get; set; }
        public int MeasureID { get; set; }
        public double WorldRecord { get; set; }
        public DateTime RecordDate { get; set; }
        public Measures Measures
        {
            get
            {
                try
                {
                    if (ControlService.Measures == null)
                        return null;
                    return ControlService.Measures.Where(p => p.MeasureID == MeasureID).FirstOrDefault();
                }
                catch { return null; }
            }
        }
    }
}
