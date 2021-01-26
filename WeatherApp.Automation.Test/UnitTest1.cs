using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace WeatherApp.Automation.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var location = "Kochi, Kerala";
            var applicationUrl = @"https://www.google.com/search?q=weather+"+location;

            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(applicationUrl);

            var tempartureText = driver.FindElementById(ElementIds.TEMPARTURE_CONTROL_ID).Text;
            var locationText = driver.FindElementById(ElementIds.LOCATION_CONTROL_ID).Text;

            Assert.Equal("28", tempartureText);
            Assert.Equal(location, locationText);
        }
    }

    public static class ElementIds
    {
        public const string TEMPARTURE_CONTROL_ID = "wob_tm";

        public const string LOCATION_CONTROL_ID = "wob_loc";
    }
}
