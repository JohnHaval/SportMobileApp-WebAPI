using Newtonsoft.Json;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SportMobileApp
{
    /// <summary>
    /// Класс, производящий работу с API. 
    /// Позволяет получать, обновлять, удалять данные относительно имеющихся сущностей.
    /// Также, включает в себя коллекции, хранящие данные из таблиц на сервере.
    /// </summary>
    public static class ControlService
    {        
        public static List<Activities> Activities;
        public static List<Competitions> Competitions;
        public static List<Measures> Measures;
        public static List<Results> Results;
        public static List<SportCategories> SportCategories;
        public static List<Sports> Sports;
        public static List<Sportsmen> Sportsmen;
        public static List<Teams> Teams;

        const string Url = "http://192.168.0.15:5000/api";    
        
        static ControlService()
        {
            try
            {
                LoadTables();
            }
            catch { }
        }
       

        public static List<T> GetItems<T>(string tableName)
        {
            var client = new WebClient();

            var response = client.DownloadString(Url + "/" + tableName);

            return JsonConvert.DeserializeObject<List<T>>(response);
        }


        public static void LoadTables()
        {
            Activities = GetItems<Activities>("Activities");
            Competitions = GetItems<Competitions>("Competitions");
            Measures = GetItems<Measures>("Measures");
            Results = GetItems<Results>("Results");
            SportCategories = GetItems<SportCategories>("SportCategories");
            Sports = GetItems<Sports>("Sports");
            Sportsmen = GetItems<Sportsmen>("Sportsmen");
            Teams = GetItems<Teams>("Teams");
        }




        public static string Post<T>(T editObject, string tableName)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add(HttpRequestHeader.ContentType, "application/Json");
            string data = JsonConvert.SerializeObject(editObject);
            return client.UploadString(Url + "/" + tableName, "POST", data);
        }
        public static string Put<T>(T editObject, int id, string tableName)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add(HttpRequestHeader.ContentType, "application/Json");
            string data = JsonConvert.SerializeObject(editObject);
            return client.UploadString(Url + "/" + tableName + "/" + id, "PUT", data);
        }
        public static string Delete<T>(T editObject, int id, string tableName)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add(HttpRequestHeader.ContentType, "application/Json");
            string data = JsonConvert.SerializeObject(editObject);
            return client.UploadString(Url + "/" + tableName + "/" + id, "DELETE", data);
        }
    }
}