using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleDataInGarage> m_Vehicles = new Dictionary<string, VehicleDataInGarage>();

        public Dictionary<string, VehicleDataInGarage> Vehicles { get { return m_Vehicles; } }

        public Dictionary<string, VehicleDataInGarage> GetLicenseNumberListByStatus(eVehicleStatus i_StatusToPresentBy)
        {
            Dictionary<string, VehicleDataInGarage> vehiclesByStatus = new Dictionary<string, VehicleDataInGarage>();

            foreach (KeyValuePair<string, VehicleDataInGarage> vehicle in m_Vehicles)
            {
                if (vehicle.Value.VehicleStatus == i_StatusToPresentBy)
                {
                    vehiclesByStatus[vehicle.Key] = vehicle.Value;
                }
            }
            return vehiclesByStatus;
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewCarStatus)
        {

            if (CheckIfTheVehicleIsInGarage(i_LicenseNumber))
            {
                m_Vehicles[i_LicenseNumber].VehicleStatus = i_NewCarStatus;
            }
            else
            {
                throw new ArgumentException("There is no such vehicle in the garage");
            }
        }

        public void InfaltionToMax(string i_LicenseNumber)
        {

            if (CheckIfTheVehicleIsInGarage(i_LicenseNumber))
            {
                foreach (Vehicle.Tire tire in m_Vehicles[i_LicenseNumber].Vehicle.Tires)
                {
                    tire.Infaltion(tire.MaxTirePressure);
                }
            }
            else
            {
                throw new ArgumentException("There is no such vehicle in the garage");
            }
        }


        public void FuelingVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_FuelAmount)
        {
            if (CheckIfTheVehicleIsInGarage(i_LicenseNumber))
            {
                m_Vehicles[i_LicenseNumber].Vehicle.Engine.FillEngine(i_FuelAmount, i_FuelType);
            }
            else
            {
                throw new ArgumentException("There is no such vehicle in the garage");
            }
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_HoursToCharge)
        {
            if (CheckIfTheVehicleIsInGarage(i_LicenseNumber))
            {
                m_Vehicles[i_LicenseNumber].Vehicle.Engine.FillEngine(i_HoursToCharge, eFuelType.None);
            }
            else
            {
                throw new ArgumentException("There is no such vehicle in the garage");
            }
        }

        public bool CheckIfTheVehicleIsInGarage(string i_LicenseNumber)
        {
            return (m_Vehicles.ContainsKey(i_LicenseNumber));
        }

        public void AddVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_Vehicles[i_Vehicle.LicenseNumber] = new VehicleDataInGarage(i_Vehicle, i_OwnerName, i_OwnerPhoneNumber);
        }

        public void ChangeVehicleStatusToInRepair(string i_LicenseNumber)
        {
            m_Vehicles[i_LicenseNumber].VehicleStatus = eVehicleStatus.InRepair;
        }

    }

}
