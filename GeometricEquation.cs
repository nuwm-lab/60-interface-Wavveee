using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Geometry
{
    /// <summary>
    /// Abstract base class for geometric shapes defined by a linear equation:
    /// A1*x1 + A2*x2 + ... + An*xn + An+1 = 0
    /// </summary>
    public abstract class GeometricEquation : IShape
    {
        private readonly double[] _coefficients;
        
        protected const double Epsilon = 1e-12;

        public int Dimension => _coefficients.Length - 1;

        public IReadOnlyList<double> Coefficients => Array.AsReadOnly(_coefficients);

        /// <summary>
        /// Ініціалізує нове рівняння.
        /// Порядок коефіцієнтів: [A1, A2, ..., An, An+1], де An+1 — вільний член.
        /// </summary>
        /// <param name="coefficients">Масив коефіцієнтів.</param>
        protected GeometricEquation(params double[] coefficients)
        {
            if (coefficients is null)
                throw new ArgumentNullException(nameof(coefficients));

            _coefficients = (double[])coefficients.Clone(); 
            
            if (_coefficients.Length == 0)
                throw new ArgumentException("At least one coefficient (the free term) is required.");

            if (_coefficients.All(c => Math.Abs(c) < Epsilon))
                throw new ArgumentException("All coefficients cannot be zero.");

            ValidateCoefficients();
        }

        /// <summary>
        /// Перевірка інваріантів, специфічних для похідного класу (наприклад, перевірка розмірності).
        /// </summary>
        protected virtual void ValidateCoefficients() { }

        // Новий абстрактний метод для демонстрації поліморфізму
        /// <summary>
        /// Нормалізує рівняння, наприклад, ділячи його на норму нормального вектора.
        /// Повертає нове рівняння (змінюваність у конструкторі заборонена).
        /// </summary>
        public abstract GeometricEquation Normalize();

        // Невіртуальний метод - фінальний контракт Print
        public string PrintEquation() => ToString();

        public abstract bool BelongsToShape(params double[] coords);

        /// <summary>
        /// Перевизначення ToString() для надання загального формату рівняння з x1...xN.
        /// Похідні класи можуть перевизначити його, щоб використовувати змінні x, y, z.
        /// </summary>
        public override string ToString()
        {
            // Генеруємо імена змінних за замовчуванням: x1, x2, ..., xN
            string[] vars = Enumerable.Range(1, Dimension)
                                      .Select(i => $"x{i}")
                                      .ToArray();
            return FormatEquation(vars);
        }

        protected virtual double Evaluate(params double[] coords)
        {
            // Перевірка кількості координат перенесена сюди
            if (coords.Length != Dimension)
                throw new ArgumentException($"Expected {Dimension} coordinates, but received {coords.Length}.");

            double sum = _coefficients[^1]; 
            
            for (int i = 0; i < coords.Length; i++)
                sum += _coefficients[i] * coords[i];

            return sum;
        }

        protected string FormatEquation(string[] vars)
        {
            if (vars.Length != Dimension)
                throw new ArgumentException("Variable names count must match dimension.");
                
            var sb = new StringBuilder();
            
            for (int i = 0; i < vars.Length; i++)
            {
                double c = _coefficients[i];
                if (Math.Abs(c) < Epsilon) continue;

                if (sb.Length > 0)
                    sb.Append(c >= 0 ? " + " : " - ");
                else if (c < 0)
                    sb.Append("- ");

                double abs = Math.Abs(c);
                if (Math.Abs(abs - 1) > Epsilon)
                    sb.Append($"{abs:0.###}");

                sb.Append(vars[i]);
            }

            double free = _coefficients[^1];
            if (Math.Abs(free) >= Epsilon)
                sb.Append(free >= 0 ? $" + {free:0.###}" : $" - {Math.Abs(free):0.###}");
            else if (sb.Length == 0)
            {
                // Це має бути відловлено у конструкторі, але як безпечний випадок
                return "0 = 0";
            }
            
            sb.Append(" = 0");
            return sb.ToString();
        }
    }
}
