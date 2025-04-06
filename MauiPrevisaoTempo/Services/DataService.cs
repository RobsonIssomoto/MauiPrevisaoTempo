using MauiPrevisaoTempo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MauiPrevisaoTempo.Services
{
    public class DataService
    {

        public static async Task<Tempo?> getPrevisao(string cidade)
        {
            Tempo? t = null;

            string chave = "012945555a312ca3cd8deafbcbf38bc0";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        sunrise = sunrise.ToString(),
                        sunset = sunset.ToString(),
                        temp_max = (double)rascunho["main"]["temp_max"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        description = (string)rascunho["weather"][0]["description"],
                        //main = (string)rascunho["weather"][0]["main"],                   
                        speed = (double)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],
                        codigo = (int)resp.StatusCode,
                    };
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    t = new()
                    {
                        codigo = (int)resp.StatusCode
                    };
                }

            }

            return t;
        }
    }
}
