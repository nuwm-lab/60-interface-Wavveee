using System;
using System.Linq;

namespace Geometry
{
    public class HyperPlane : GeometricEquation
    {
        public HyperPlane(params double[] coefficients) : base(coefficients) { }

        public override bool BelongsToShape(params double[] coords)
            => Math.Abs(Evaluate(coords)) < Epsilon;

        public override string ToString()
        {
            var vars = Enumerable.Range(1, _coefficients.Length - 1)
                                 .Select(i => $"x{i}")
                                 .ToArray();
            return FormatEquation(vars);
        }
    }
}
