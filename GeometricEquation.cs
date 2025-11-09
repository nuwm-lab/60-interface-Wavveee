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
        // Приватне поле для зберігання коефіцієнтів. 
        // Зберігаємо власний клон масиву для уникнення зовнішньої мутації.
        private readonly double[] _coefficients;
        
        protected const double Epsilon = 1e-12;

        /// <summary>
        /// Кількість вимірів (координат), необхідних для перевірки.
        /// Обчислюється як загальна кількість коефіцієнтів мінус один (вільний член).
        /// </summary>
        public int Dimension => _coefficients.Length - 1;

        /// <summary>
        /// Returns a copy of the coefficient list to preserve encapsulation.
        /// </summary>
        public IReadOnlyList<double> Coefficients => Array.AsReadOnly(_coefficients);

        /// <summary>
        /// Ініціалізує нове рівняння.
        /// Порядок коефіцієнтів: [A1, A2, ..., An, An+1], де An+1 — вільний член.
        /// x1, x2, ..., xn — це змінні, що відповідають координатам.
        /// </summary>
        /// <param name="coefficients">Масив коефіцієнтів.</param>
        protected GeometricEquation(params double[] coefficients)
        {
            if (coefficients is null)
                throw new ArgumentNullException(nameof(coefficients));

            // Створюємо копію масиву, щоб уникнути зміни ззовні.
            _coefficients = (double[])coefficients.Clone(); 
            
            // Загальні перевірки
            if (_coefficients.Length == 0)
                throw new ArgumentException("At least one coefficient (the free term) is required.");

            if (_coefficients.All(c => Math.Abs(c) < Epsilon))
                throw new ArgumentException("All coefficients cannot be zero.");

            // Перевірки, специфічні для конкретної форми
            ValidateCoefficients();
        }

        /// <summary>
        /// Віртуальний метод для перевірки інваріантів конкретної форми 
        /// (наприклад, мінімальна кількість вимірів, ненульові провідні коефіцієнти).
        /// </summary>
        protected virtual void ValidateCoefficients()
        {
            // Базовий клас не має додаткових інваріантів, крім тих, що у конструкторі.
        }

        protected virtual double Evaluate(params double[] coords)
        {
            if (coords is null)
                throw new ArgumentNullException(nameof(coords));

            // Перевірка відповідності кількості координат виміру
            if (coords.Length != Dimension)
                throw new ArgumentException($"Expected {Dimension} coordinates.");

            // Вільний член A_{n+1}
            double sum = _coefficients[^1]; 
            
            // Додавання суми A_i * x_i
            for (int i = 0; i < coords.Length; i++)
                sum += _coefficients[i] * coords[i];

            return sum;
        }

        /// <summary>
        /// Форматує рівняння у вигляді рядка, наприклад, "3x - 5y + 1 = 0".
        /// Порядок змінних: x1 (або x), x2 (або y), x3 (або z), ..., xn.
        /// </summary>
        /// <param name="vars">Масив символьних імен для змінних (x1, x2, ...).</param>
        /// <returns>Відформатоване рівняння.</returns>
        protected string FormatEquation(string[] vars)
        {
            if (vars is null || vars.Length != Dimension)
                throw new ArgumentException("Variable names count must match dimension.");
                
            var sb = new StringBuilder();
            
            // Ітерація по коефіцієнтах A1, A2, ..., An (без вільного члена)
            for (int i = 0; i < vars.Length; i++)
            {
                double c = _coefficients[i];
                if (Math.Abs(c) < Epsilon) continue;

                // Додавання знаку
                if (sb.Length > 0)
                    sb.Append(c >= 0 ? " + " : " - ");
                else if (c < 0)
                    sb.Append("- ");

                // Додавання абсолютного значення коефіцієнта (якщо не 1 або -1)
                double abs = Math.Abs(c);
                if (Math.Abs(abs - 1) > Epsilon)
                    sb.Append($"{abs:0.###}");

                sb.Append(vars[i]);
            }

            // Додавання вільного члена An+1
            double free = _coefficients[^1];
            if (Math.Abs(free) >= Epsilon)
                sb.Append(free >= 0 ? $" + {free:0.###}" : $" - {Math.Abs(free):0.###}");
            else if (sb.Length == 0)
            {
                // Випадок, коли всі коефіцієнти нульові (має бути відловлено в конструкторі)
                return "0 = 0";
            }
            
            sb.Append(" = 0");
            return sb.ToString();
        }

        public abstract bool BelongsToShape(params double[] coords);

        public virtual string PrintEquation() => ToString();
    }
}
