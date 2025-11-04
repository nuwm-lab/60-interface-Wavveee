using System;

namespace Geometry
{
    public interface IShape
    {
        /// <summary>
        /// Checks whether a point belongs to the geometric object.
        /// </summary>
        bool BelongsToShape(params double[] coords);

        /// <summary>
        /// Returns equation as formatted string.
        /// </summary>
        string PrintEquation();
    }
}
