namespace Ex03.GarageLogic
{
    public class Tire
    {
        private string m_ManufacturerName;
        private float m_TirePressure;
        private float m_MaxTirePressure;

        public Tire(string i_ManufacturerName, float i_TirePressure, float i_MaxTirePressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_TirePressure = i_TirePressure;
            m_MaxTirePressure = i_MaxTirePressure;
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
            set
            {
                m_ManufacturerName = value;
            }
        }

        public float TirePressure
        {
            get
            {
                return m_TirePressure;
            }
            set
            {
                m_TirePressure = value;
            }
        }

        public float MaxTirePressure
        {
            get
            {
                return m_MaxTirePressure;
            }
            set
            {
                m_MaxTirePressure = value;
            }
        }

        public void Inflation(float i_HowManyAirToAdd)
        {
            if (m_TirePressure + i_HowManyAirToAdd <= m_MaxTirePressure)
            {
                m_TirePressure += i_HowManyAirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(m_MaxTirePressure, 0);
            }
        }
    }
}