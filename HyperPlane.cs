using System;
using System.Linq;

namespace Geometry
{
    public class HyperPlane : GeometricEquation, ILoggable
    {
        public HyperPlane(params double[] coefficients) : base(coefficients)
        {
        }
        
        protected override void ValidateCoefficients()
        {
            // Мінімум 1D (2 коефіцієнти)
            if (Dimension < 1)
                throw new ArgumentException("HyperPlane requires at least 1 dimension (min 2 coefficients: A1, C).");
        }

        public override bool BelongsToShape(params double[] coords)
        {
            return Math.Abs(Evaluate(coords)) < Epsilon;
        }

        /// <summary>
        /// Перевизначення ToString() для використання загального формату x1...xN, 
        /// успадкованого від GeometricEquation.
        /// </summary>
        // public override string ToString() => base.ToString(); // Можна не перевизначати, якщо використовуємо x1, x2, ...

        // Реалізація нового абстрактного методу
        public override GeometricEquation Normalize()
        {
            // Норма гіперплощини (вектор [A1, ..., An])
            double normSq = Coefficients.Take(Dimension).Sum(c => c * c);
            double norm = Math.Sqrt(normSq);

            // Якщо норма нульова, це означає, що всі провідні коефіцієнти нульові (відловлено ValidateCoefficients)
            if (Math.Abs(norm) < Epsilon)
                return this; 

            double[] normalizedCoeffs = Coefficients.Select(c => c / norm).ToArray();
            
            // Повертаємо новий екземпляр з нормалізованими коефіцієнтами
            return new HyperPlane(normalizedCoeffs);
        }

        // Реалізація інтерфейсу ILoggable
        public string GetLogDescription()
        {
            return $"N-dimensional HyperPlane (N={Dimension}) defined by: {ToString()}";
        }
    }
}
