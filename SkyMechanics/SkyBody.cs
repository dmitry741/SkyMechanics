using System;
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

        readonly Color _color = Color.Black;
        readonly Bitmap _texture = null;

        #endregion

        public SkyBody(Vector2 position, Vector2 velocity, float r, float weight, Color color)
        {
            Position = position;
            Velocity = velocity;
            R = r;
            Weight = weight;
            _color = color;
        }

        public SkyBody(Vector2 position, Vector2 velocity, float r, float weight, Bitmap texture)
        {
            Position = position;
            Velocity = velocity;
            R = r;
            Weight = weight;
            _texture = texture;
        }

        public Color SBColor => _color;
        public Bitmap Texture => _texture;
    }
}
