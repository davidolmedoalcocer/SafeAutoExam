using SafeAuto.Core.Entities;
using SafeAuto.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SafeAuto.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        //Hashtable to save Driver name and its index in the List<Driver>
        //Used to avoid the driver
        private Hashtable _listIds;

        //Id int the List<Driver>
        private int _idCounter;

        //Keep drivers in memory
        private List<Driver> _drivers;

        public DriverRepository()
        {
            this._drivers = new List<Driver>();
            this._idCounter = 0;
            this._listIds = new Hashtable();
        }
        
        public void AddDriver(Driver driver)
        {
            this._listIds.Add(driver.Name, this._idCounter);

            this._drivers.Add(driver);

            //Increment the id
            this._idCounter++;
        }

        public void AddTrip(string driverName, Trip trip)
        {

            int listIndex = Convert.ToInt32(this._listIds[driverName]);

            //If driver doesn't have trip already, we create trip object
            if (this._drivers[listIndex].Trips == null)
            {
                this._drivers[listIndex].Trips = new List<Trip>();
            }

            this._drivers[listIndex].Trips.Add(trip);

            //Calculate the total distance of the trips
            this._drivers[listIndex].Distance = this._drivers[listIndex].Trips.Sum(x => x.MilesDriven);

            //Calculate the speed average of the trip of the trips
            this._drivers[listIndex].SpeedAverage = this._drivers[listIndex].Trips.Average(x => x.Speed);

        }

        public List<Driver> GetDriversSorted()
        {
            return this._drivers.OrderByDescending(x => x.Distance).ToList();
        }

        public void CleanDriverList()
        {
            this._drivers.Clear();
            this._idCounter = 0;
            this._listIds.Clear();

        }

       
    }
}