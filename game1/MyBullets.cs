using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game1
{
    /// <summary>
    /// 我方子弹
    /// </summary>
    public class MyBullets:Bullets
    {
        public MyBullets(float x, float y, int height, int width, int speed, GameConsole gc, bool state, int type,int angle)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
            Speed = speed;
            GC = gc;
            State = state;
            Type = type;
            Angle = angle;
        }

        public void Draw(Graphics g)
        {
            if (this.State)
            {
                Move();
                BumpPlane();
                g.DrawImage(GC.imageMyBullet, X, Y, Width, Height);
            }
            else
            {
                this.GC.myBullets.Remove(this);
            }
        }

        public void BumpPlane()
        {
            for (int i = 0; i < GC.enemyPlanes.Count; i++)
            {
                if (this.GetBulletsRectangle().IntersectsWith(GC.enemyPlanes[i].GetEnemyPlaneRectangle()))
                {
                    this.State = false;
                    GC.enemyPlanes[i].HP--;
                    if(GC.enemyPlanes[i].HP<1)
                    {
                        GC.enemyPlanes[i].State = false;
                        GC.Grade += (GC.enemyPlanes[i].Type + 1) * 123;
                    }
                }
            }
        }
    }
}
