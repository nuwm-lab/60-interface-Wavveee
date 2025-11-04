namespace Geometry
{
    public class HyperPlane : GeometricEquation
    {
        public HyperPlane(params double[] coefficients) : base(coefficients) { }

        public override bool BelongsToShape(params double[] coords)
            => Math.Abs(Evaluate(coords)) < Epsilon;

        public override string ToString()
        {
            // для 4D: x, y, z, w і т.д.
            var variableNames = Enumerable.Range(0, _coefficients.Length - 1)
                                         .Select(i => $"x{i+1}")
                                         .ToArray();

            return FormatEquation(variableNames);
        }
    }
}
