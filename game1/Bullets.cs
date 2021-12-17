using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game1
{
    /// <summary>
    /// 子弹父类
    /// </summary>
    public class Bullets
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Speed { get; set; }
        public bool State { get; set; }
        public int Type { get; set; }
        public GameConsole GC { get; set; }
        public int Angle { get; set; }

        public void Move()
        {
            X = X + Speed * Convert.ToSingle(Math.Cos(Math.PI * Angle/ 180));
            Y = Y + Speed * Convert.ToSingle(Math.Sin(Math.PI * Angle / 180));
            if (X < 0)
                State = false;
            if (X > GC.Width-Width)
                State = false;
            if (Y > GC.Height-Height)
                State = false;
            if (Y < 0)
                State = false;
        }

        public Rectangle GetBulletsRectangle()
        {
            Rectangle r = new Rectangle(Convert.ToInt32(this.X), Convert.ToInt32(this.Y), this.Width, this.Height);
            return r;
        }
    }
}
