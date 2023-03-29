namespace SportMobileApp.Models
{
    /// <summary>
    /// Класс, отвечающий за выборку данных
    /// </summary>
    public static class Queries
    {
        /// <summary>
        /// Получает записи на основании указанного спорта, места проведения, имени соревнования и
        /// его годе проведения.Возвращает список результатов. 
        /// Логика получения результатов соревнования: выбирается вид спорта, вводится место проведения в точности,
        /// имя соревнования (можно написать часть полного имени), а также указывается точный год проведения.
        /// </summary>
        /// <param name="sportID">код спорта</param>
        /// <param name="venue">место проведения</param>
        /// <param name="competitionName">имя соревнования</param>
        /// <param name="eventYear">год проведения</param>
        /// <returns></returns>
        public static List<Results> GetCompetitionTop(int sportID, string venue, string competitionName, string eventYear)
        {
            if (ControlService.Results == null) throw new Exception("Результатов соревнований нет в базе!");
            if (venue == null) venue = "";
            if (competitionName == null) competitionName = "";
            if (eventYear == null) eventYear = "";
            return ControlService.Results.Where(p => p.Competitions.SportID == sportID
                && p.Competitions.Venue == venue
                && p.Competitions.Name.ToLower().Contains(competitionName.ToLower())
                && p.Competitions.EventDate.Year.ToString() == eventYear).ToList();
        }
        /// <summary>
        /// Получает число записей, с которым будет сравниваться по условию "более, чем в указанном".
        /// Результатом будет список спортсменов. 
        /// Логика подсчета видов спорта для каждого спортсмена: 
        /// Каждый спортсмен проверяется выбирается через foreach и в таблице Activities (занятий спортсменов) ведется поиск по ID. 
        /// Найденное кол-во записей сравнивается с указанным кол-вом в поле по условию "найденное больше, чем указанное" и производится вывод.
        /// </summary>
        /// <param name="activitiesCount">число видов спорта, которое должно быть превышено для отобора спортсменов</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<Sportsmen> GetSportsmenSports(int activitiesCount)
        {
            if (ControlService.Sportsmen == null) throw new Exception("Спортсменов нет в базе!");
            if (ControlService.Activities == null) throw new Exception("Занятий спортсменов нет в базе!");
            if (activitiesCount < 0) throw new Exception("Количество занятий не может быть меньше 0!");
            List<Sportsmen> sportsmenResult = new List<Sportsmen>();
            foreach (var item in ControlService.Sportsmen)
            {
                List<Activities> list = ControlService.Activities.Where(p => p.SportsmanID == item.SportsmanID).ToList();
                int gottenCount = list.Count();
                if (gottenCount > activitiesCount) sportsmenResult.Add(item);
            }
            return sportsmenResult;
        }
        /// <summary>
        /// Возвращает спортсменов, побивших мировой рекорд. 
        /// Логика получения: относительно указанной единицы измерения и стороны "превышения" рекорда 
        /// производится поиск по каждому спортсмену. Записи собираются вручную.
        /// </summary>
        /// <param name="measureID">код единицы измерения</param>
        /// <param name="findOnRecordUpping">параметр проверки на "увеличение" рекорда или его "уменьшения" для засчитывания
        /// его побившим мировой</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<Results> GetWorldRecordUppers(int measureID, bool findOnRecordUpping)
        {
            if (ControlService.Results == null) throw new Exception("Результатов соревнований нет в базе!");
            if (ControlService.Measures == null) throw new Exception("Единиц измерений нет в базе!");

            List<Results> sportsmenResult = new List<Results>();
            foreach (var item in ControlService.Sportsmen)
            {
                List<Results> list;
                if (findOnRecordUpping)
                    list = ControlService.Results.Where(p => p.SportsmanID == item.SportsmanID
                    && p.Competitions.Sports.MeasureID == measureID
                    && p.Competitions.Sports.WorldRecord < p.Record).ToList();

                else list = ControlService.Results.Where(p => p.SportsmanID == item.SportsmanID
                    && p.Competitions.Sports.MeasureID == measureID
                    && p.Competitions.Sports.WorldRecord > p.Record).ToList();

                int gottenCount = list.Count();
                if (gottenCount > 0) sportsmenResult.Add(list.First());
            }
            return sportsmenResult;
        }
        /// <summary>
        /// Возвращает лучшие рекорды спортсмена(ов). Количество зависит от точности ввода фамилии спортсмена.
        /// Логика получения лучших результатов: Каждый результат проверяется по спортсмену, выбранной ед.измерения и в какую сторону ведется
        /// учет "превышения" рекорда.
        /// </summary>
        /// <param name="sportID">код спорта</param>
        /// <param name="measureID">код единицы измерения</param>
        /// <param name="secondName">фамилия спортсмена</param>
        /// <param name="findOnRecordUpping">параметр проверки на "увеличение" рекорда или его "уменьшения" для засчитывания
        /// его побившим мировой</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<Results> GetBestRecords(int sportID, int measureID, string secondName, bool findOnRecordUpping)
        {
            if (ControlService.Results == null) throw new Exception("Результатов соревнований нет в базе!");
            if (ControlService.Sports == null) throw new Exception("Видов спорта нет в базе!");
            if (ControlService.Measures == null) throw new Exception("Единиц измерений нет в базе!");

            if (secondName == null) secondName = "";

            List<Results> sportsmenResult = new List<Results>();
            foreach (var item in ControlService.Results)
            {
                Results result;
                if (findOnRecordUpping) result = ControlService.Results.Where(p => p.SportsmanID == item.SportsmanID
                && p.Competitions.SportID == sportID
                && p.Competitions.Sports.MeasureID == measureID
                && p.Sportsmen.SecondName.ToLower().Contains(secondName.ToLower())).MaxBy(p => p.Record);

                else result = ControlService.Results.Where(p => p.SportsmanID == item.SportsmanID
                && p.Competitions.SportID == sportID
                && p.Competitions.Sports.MeasureID == measureID
                && p.Sportsmen.SecondName.ToLower().Contains(secondName.ToLower())).MinBy(p => p.Record);
                if (result != null)
                    if (!sportsmenResult.Contains(result)) sportsmenResult.Add(result);
            }
            return sportsmenResult;
        }
    }
}
