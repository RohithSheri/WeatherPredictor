using DSWeatherPrediction.Models;
using System;
using System.Threading.Tasks;

namespace DSWeatherPrediction.Interfaces
{
    /// <summary>
    /// Provides the definition to get the weather data
    /// </summary>
    /// <typeparam name="T">
    /// Here T is any object reference type
    /// </typeparam>
    public interface IDSWeatherPredictor<T> where T: Weather
    {
        /// <summary>
        /// Provides the ability to get the weather data for a given julian day
        /// </summary>
        /// <param name="julianDay">
        /// Pass the julian day
        /// </param>
        /// <returns></returns>
        Task<T> GetWeather(int julianDay);

        /// <summary>
        /// Provides the ability to get the weather for a given date
        /// </summary>
        /// <param name="dateTime">
        /// Pass the date time
        /// </param>
        /// <returns></returns>
        Task<T> GetWeather(DateTime dateTime);
    }
}
