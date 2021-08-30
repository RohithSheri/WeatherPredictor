using DSWeatherPrediction.Interfaces;
using DSWeatherPrediction.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSWeatherPrediction.Implementations
{
    /// <summary>
    /// Provides the ability to read the weather data from a given file source
    /// </summary>
    public class DSWeatherFileService : IReadDataService<Weather>
    {
        /// <summary>
        /// File path to get the weather data
        /// </summary>
        private static readonly string _filePath = $"{AppContext.BaseDirectory}/DataResources/RDUWeatherDataDump.csv";

        /// <summary>
        /// Provides the ability to lazy load the weather data on demand
        /// </summary>
        private static readonly Lazy<Task<List<Weather>>> _weatherData = new Lazy<Task<List<Weather>>>(() =>
           Task.Run(() => File.ReadAllLines(_filePath).Skip(1).Select(s => Parse(s)).ToList())
        );

        /// <summary>
        /// Provides the ability to return all the weather data
        /// </summary>
        /// <returns></returns>
        public async Task<List<Weather>> GetAll()
        {
            return await _weatherData.Value;
        }

        /// <summary>
        /// Provides the ability to get a single record for a given filter action
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task<Weather> Get(Func<Weather, bool> predicate)
        {
            var result = await GetAll();
            return result.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Provides the ability to map the CSV data to weather object
        /// </summary>
        /// <param name="weatherData"></param>
        /// <returns></returns>
        private static Weather Parse(string weatherData)
        {
            var data = weatherData.Split(',');
            Weather weather = new Weather();
            weather.Month = DateTime.ParseExact(data[0], "MMMM", CultureInfo.CurrentCulture).Month;
            int.TryParse(data[1], out int day);
            weather.Day = day;
            int.TryParse(data[2], out int julianDay);
            weather.JulianDay = julianDay;
            double.TryParse(data[3], out double minTemp);
            weather.MinTemperature = minTemp;
            double.TryParse(data[4], out double maxTemp);
            weather.MaxTemperature = maxTemp;
            double.TryParse(data[5], out double precipitation);
            weather.Precipitation = precipitation;

            return weather;
        }
    }
}
