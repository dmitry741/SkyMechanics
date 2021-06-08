using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace SkyMechanics
{
    /// <summary>
    /// Класс небесного тела с параметрами отображения.
    /// </summary>
    class SkyBody : SkyBodyBase
    {
        #region === members ===

        readonly Color _color = Color.Black;
        readonly Bitmap _texture = null;

        #endregion

        /// <summary>
        /// Конструктор объекта SkyBody.
        /// </summary>
        /// <param name="position">Позиция.</param>
        /// <param name="velocity">Скорость.</param>
        /// <param name="r">Радиус.</param>
        /// <param name="weight">Масса.</param>
        /// <param name="color">Цвет.</param>
        public SkyBody(Vector2 position, Vector2 velocity, float r, float weight, Color color)
        {
            Position = position;
            Velocity = velocity;
            R = r;
            Weight = weight;
            _color = color;
        }

        /// <summary>
        /// Конструктор объекта SkyBody.
        /// </summary>
        /// <param name="position">Позиция.</param>
        /// <param name="velocity">Скорость.</param>
        /// <param name="r">Радиус.</param>
        /// <param name="weight">Масса.</param>
        /// <param name="color">Текстура.</param>
        public SkyBody(Vector2 position, Vector2 velocity, float r, float weight, Bitmap texture)
        {
            Position = position;
            Velocity = velocity;
            R = r;
            Weight = weight;
            _texture = texture;
        }

        /// <summary>
        /// Цвет.
        /// </summary>
        public Color SBColor => _color;

        /// <summary>
        /// Текстура.
        /// </summary>
        public Bitmap Texture => _texture;
    }
}
