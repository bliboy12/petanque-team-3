using System;
using System.Collections.Generic;
using System.Text;

namespace Petanque.Models.Tests {
    public class WeatherTests {
        // Er werd geen model aangemaakt voor WeatherResponse
        [Theory]
        [InlineData(-80)]
        [InlineData(56,5)]
        public void Test_Temperature_Valid(double temparture) {
            WeatherResponseContract s = new WeatherResponseContract();
            s.Temperature = temparture;        

            Assert.Equal(temparture, s.Temperature);
        }

        [Theory]
        [InlineData("0")]
        public void Test_Temperature_Invalid(double temparture) {
            SeasonModel s = new SeasonModel();
            s.Temperature = temparture;

            Assert.Throws<Exception>(() => s.Temperature = temparture);
        }

        [Theory]
        [InlineData(1,8)]
        [InlineData(12,5)]
        public void Test_Precipitation_Valid(double precipitation) {
            WeatherResponseContract s = new WeatherResponseContract();
            s.Precipitation = precipitation;          

            Assert.Equal(precipitation, s.Precipitation);
        }

        [Theory]
        [InlineData("0")]
        public void Test_Precipitation_Invalid(double precipitation) {
            SeasonModel s = new SeasonModel();
            s.Precipitation = precipitation;

            Assert.Throws<Exception>(() => s.Id = precipitation);
        }

        [Theory]
        [InlineData("Ghent")]
        [InlineData("Lokeren")]
        public void Test_Location_Valid(string location) {
            WeatherResponseContract s = new WeatherResponseContract();
            s.Location = location;

            Assert.Equal(id, s.Location);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-10)]
        public void Test_Location_Invalid(string location) {
            SeasonModel s = new SeasonModel();
            s.Location = location;

            Assert.Throws<Exception>(() => s.Location = location);
        }

        [Theory]
        [InlineData(new DateTime(2025,10,10))]
        [InlineData(new DateTime(2026, 11, 11))]
        public void Test_Date_Valid(DateTime dateTime) {
            WeatherResponseContract s = new WeatherResponseContract();
            s.Date = dateTime;

            Assert.Equal(dateTime, s.Date);
        }

        [Theory]
        [InlineData("0")]
        [InlineData(-100)]
        [InlineData(-10)]
        public void Test_Date_Invalid(DateTime dateTime) {
            SeasonModel s = new SeasonModel();

            Assert.Throws<Exception>(() => s.Date = dateTime);
        }

        [Fact]
        public async Task GetWeatherForecastAsync_WithNullResponse_ReturnResult() {
            #region arrange
            var mockResponse = new OpenMeteoResponse
            {
                Hourly = new HourlyData
                {
                    Time = new List<DateTime> { new DateTime(2025, 12, 11) },
                    Temperature_2m = new List<double> { 10.0 },
                    Precipitation = new List<double> { 2.0 }
                }
            };

            var json = JsonSerializer.Serialize(mockResponse);
            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            var service = new WeatherService(httpClient);
            var date = DateTime.Parse("2025-12-11");
            var latitude = 51.37;
            var longitude = 3.71;

            #endregion

            #region Act & Assert
            var result = await service.GetWeatherForecastAsync(date, latitude, longitude);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Time);
            Assert.Equal(date, result.Time);
            Assert.Equal(10.0, result.Temperature[0]);
            Assert.Equal(2.0, result.Precipitation[0]);
            #endregion
        }

        [Fact]
        public async Task GetWeatherForecastAsync_WithInvalidParameters_ThrowsException() {
            #region arrange
            var mockResponse = new OpenMeteoResponse
            {
                Hourly = new HourlyData
                {
                    Time = new List<DateTime> { new DateTime(2025,12,11) },
                    Temperature_2m = new List<double> { 10.0 },
                    Precipitation = new List<double> { 2.0 }
                }
            };

            var json = JsonSerializer.Serialize(mockResponse);
            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            var service = new WeatherService(httpClient);
            var date = DateTime.Parse("2025-12-11");
            var latitude = 321.12;
            var longitude = 123.45;

            #endregion

            #region Act & Assert
            var result = await service.GetWeatherForecastAsync(date, latitude, longitude);

            // Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetWeatherForecastAsync(date, latitude, longitude));

            #endregion
        }
    }
}
