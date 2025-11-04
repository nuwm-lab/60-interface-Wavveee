using System;

namespace Geometry
{
    public class HyperPlane : GeometricEquation
    {
        public HyperPlane(params double[] coefficients) : base(coefficients)
        {
            if (coefficients.Length != 5)
                throw new ArgumentException("4D hyperplane requires exactly 5 coefficients (A, B, C, D, E).");
        }

        public override bool BelongsToShape(params double[] coords)
        {
            return Math.Abs(Evaluate(coords)) < Epsilon;
        }

        public override string ToString()
        {
            string[] vars = { "x1", "x2", "x3", "x4" };
            return FormatEquation(vars);
        }
    }
}

