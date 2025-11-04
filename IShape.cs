using System;

namespace Geometry
{
    /// <summary>
    /// Represents a geometric object that can check point belonging and return its equation.
    /// </summary>
    public interface IShape
    {
        bool BelongsToShape(params double[] coords);
        string PrintEquation();
    }
}
