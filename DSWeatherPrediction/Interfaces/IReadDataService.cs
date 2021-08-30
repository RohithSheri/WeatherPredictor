using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSWeatherPrediction.Interfaces
{
    /// <summary>
    /// Provides the definition to read data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadDataService<T> where T:class
    {
        /// <summary>
        /// Provides the ability to read all the data
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Provides the ability to read a single record for a given filter function
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        Task<T> Get(Func<T, bool> predicate);
    }
}
