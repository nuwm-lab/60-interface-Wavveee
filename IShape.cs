using System;

namespace Geometry
{
    /// <summary>
    /// Represents a geometric object that can check point belonging and return its equation.
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// Gets the dimension of the space the shape exists in (e.g., 2 for Line, 4 for 4D HyperPlane).
        /// This is the required number of coordinates for BelongsToShape check.
        /// </summary>
        int Dimension { get; }

        bool BelongsToShape(params double[] coords);
        string PrintEquation();
    }
}