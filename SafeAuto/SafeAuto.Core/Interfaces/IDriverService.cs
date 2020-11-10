using SafeAuto.Core.Entities;
using System.Collections.Generic;

namespace SafeAuto.Core.Interfaces
{
    public interface IDriverService
    {

        /// <summary>
        /// This method process a line of comand
        /// </summary>
        /// <param name="commandLine">Example:
        /// Driver Dan 
        /// Trip Dan 07:15 07:45 17.3</param>
        void ProcessComand(string commandLine);

        /// <summary>
        /// Returns a List of driver sorted by most miles driven to least.
        /// </summary>
        /// <returns></returns>
        List<Driver> GetDriversSorted();
    }
}