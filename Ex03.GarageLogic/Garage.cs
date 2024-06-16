using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, Vehicle> m_Vehicles = new Dictionary<string, Vehicle>();

        public Dictionary<string, Vehicle> Vehicles {  get { return m_Vehicles; } }

        public Dictionary<string, Vehicle> GetLicenseNumberList(eCarStatus i_StatusToPresentBy)
        {
            Dictionary<string, Vehicle> vehiclesByStatus = new Dictionary<string, Vehicle>();
        
            foreach(KeyValuePair<string, Vehicle> vehicle in m_Vehicles)
            {
                if(vehicle.Value.CarStatus == i_StatusToPresentBy)
                    vehiclesByStatus[vehicle.Key] = vehicle.Value;
            }

            return vehiclesByStatus;
        }

        public void ChangeVehicleStatus()
        public void AddVehicle(Vehicle i_Vehicle)
        {
            if(m_Vehicles.ContainsKey(i_Vehicle.LicenseNumber))
            {
                throw new ArgumentException("Vehicle found in the garage, We will use the existed car.");
            }
            else
            {
                m_Vehicles[i_Vehicle.LicenseNumber] = i_Vehicle;
            }
            m_Vehicles[i_Vehicle.LicenseNumber].CarStatus = eCarStatus.InRepair;
        }
    }

}
