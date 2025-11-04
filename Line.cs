namespace Geometry
{
    public class Line : GeometricEquation
    {
        public Line(double a, double b, double c) : base(a, b, c) {}

        public override bool BelongsToShape(params double[] coords)
            => Math.Abs(Evaluate(coords)) < Epsilon;

        public override string ToString()
            => FormatEquation(new[] { "x", "y" });
    }
}
