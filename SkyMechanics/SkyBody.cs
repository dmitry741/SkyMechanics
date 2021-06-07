﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace SkyMechanics
{
    class SkyBody : SkyBodyBase
    {
        #region === members ===

        Color _color;
        readonly Queue<Vector2> _queue = new Queue<Vector2>();

        #endregion

        public SkyBody(Vector2 position, Vector2 velocity, float r, float weight, Color color)
        {
            Position = position;
            Velocity = velocity;
            R = r;
            Weight = weight;
            _color = color;
        }

        public Color SBColor => _color;

        public IEnumerable<Vector2> Trace => _queue.ToList();
    }
}
