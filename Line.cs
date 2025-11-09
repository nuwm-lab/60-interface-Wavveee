using System;
using System.Linq;

namespace Geometry
{
    public class Line : GeometricEquation, ILoggable
    {
        public Line(double a, double b, double c) : base(a, b, c)
        {
        }

        protected override void ValidateCoefficients()
        {
            // Чітке повідомлення про помилку
            if (Dimension != 2)
                throw new ArgumentException($"Line requires exactly 3 coefficients [A, B, C] for 2D space, but received {Coefficients.Count}.");

            if (Math.Abs(Coefficients[0]) < Epsilon && Math.Abs(Coefficients[1]) < Epsilon)
                throw new ArgumentException("For a 2D line, coefficients A and B (for x and y) cannot both be zero.");
        }

        public override bool BelongsToShape(params double[] coords)
        {
            return Math.Abs(Evaluate(coords)) < Epsilon;
        }

        // Перевизначаємо ToString для використання спеціальних імен змінних (x, y)
        public override string ToString()
        {
            return FormatEquation(new[] { "x", "y" });
        }

        // Реалізація нового абстрактного методу
        public override GeometricEquation Normalize()
        {
            // Норма нормального вектора [A, B]
            double normSq = Coefficients[0] * Coefficients[0] + Coefficients[1] * Coefficients[1];
            double norm = Math.Sqrt(normSq);

            if (Math.Abs(norm) < Epsilon)
                return this; // Має бути відловлено ValidateCoefficients

            double[] normalizedCoeffs = Coefficients.Select(c => c / norm).ToArray();
            
            return new Line(normalizedCoeffs[0], normalizedCoeffs[1], normalizedCoeffs[2]);
        }
        
        // Реалізація інтерфейсу ILoggable
        public string GetLogDescription()
        {
            return $"2D Line: {ToString()}";
        }
    }
}