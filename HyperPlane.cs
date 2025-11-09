using System;
using System.Linq;

namespace Geometry
{
    public class HyperPlane : GeometricEquation
    {
        /// <summary>
        /// Ініціалізує гіперплощину.
        /// Порядок коефіцієнтів: [A1, A2, ..., An, An+1], де An+1 — вільний член.
        /// Якщо кількість коефіцієнтів M, то це (M-1)-вимірна гіперплощина.
        /// </summary>
        public HyperPlane(params double[] coefficients) : base(coefficients)
        {
            // Примітка: Обмеження на 5 коефіцієнтів, яке було тут раніше, 
            // краще прибрати, щоб HyperPlane був узагальненням для N-вимірів.
            // Якщо потрібна виключно 4D площина, слід створити клас Plane4D.
            // Залишимо її універсальною для N-вимірів.
        }
        
        // (Опціонально, якщо ви хочете перевіряти, що хоча б один провідний коефіцієнт ненульовий)
        protected override void ValidateCoefficients()
        {
            // Перевіряємо, чи має гіперплощина хоча б один вимір (Dimension >= 1)
            if (Dimension < 1)
                throw new ArgumentException("HyperPlane must be in at least 1 dimension (min 2 coefficients).");
        }

        public override bool BelongsToShape(params double[] coords)
        {
            return Math.Abs(Evaluate(coords)) < Epsilon;
        }

        public override string ToString()
        {
            // Генеруємо імена змінних x1, x2, ..., xN
            string[] vars = Enumerable.Range(1, Dimension)
                                      .Select(i => $"x{i}")
                                      .ToArray();
            return FormatEquation(vars);
        }
    }
}
