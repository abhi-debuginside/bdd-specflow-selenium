using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using Weather.API;
using Xunit;
using FluentAssertions;

namespace WeatherApp.Automation.Test
{
    public class UnitTest1
    {
        //[Fact]
        public async Task Test1()
        {
            // arrange
            var city = "Kochi";
            var state = "Kerala";
            var location = $"{city}, {state}".ToLower();
            double actualTemparture = 0;

            var applicationUrl = @"https://www.google.com/search?q=weather+" + location;
            var apiManager = new WeatherApiManager();

            // act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(applicationUrl);

            var actualTempartureText = driver.FindElementById(ElementIds.TEMPARTURE_CONTROL_ID).Text;
            var actualLocation = driver.FindElementById(ElementIds.LOCATION_CONTROL_ID).Text;
           
            double.TryParse(actualTempartureText, out actualTemparture);
          
            await apiManager.GetWeatherDataByCity(city, state);
            var apiData = apiManager.ApiResponse;


            // assert

            // there is difference in weather reading in google api and open weather api so checking the apporximate value with 2.0 precision.
            apiData.Main.Temp.Should().BeApproximately(actualTemparture, 2.0);
            location.Should().Be(actualLocation);
        }
    }
}
