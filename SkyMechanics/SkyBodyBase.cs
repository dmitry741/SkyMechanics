using System.Numerics;

namespace SkyMechanics
{
    /// <summary>
    /// Класс представляющий общие свойства небесного тела.
    /// </summary>
    class SkyBodyBase
    {
        /// <summary>
        /// Радиус.
        /// </summary>
        public float R { get; set; }

        /// <summary>
        /// Масса.
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Позиция.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Скорость.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Ускорение.
        /// </summary>
        public Vector2 Acceleration { get; set; }

        /// <summary>
        /// Движение.
        /// </summary>
        public void Move()
        {
            Velocity += Acceleration;
            Position += Velocity;
            Acceleration = Vector2.Zero;
        }
    }
}
