using System;

namespace Geometry
{
    public interface IShape
    {
        bool BelongsToShape(params double[] coords);

        string PrintEquation(); // <-- додано, згідно контракту
    }
}
