using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SkyMechanics
{
    class SkyBodyBase
    {
        public float R { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public void Move()
        {
            Velocity += Acceleration;
            Position += Velocity;
            Acceleration = Vector2.Zero;
        }
    }
}
