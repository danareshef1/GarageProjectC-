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

        public void ChangeVehicleStatus(string i_LicenseNumber, eCarStatus i_NewCarStatus)
        {
            //ToDo = check if no vehicle in the garage
            m_Vehicles[i_LicenseNumber].CarStatus = i_NewCarStatus;
        }

        public void InfaltionToMax(string i_LicenseNumber)
        {
            //ToDo = check if no vehicle in the garage
            foreach (Vehicle.Tire tire in m_Vehicles[i_LicenseNumber].Tires)
            {
                tire.Infaltion(tire.MaxTirePressue);
            }
        }


        public void FuelingVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_FuelAmount)
        {
            m_Vehicles[i_LicenseNumber].Engine.FillEngine(i_FuelAmount, i_FuelType);
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_MinutesToCharge)
        {
            m_Vehicles[i_LicenseNumber].Engine.FillEngine(i_MinutesToCharge, eFuelType.None);
        }

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
