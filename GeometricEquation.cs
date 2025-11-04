using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Geometry
{
    public abstract class GeometricEquation : IShape
    {
        protected readonly double[] _coefficients;
        protected const double Epsilon = 1e-12;

        /// <summary>
        /// Returns a copy of the coefficient list to preserve encapsulation.
        /// </summary>
        public IReadOnlyList<double> Coefficients => Array.AsReadOnly(_coefficients);

        protected GeometricEquation(params double[] coefficients)
        {
            if (coefficients is null)
                throw new ArgumentNullException(nameof(coefficients));

            if (coefficients.Length == 0)
                throw new ArgumentException("At least one coefficient is required.");

            if (coefficients.All(c => Math.Abs(c) < Epsilon))
                throw new ArgumentException("All coefficients cannot be zero.");

            _coefficients = coefficients;
        }

        protected virtual double Evaluate(params double[] coords)
        {
            if (coords is null)
                throw new ArgumentNullException(nameof(coords));

            if (coords.Length != _coefficients.Length - 1)
                throw new ArgumentException($"Expected {_coefficients.Length - 1} coordinates.");

            double sum = _coefficients[^1];
            for (int i = 0; i < coords.Length; i++)
                sum += _coefficients[i] * coords[i];

            return sum;
        }

        protected string FormatEquation(string[] vars)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < vars.Length; i++)
            {
                double c = _coefficients[i];
                if (Math.Abs(c) < Epsilon) continue;

                if (sb.Length > 0)
                    sb.Append(c >= 0 ? " + " : " - ");
                else if (c < 0)
                    sb.Append("- ");

                double abs = Math.Abs(c);
                if (Math.Abs(abs - 1) > Epsilon)
                    sb.Append($"{abs:0.###}");

                sb.Append(vars[i]);
            }

            double free = _coefficients[^1];
            if (Math.Abs(free) >= Epsilon)
                sb.Append(free >= 0 ? $" + {free:0.###}" : $" - {Math.Abs(free):0.###}");

            sb.Append(" = 0");
            return sb.ToString();
        }

        public abstract bool BelongsToShape(params double[] coords);

        public virtual string PrintEquation() => ToString();
    }
}

