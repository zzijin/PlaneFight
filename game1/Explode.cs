using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game1
{
    /// <summary>
    /// 爆炸特效
    /// </summary>
    public class Explode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Direction direction { get; set; }
        public bool State { get; set; }
        public int Index;
        public GameConsole GC { get; set; }

        public Explode(int x, int y, int height, int width, GameConsole gc, bool state)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
            GC = gc;
            direction = Direction.Center;
            State = state;
        }

        public void Draw(Graphics g)
        {
            if (State)
            {
                g.DrawImage(this.GC.imageExplode[Index], X + 30, Y + 20, Width + Index * 5, Height + Index * 5);
                if (Index >= 3)
                    State = false;
            }
            else
                GC.explodes.Remove(this);
        }

        public void Time()
        {
            Index++;
        }
    }
}
