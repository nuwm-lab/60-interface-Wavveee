using System;

namespace Geometry
{
    public class Line : GeometricEquation
    {
        public Line(double a, double b, double c) : base(a, b, c)
        {
            if (Math.Abs(a) < Epsilon && Math.Abs(b) < Epsilon)
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
