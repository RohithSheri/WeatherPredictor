using DSWeatherPrediction.Implementations;
using DSWeatherPrediction.Interfaces;
using DSWeatherPrediction.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WeatherPredictionService.Implementation;
using WeatherPredictionService.Inteface;

namespace WeatherPredictor
{
    /// <summary>
    /// Basic console app to predict weather for a given day
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // config all the services
                using IHost host = CreateHostBuilder().Build();
                host.RunAsync();

                // Read and parse input from user
                Console.WriteLine("Please enter the month number and press enter");
                string month = Console.ReadLine();
                Console.WriteLine("Please enter the day number and press enter");
                string day = Console.ReadLine();
                var date = ValidateAndParseInput(month, day);

                using (var scope = host.Services.CreateScope())
                {
                    // Get the service by contract
                    var service = scope.ServiceProvider.GetRequiredService<IWeatherPrediction<Weather>>();
                    // Predict the weather for a given day
                    var weather = await service.Predict(date);
                    // output the weather data as JSON response
                    var jsonResponse = JsonConvert.SerializeObject(weather);
                    Console.WriteLine($"Weather JSON response for the day is:");
                    Console.WriteLine(jsonResponse);
                }
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured with message: {ex.Message}");
            }
            finally
            {
                Console.WriteLine();
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Provides the ability to configure the services and scope
        /// </summary>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder().ConfigureServices((_, services) =>
            {
                services.AddScoped<IDSWeatherPredictor<Weather>, DSWeatherPredictorService<Weather>>()
                        .AddScoped<IWeatherPrediction<Weather>, BasicWeatherPredictorService>()
                        .AddScoped<IReadDataService<Weather>, DSWeatherFileService>();
            });

        /// <summary>
        /// Provides the ability to validate and parse the given input to datetime
        /// </summary>
        /// <param name="monthString"></param>
        /// <param name="dayString"></param>
        /// <returns></returns>
        private static DateTime ValidateAndParseInput(string monthString, string dayString)
        {
            // TODO: Move this method to a utility class 

            // If no input was provided predict for the current day
            if (string.IsNullOrWhiteSpace(monthString) && string.IsNullOrWhiteSpace(dayString))
            {
                return DateTime.Now;
            }

            // Check if the month and day are valid inputs
            if (!int.TryParse(dayString, out int day))
            {
                throw new ArgumentException("Invalid day format");
            }

            if (!int.TryParse(monthString, out int month))
            {
                throw new ArgumentException("Invalid month format");
            }

            // Currently we don't care about the year but may change in the future
            if (!DateTime.TryParse($"{month}/{day}/1970", out DateTime date))
            {
                throw new ArgumentException("Invalid date provided");
            }

            // return the valid date
            return date;
        }
    }
}
