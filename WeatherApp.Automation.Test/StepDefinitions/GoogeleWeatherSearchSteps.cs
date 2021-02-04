using FluentAssertions;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Weather.API;

namespace WeatherApp.Automation.Test.StepDefinitions
{
    [Binding]
    public class GoogeleWeatherSearchSteps
    {
        private readonly ChromeDriver _chromeDriver;
        private readonly WeatherApiManager _weatherApiManager;

        private string city = string.Empty;
        private string state = string.Empty;
        private string unit = string.Empty;
        private string location => $"{city}, {state}";
        public GoogeleWeatherSearchSteps()
        {
            _chromeDriver = new ChromeDriver();
            _weatherApiManager = new WeatherApiManager();
        }

        [Given(@"city is '(.*)'")]
        public void GivenCityIs(string p0)
        {
            city = p0;
        }
        
        [Given(@"state is '(.*)'")]
        public void GivenStateIs(string p0)
        {
            state = p0;
        }
        
        [Given(@"unit is '(.*)'")]
        public void GivenUnitIs(string p0)
        {
            unit = p0;
        }
        
        [When(@"Search google with city, state and unit")]
        public async Task WhenSearchGoogleWithCityStateAndUnit()
        {
            var applicationUrl = string.Format(Constants.GoogleWeatherConstants.SearchUrl, location.ToLower());
            _chromeDriver.Navigate().GoToUrl(applicationUrl);

            // calling weather api
            await _weatherApiManager.GetWeatherDataByCity(city, state, unit);
        }
        
        [Then(@"Teamperature displayed should match value from open weather api")]
        public void ThenTeamperatureDisplayedShouldMatchValueFromOpenWeatherApi()
        {
            // extract temp from UI
            var actualTempartureText = _chromeDriver.FindElementById(ElementIds.TEMPARTURE_CONTROL_ID).Text;
            var actualLocation = _chromeDriver.FindElementById(ElementIds.LOCATION_CONTROL_ID).Text;
            double.TryParse(actualTempartureText, out double actualTemparture);

            _chromeDriver.Close();
            
            // Assert
            _weatherApiManager.Temperature.Should().BeApproximately(actualTemparture, 2.0);
            location.Should().Be(actualLocation);
        }
    }
}
