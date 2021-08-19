using System;
using System.Collections.Generic;

namespace SternsMath.Models.NumberSystems
{
    public static class NumSystem
    {
        private const string _chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string ToBase(this int num, int b)
        {
            if (b < 2 || b > _chars.Length)
                throw new Exception("Недопустимая система счисления");

            List<int> nums = new List<int>();

            while (num > 0)
            {
                var ost = num % b;
                nums.Add(ost);
                num /= b;
            }

            nums.Reverse();

            string res = string.Empty;

            foreach (var i in nums)
            {
                res += _chars[i];
            }

            return res;
        }
    }
}