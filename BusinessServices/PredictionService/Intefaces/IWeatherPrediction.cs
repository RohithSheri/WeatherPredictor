using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPredictionService.Inteface
{
    /// <summary>
    /// Provides the ability to predict weather
    /// </summary>
    /// <typeparam name="T">
    /// Here T is any object reference type
    /// </typeparam>
    public interface IWeatherPrediction<T> where T: class
    {
        /// <summary>
        /// Provides the ability to predict the weather for current day
        /// </summary>
        /// <returns></returns>
        Task<T> Predict();

        /// <summary>
        /// Provides the ability to predict the weather for a given julian day
        /// </summary>
        /// <param name="julianDay"></param>
        /// <returns></returns>
        Task<T> Predict(int julianDay);

        /// <summary>
        /// Provides the ability to predict the weather for a give date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<T> Predict(DateTime date);
    }
}
