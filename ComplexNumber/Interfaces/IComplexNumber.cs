namespace ComplexNumber.Interfaces
{
    public interface IComplexNumber
    {
        /// <summary>
        /// Действительная часть
        /// </summary>
        public double Real { get; set; }

        /// <summary>
        /// Мнимая часть
        /// </summary>
        public double Imaginary { get; set; }
    }
}