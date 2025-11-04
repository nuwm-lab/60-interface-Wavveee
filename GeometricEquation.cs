using System;

public abstract class GeometricEquation : IShape
{
    private readonly double _a;
    private readonly double _b;
    private readonly double _c;

    protected const double Epsilon = 1e-12;

    protected GeometricEquation(double a, double b, double c)
    {
        _a = a;
        _b = b;
        _c = c;
    }

    protected double A => _a;
    protected double B => _b;
    protected double C => _c;

    public abstract string PrintEquation();
    public abstract bool BelongsToShape(params double[] coordinates);

    protected string FormatTerm(double coefficient, string variable)
    {
        if (Math.Abs(coefficient) < Epsilon) return string.Empty;

        string sign = coefficient > 0 ? " + " : " - ";
        double value = Math.Abs(coefficient);

        return $"{sign}{value}*{variable}";
    }
}
