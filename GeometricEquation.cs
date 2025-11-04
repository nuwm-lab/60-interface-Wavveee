namespace Geometry
{
    public abstract class GeometricEquation : IShape
    {
        protected readonly double[] _coefficients;
        protected const double Epsilon = 1e-12;

        protected GeometricEquation(params double[] coefficients)
        {
            if (coefficients == null)
                throw new ArgumentNullException(nameof(coefficients));

            if (coefficients.Length == 0)
                throw new ArgumentException("At least one coefficient must be provided.");

            if (coefficients.All(c => Math.Abs(c) < Epsilon))
                throw new ArgumentException("All coefficients cannot be zero.");

            _coefficients = coefficients;
        }

        protected virtual double Evaluate(params double[] coords)
        {
            if (coords == null)
                throw new ArgumentNullException(nameof(coords));

            if (coords.Length != _coefficients.Length - 1)
                throw new ArgumentException($"Expected {_coefficients.Length - 1} coordinates.");

            double sum = _coefficients[^1]; // free term
            for (int i = 0; i < coords.Length; i++)
                sum += _coefficients[i] * coords[i];

            return sum;
        }

        public abstract bool BelongsToShape(params double[] coords);

        protected string FormatEquation(string[] variableNames)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < _coefficients.Length - 1; i++)
            {
                double c = _coefficients[i];
                if (Math.Abs(c) < Epsilon) continue;

                if (sb.Length > 0)
                    sb.Append(c >= 0 ? " + " : " - ");
                else if (c < 0)
                    sb.Append("- ");

                sb.Append($"{Math.Abs(c)}{variableNames[i]}");
            }

            double free = _coefficients[^1];
            if (Math.Abs(free) >= Epsilon)
                sb.Append(free >= 0 ? $" + {free}" : $" - {Math.Abs(free)}");

            sb.Append(" = 0");
            return sb.ToString();
        }
    }
}
