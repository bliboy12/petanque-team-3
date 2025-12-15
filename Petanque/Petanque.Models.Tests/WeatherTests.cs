using Moq;
using Petanque.Contracts.Responses;
using Petanque.Services.Api;
using Petanque.Services.Services;
using System.Globalization;
using static Petanque.Services.Services.WeatherService;

namespace Petanque.Models.Tests {
    public class WeatherTests {
        [Theory]
        [InlineData(-80)]
        [InlineData(56.5)]
        public void Test_Temperature_Valid(double temparture) {
            WeatherResponseContract w = new WeatherResponseContract();
            w.Temperature = temparture;        

            Assert.Equal(temparture, w.Temperature);
        }

        [Theory]
        [InlineData(1.8)]
        [InlineData(12.5)]
        public void Test_Precipitation_Valid(double precipitation) {
            WeatherResponseContract w = new WeatherResponseContract();
            w.Precipitation = precipitation;          

            Assert.Equal(precipitation, w.Precipitation);
        }

        [Theory]
        [InlineData("Ghent")]
        [InlineData("Lokeren")]
        public void Test_Location_Valid(string location) {
            WeatherResponseContract w = new WeatherResponseContract();
            w.Location = location;

            Assert.Equal(location, w.Location);
        }

        [Theory]
        [InlineData("21/12/2025")]
        [InlineData("16/03/2026")]
        public void Test_Date_Valid(string dateTime) {
            var datuminput = DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            WeatherResponseContract w = new WeatherResponseContract();
            w.Date = datuminput;

            Assert.Equal(datuminput, w.Date);
        }

        [Fact]
        public async Task GetWeatherForecastAsync_WithResponse_ReturnResult() {
            #region arrange
            var mockResponse = new OpenMeteoResponse
            {
                Hourly = new HourlyData
                {
                    Time = new List<string>() { "2025/12/01" },
                    Temperature2m = new List<double> { 10.0 },
                    Precipitation = new List<double> { 2.0 }
                }
            };

            var httpClientMock = new Mock<IWeatherApiClient>();
            httpClientMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>()))
                   .ReturnsAsync(mockResponse);


            var service = new WeatherService(httpClientMock.Object);
            var date = DateTime.Parse("2025-12-01");
            var latitude = 51.37;
            var longitude = 3.71;

            #endregion

            #region Act & Assert
            var result = await service.GetWeatherForecastAsync(date, latitude, longitude);

            Assert.NotNull(result);
            Assert.Equal(date, result.Date);
            Assert.Equal(10.0, result.Temperature);
            Assert.Equal(2.0, result.Precipitation);
            #endregion
        }

        [Fact]
        public async Task GetWeatherForecastAsync_WithInvalidParameters_ThrowsException() {
            #region arrange
            var mockEmptyResponse = new OpenMeteoResponse();

            var httpClientMock = new Mock<IWeatherApiClient>();
            httpClientMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>()))
                   .ReturnsAsync(mockEmptyResponse);

            var service = new WeatherService(httpClientMock.Object);
            var date = DateTime.Parse("2025-12-01");
            var latitude = 321.12;
            var longitude = 123.45;

            #endregion

            #region Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetWeatherForecastAsync(date, latitude, longitude));

            #endregion
        }
    }
}
