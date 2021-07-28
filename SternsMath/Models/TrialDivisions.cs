using System.Collections.Generic;

namespace SternsMath.Models
{
    public static class TrialDivisions
    {
        public static List<ulong> GetTrialDivision(this ulong n)
        {
            var divides = new List<ulong>();
            ulong div = 2;
            while (n > 1)
            {
                if (n % div == 0)
                {
                    divides.Add(div);
                    n /= div;
                }
                else
                {
                    div++;
                }
            }

            return divides;
        }
    }
}