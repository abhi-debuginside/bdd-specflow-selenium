using RestSharp;
using System;
using System.Threading.Tasks;
using Weather.API.Models;

namespace Weather.API
{
    public class WeatherApiManager : BaseApiManager
    {
        private readonly RestClient _restClient;

        public WeatherApiResponseModel ApiResponse { get; private set; }
        public double Temperature => ApiResponse.Main.Temp;

        public WeatherApiManager()
        {
            _restClient = new RestClient(BaseApiUrl);
        }

        public async Task GetWeatherDataByCity(string city, string state, string unit = "metric")
        {
            //api.openweathermap.org/data/2.5/weather?q={city name},{state code},{country code}&appid={API key}            
            var request = new RestRequest("weather", Method.GET);
            request.AddParameter("appid", ApiKey);
            request.AddParameter("units", unit);
            request.AddParameter("q", $"{city},{state}");

            var response = await _restClient.ExecuteAsync<WeatherApiResponseModel>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Api call failed");
            }

            ApiResponse = response.Data;
        }
    }
    public abstract class BaseApiManager
    {
        protected string BaseApiUrl { get; set; }
        protected string ApiKey { get; set; }

        public BaseApiManager()
        {
            BaseApiUrl = "https://api.openweathermap.org/data/2.5/";
            ApiKey = "b103808f24f0c204af56ca5a788248ad";
        }
    }
}
