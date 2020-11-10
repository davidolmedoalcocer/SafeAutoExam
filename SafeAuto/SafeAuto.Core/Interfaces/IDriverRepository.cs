using SafeAuto.Core.Entities;
using System.Collections.Generic;

namespace SafeAuto.Core.Interfaces
{
    public interface IDriverRepository
    {
        void AddDriver(Driver driver);
        void AddTrip(string driverName, Trip trip);
        List<Driver> GetDriversSorted();

        void CleanDriverList();
    }
}
