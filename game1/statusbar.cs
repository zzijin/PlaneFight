using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game1
{
    /// <summary>
    /// 状态栏
    /// </summary>
    public class StatusBar
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool State { get; set; }

        public GameConsole GC { get; set; }

        public StatusBar(int x,int y,int width,int heght,bool state,GameConsole gc)
        {
            X = x;
            Y = y;
            Width = width;
            Height = heght;
            State = state;
            GC = gc;
        }

        public void Draw(Graphics g)
        {
            //分数
            for (int i = 0; i < GC.Grade.ToString().Length; i++)
            {
                int o;
                o = GC.Grade / Convert.ToInt32(Math.Pow(10, i)) % 10;
                g.DrawImage(this.GC.imageGrade[o], GC.Width * 3 / 4 - i * 25, 5, 22, 27);
            }
            //生命
            for (int i = 0; i < GC.myPlane.HP; i++)
            {
                g.DrawImage(this.GC.imageMyHP, 5 + 20 * i, 10, 20, 20);
            }

            g.DrawLine(new Pen(Color.BlueViolet), new Point(0, Height - 3), new Point(GC.Width, Height - 3));
        }
    }
}
