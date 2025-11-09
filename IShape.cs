using System;

namespace Geometry
{
    /// <summary>
    /// Represents a geometric object that can check point belonging and return its equation.
    /// </summary>
    public interface IShape
    {
        int Dimension { get; }
        bool BelongsToShape(params double[] coords);
        string PrintEquation();
    }

    /// <summary>
    /// Interface for objects that can provide a concise log description.
    /// </summary>
    public interface ILoggable
    {
        string GetLogDescription();
    }
}