using System;
using System.Collections.Generic;
using System.Text;

namespace DSWeatherPrediction.Models
{
    /// <summary>
    /// Base class that represents the weather
    /// </summary>
    public class Weather
    {
        public Weather()
        {

        }
        public Weather(int month, int day, int julianDay, double minTemperature, double maxTemperature, double precipitation)
        {
            Month = month;
            Day = day;
            JulianDay = julianDay;
            MinTemperature = minTemperature;
            MaxTemperature = maxTemperature;
            Precipitation = precipitation;
        }

        public int JulianDay { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public double Precipitation { get; set; }
    }
}
