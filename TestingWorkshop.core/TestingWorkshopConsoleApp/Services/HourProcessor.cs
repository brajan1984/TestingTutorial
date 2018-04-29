
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestingWorkshop.Models;

namespace TestingWorkshop.Services
{
    public class HourProcessor : IHoursProcessor
    {
        public string Process(List<Hour24Model> hours)
        {
            HttpClient client = new HttpClient();
            var jsonObject = JsonConvert.SerializeObject(hours.Select(h => h.ToString()).ToArray());
            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var response = client.PostAsync("http://localhost:5000/api/values/getmin", content).Result;

            Stream receiveStream = response.Content.ReadAsStreamAsync().Result;
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            return readStream.ReadToEnd();
        }
    }
}
