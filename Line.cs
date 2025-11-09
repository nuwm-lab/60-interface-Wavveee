using System;
using System.Linq;

namespace Geometry
{
    public class Line : GeometricEquation
    {
        /// <summary>
        /// Ініціалізує 2D пряму: Ax + By + C = 0.
        /// Порядок коефіцієнтів: [A, B, C].
        /// </summary>
        /// <param name="a">Коефіцієнт при x.</param>
        /// <param name="b">Коефіцієнт при y.</param>
        /// <param name="c">Вільний член C.</param>
        public Line(double a, double b, double c) : base(a, b, c)
        {
        }

        protected override void ValidateCoefficients()
        {
            // Перевірка інваріанта: пряма має бути 2D.
            if (Dimension != 2)
                throw new ArgumentException("Line must be 2-dimensional (requires 3 coefficients).");

            // Перевірка інваріанта: A і B не можуть бути одночасно нульовими
            if (Math.Abs(Coefficients[0]) < Epsilon && Math.Abs(Coefficients[1]) < Epsilon)
                throw new ArgumentException("For a 2D line, A and B cannot both be zero.");
        }

        public override bool BelongsToShape(params double[] coords)
        {
            return Math.Abs(Evaluate(coords)) < Epsilon;
        }

        public override string ToString()
        {
            return FormatEquation(new[] { "x", "y" });
        }
    }
}