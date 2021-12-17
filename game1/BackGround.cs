using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game1
{
    /// <summary>
    /// 背景类
    /// </summary>
    public class BackGround
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Speed { get; set; }
        public GameConsole GC { get; set; }

        public BackGround(int x,int y,int height,int width,int speed,GameConsole gc)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
            Speed = speed;
            GC = gc;
        }

        //背景绘制
        public void Draw(Graphics g)
        {
            g.DrawImage(GC.imageBackGround1, X, Y, Width, Height);
            g.DrawImage(GC.imageBackGround1, X + GC.Width-1, Y, Width, Height);
            Move();
        }

        //背景移动
        private void Move()
        {
            X -= Speed;

            if (X < -Width+1)
            {
                X = 0;
            }
        }
    }
}
