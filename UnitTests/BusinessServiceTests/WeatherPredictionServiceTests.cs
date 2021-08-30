using DSWeatherPrediction.Implementations;
using DSWeatherPrediction.Interfaces;
using DSWeatherPrediction.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherPredictionService.Implementation;

namespace BusinessServiceTests
{
    [TestFixture]
    public class WeatherPredictionServiceTests
    {
        private Mock<IReadDataService<Weather>> _readServiceMock;

        private DSWeatherPredictorService<Weather> _dsWeatherPredictorService;
        private BasicWeatherPredictorService _basicWeatherPredictorService;

        [TearDown]
        public void Teardown()
        {
            _readServiceMock.Reset();
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _readServiceMock = new Mock<IReadDataService<Weather>>();

            // Setup _readServiceMock Behavior
            // TODO: To be used for full fledged testing
            _readServiceMock.Setup(f => f.GetAll()).Returns(
            Task.FromResult(new List<Weather>
            {
                new Weather(1,5,5,50.4,30.7,0.11),
                new Weather(2,23,54,57.1,35.1,0.12),
                new Weather(5,29,150,82.8,60.6,0.12),
                new Weather(5,23,144,81.2,58.6,0.12),
                new Weather(7,9,191,90.4,69.9,0.14)
            }));

            _dsWeatherPredictorService = new DSWeatherPredictorService<Weather>(_readServiceMock.Object);
            _basicWeatherPredictorService = new BasicWeatherPredictorService(_dsWeatherPredictorService);
        }

        [Test]
        public async Task Should_ReturnCorrectWeather_When_GivenADate()
        {
            // Set up the behavior for the readservice dependency
            var expected = new Weather(5, 23, 144, 81.2, 58.6, 0.12);
            _readServiceMock.Setup(f => f.Get(It.IsAny<Func<Weather, bool>>()))
                            .Returns(Task.FromResult(expected));

            // call the predict method and get the result
            var dateTime = new DateTime(1990, 5, 23);
            var result = await _basicWeatherPredictorService.Predict(dateTime);

            // compare the results and it should be the same
            Assert.True(IsEquals(expected, result));
        }

        [Test]
        public async Task Should_ReturnCorrectWeather_When_GivenCorrectJulianDay()
        {
            // Set up the behavior for the readservice dependency
            var julianDay = 191;
            var expected = new Weather(7, 9, julianDay, 90.4, 69.9, 0.14);
            _readServiceMock.Setup(f => f.Get(It.IsAny<Func<Weather, bool>>()))
                            .Returns(Task.FromResult(expected));

            // call the predict method for a given julian day and get the result
            var result = await _basicWeatherPredictorService.Predict(julianDay);

            // compare the results and it should be the same
            Assert.True(IsEquals(expected, result));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        [TestCase(366)]
        [TestCase(450)]

        public void Should_ThrowException_When_GivenWrongJulianDay(int julianDay)
        {
            // Set up the behavior for the readservice dependency
            var expected = new Weather(7, 9, julianDay, 90.4, 69.9, 0.14);
            _readServiceMock.Setup(f => f.Get(It.IsAny<Func<Weather, bool>>()))
                            .Returns(Task.FromResult(expected));

            // call the predict method for a given julian day and expected an exception to be thrown
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async() => await _basicWeatherPredictorService.Predict(julianDay));
        }


        private bool IsEquals(Weather source, Weather result)
        {
            // TODO: Use reflection to make it generic
            return source != null &&
                   result != null &&
                   source.Day == result.Day &&
                   source.Month == result.Month &&
                   source.MinTemperature == result.MinTemperature &&
                   source.MaxTemperature == result.MaxTemperature &&
                   source.JulianDay == result.JulianDay &&
                   source.Precipitation == result.Precipitation;
        }
    }
}