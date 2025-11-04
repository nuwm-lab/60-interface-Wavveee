using System;

public class Line : GeometricEquation
{
    public Line(double a, double b, double c) : base(a, b, c)
    {
        if (Math.Abs(a) < Epsilon && Math.Abs(b) < Epsilon)
            throw new ArgumentException("A та B не можуть бути обидва рівні нулю для лінії.");
    }

    public override string PrintEquation()
    {
        return $"Line (2D):{FormatTerm(A, "x")}{FormatTerm(B, "y")}{FormatTerm(C, "")} = 0".Replace("+ -", "- ");
    }

    public override bool BelongsToShape(params double[] coordinates)
    {
        if (coordinates.Length != 2)
            throw new ArgumentException("Для лінії потрібно 2 координати.");

        double x = coordinates[0];
        double y = coordinates[1];

        return Math.Abs(A * x + B * y + C) < Epsilon;
    }
}
