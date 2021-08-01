namespace SternsMath.Interfaces.Common
{
    public interface ICommonMath
    {
        void CheckNumType<T>(T num) where T : struct;
    }
}