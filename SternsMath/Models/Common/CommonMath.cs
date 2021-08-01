using System;
using SternsMath.Interfaces.Common;

namespace SternsMath.Models.Common
{
    public class CommonMath : ICommonMath
    {
        public void CheckNumType<T>(T num) where T : struct
        {
            if (typeof(T) != typeof(Int16) &&
                typeof(T) != typeof(Int32) &&
                typeof(T) != typeof(Int64) &&
                typeof(T) != typeof(UInt16) &&
                typeof(T) != typeof(UInt32) &&
                typeof(T) != typeof(UInt64))
            {
                throw new ArgumentException($@"Type '{typeof(T)}' is not valid.");
            }
        }
    }
}