using System;
using System.Threading.Tasks;
using WeatherPredictionService.Inteface;
using DSWeatherPrediction.Interfaces;
using DSWeatherPrediction.Models;

namespace WeatherPredictionService.Implementation
{
    /// <summary>
    /// Provides the ability to do basic weather prediction
    /// </summary>
    public class BasicWeatherPredictorService : IWeatherPrediction<Weather>
    {
        /// <summary>
        /// Property definition to access the weather data
        /// </summary>
        private readonly IDSWeatherPredictor<Weather> _dsWeatherPredictorService;

        public BasicWeatherPredictorService(IDSWeatherPredictor<Weather> dSWeatherPredictor) => _dsWeatherPredictorService = dSWeatherPredictor;

        /// <summary>
        /// Provides the ability to predict the weather for the current day
        /// </summary>
        /// <returns></returns>
        public Task<Weather> Predict()
        {
            return _dsWeatherPredictorService.GetWeather(DateTime.Now);
        }

        /// <summary>
        /// Provides the ability to predict the weather for a given julian day
        /// </summary>
        /// <param name="julianDay"></param>
        /// <returns></returns>
        public Task<Weather> Predict(int julianDay)
        {
            if (julianDay < 1 || julianDay > 365)
            {
                throw new ArgumentOutOfRangeException("Invalid julian day range");
            }
            return _dsWeatherPredictorService.GetWeather(julianDay);
        }

        /// <summary>
        /// Provides the ability to predict the weather for a given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Task<Weather> Predict(DateTime date)
        {
            return _dsWeatherPredictorService.GetWeather(date);
        }
    }
}
