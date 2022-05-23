using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Business.Functions
{
    public class IsNumberOrNot
    {
        public bool Validate(string number)
        {
            bool entero = true ;

            char[] test = number.ToCharArray();

            for (int i = 0; i < test.Length; i++)
            {
                if (test[i] == '.')
                {
                    entero = false;
                }
            }

            try
            {
                if (entero)
                {
                    Convert.ToInt32(number);
                    entero = true;
                }
                else
                {
                    Convert.ToDouble(number);
                    entero = false;
                }
            }
            catch (FormatException)
            {
                entero = false;
            }

            return false;
        }
    }
}
