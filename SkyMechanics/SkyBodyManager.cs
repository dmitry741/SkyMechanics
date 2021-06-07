using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace SkyMechanics
{
    class SkyBodyManager : IList<SkyBody>
    {
        readonly List<SkyBody> _items = new List<SkyBody>();

        #region === private methods ===

        void Process(SkyBodyBase sb1, SkyBodyBase sb2)
        {
            Vector2 v = sb2.Position - sb1.Position;
            float r2 = v.LengthSquared();
            float a1 = G * sb2.Weight / r2;
            float a2 = G * sb1.Weight / r2;
            Vector2 vn = Vector2.Normalize(v);
            Vector2 da1 = a1 * vn;
            Vector2 da2 = -a2 * vn;

            sb1.Acceleration += da1;
            sb2.Acceleration += da2;
        }

        #endregion

        #region === public ===

        public float G { get; set; } = 0.4f;

        public void Next()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                for (int j = i + 1; j < _items.Count; j++)
                {
                    Process(_items[i], _items[j]);
                }
            }

            foreach(SkyBody sb in _items)
            {
                sb.Move();
            }
        }

        public RectangleF BoundRect
        {
            get
            {
                float size = _items.Max(s => Math.Max(Math.Abs(s.Position.X), Math.Abs(s.Position.Y)));
                return new RectangleF(-size, -size, 2 * size, 2 * size);
            }
        }

        #endregion

        #region === IList implementation ===

        public SkyBody this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        public int Count => _items.Count();

        public bool IsReadOnly => false;

        public void Add(SkyBody item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(SkyBody item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(SkyBody[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<SkyBody> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public int IndexOf(SkyBody item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, SkyBody item)
        {
            _items.Insert(index, item);
        }

        public bool Remove(SkyBody item)
        {
            return _items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion
    }
}
