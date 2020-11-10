using SafeAuto.Core.Entities;
using SafeAuto.Core.Enumerations;
using SafeAuto.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace SafeAuto.Core.Services
{
    public class DriverService : IDriverService
    {


        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            this._driverRepository = driverRepository;
        }

        public List<Driver> GetDriversSorted()
        {
            return _driverRepository.GetDriversSorted();
        }

        public void ProcessComand(string commandLine)
        {
            //Remove blank spaces
            commandLine = commandLine.Trim();

            string[] lineArr = commandLine.Split(' ');

            CommandType commandType;

            //Verify that command line has command
            if (!string.IsNullOrEmpty(commandLine))
            {
                commandType = this.GetCommandTypeOfLine(lineArr);
                switch (commandType)
                {
                    case CommandType.Driver: this.AddDriver(lineArr); break;
                    case CommandType.Trip: this.AddTrip(lineArr); break;
                }
            }
        }

        private void AddDriver(string[] parameters)
        {
            Driver driver = new Driver();
            driver.Name = this.GetVariable(parameters, VariableLine.DriverName);

            this._driverRepository.AddDriver(driver);
        }

        private void AddTrip(string[] parameters)
        {
            Trip trip = new Trip();

            string driverName = this.GetVariable(parameters, VariableLine.DriverName);

            trip.StartTime = TimeSpan.Parse(this.GetVariable(parameters, VariableLine.StartTrip));
            trip.StopTime = TimeSpan.Parse(this.GetVariable(parameters, VariableLine.StopTrip));
            trip.MilesDriven = Convert.ToDouble(this.GetVariable(parameters, VariableLine.DistanceTrip));

            //when we create the object, we calculate the speed
            double diffHour = (trip.StopTime - trip.StartTime).TotalHours;

            double speed = trip.MilesDriven / diffHour;

            trip.Speed = speed;

            this._driverRepository.AddTrip(driverName, trip);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineParameters">line in array</param>
        /// <returns>Comand type</returns>
        private CommandType GetCommandTypeOfLine(string[] lineParameters)
        {
            CommandType response = CommandType.None;

            string command = this.GetVariable(lineParameters, VariableLine.Command);

            if (command.Equals(CommandType.Driver.ToString()))
            {
                response = CommandType.Driver;

            }
            else if (command.Equals(CommandType.Trip.ToString()))
            {
                response = CommandType.Trip;
            }

            return response;

        }

        /// <summary>
        /// Returns the variable of the line
        /// </summary>
        /// <param name="lineParameters">line in array</param>
        /// <param name="variable">variable to return from the array</param>
        /// <returns></returns>
        private string GetVariable(string[] lineParameters, VariableLine variable)
        {
            return lineParameters[Convert.ToInt32(variable)];
        }
    }
}