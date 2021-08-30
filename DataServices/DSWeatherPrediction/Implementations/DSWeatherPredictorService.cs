using DSWeatherPrediction.Interfaces;
using DSWeatherPrediction.Models;
using System;
using System.Threading.Tasks;

namespace DSWeatherPrediction.Implementations
{
    /// <summary>
    /// Provides the ability to access the weather data
    /// </summary>
    public class DSWeatherPredictorService<T> : IDSWeatherPredictor<T> where T: Weather
    {
        /// <summary>
        /// IReadService to read the data
        /// </summary>
        private readonly IReadDataService<T> _readDataService;
        public DSWeatherPredictorService(IReadDataService<T> readDataService)
        {
            _readDataService = readDataService;
        }

        /// <summary>
        /// Provides the ability to get weather for given julian day
        /// </summary>
        /// <param name="julianDay"></param>
        /// <returns></returns>
        public async Task<T> GetWeather(int julianDay)
        {
            return await _readDataService.Get(s => s.JulianDay == julianDay);
        }

        /// <summary>
        /// Provides the ability to get weather for a given date
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public async Task<T> GetWeather(DateTime dateTime)
        {
            return await _readDataService.Get(s => s.Day == dateTime.Day && s.Month == dateTime.Month);
        }
    }
}
