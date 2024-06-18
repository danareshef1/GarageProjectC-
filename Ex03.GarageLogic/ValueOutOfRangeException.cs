using System;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;
        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
            : base(string.Format("The value needs to be in the range {0} to {1}", i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }

        float MinValue
        {
            get
            {
                return m_MinValue;
            }
        }
    }
}
