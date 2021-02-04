using RestSharp;
using System;
using System.Linq;
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

        public async Task GetWeatherDataByCity(string city, string state, string unit = "metric", string country="India")
        {
            //api.openweathermap.org/data/2.5/weather?q={city name},{state code},{country code}&appid={API key}     
            var location = Constants.Locations.FirstOrDefault(loc => 
                    city.Equals(loc.City, StringComparison.OrdinalIgnoreCase)
                    && state.Equals(loc.State, StringComparison.OrdinalIgnoreCase)
                    && country.Equals(loc.Country, StringComparison.OrdinalIgnoreCase)
                    );

            var cityId = location.CityId;

            var request = new RestRequest("weather", Method.GET);
            request.AddParameter("appid", ApiKey);
            request.AddParameter("units", unit);
            request.AddParameter("id", cityId);

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
