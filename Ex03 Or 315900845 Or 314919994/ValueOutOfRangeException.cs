using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03_Or_315900845_Or_314919994
{
    internal class ValueOutOfRangeException : Exception
    {
        public float m_MaxValue { get; private set; }
        public float m_MinValue { get; private set; }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) : base($"The value is not in the range {i_MinValue} to {i_MaxValue}.")
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        //public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_Message, Exception i_InnerException)
        //    : base(i_Message, i_InnerException)
        //{
        //    m_MinValue = i_MinValue;
        //    m_MaxValue = i_MaxValue;
        //}

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_Message)
            : base(i_Message)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }
    }
}
