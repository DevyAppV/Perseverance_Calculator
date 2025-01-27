using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perseverance_Calculator_1.Controller
{
    internal partial class MathVue<T>
    {
        private string factorial(string value)
        {
            string returnVal = "";
            int indexofDecimal = value.IndexOf(".");
            if (typeof(T).Equals(typeof(decimal)))
            {
                decimal result = 1;
                decimal valueDecimal = Math.Floor(decimal.Parse(value));

                for (decimal i = valueDecimal;i>0;i--) {
                    result *= i;
                }
                returnVal = result.ToString();
            }
            else if (typeof(T).Equals(typeof(double)))
            {

                double result = 1;
                double valueDecimal = Math.Floor(double.Parse(value));

                for (double i = valueDecimal; i > 0; i--)
                {
                    result *= i;
                }
                returnVal = result.ToString();
            }
            return returnVal;
        }
    }
}
