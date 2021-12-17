using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game1
{
    /// <summary>
    /// 敌方子弹
    /// </summary>
    public class EnemyBullets:Bullets
    {
        public EnemyBullets(float x, float y, int height, int width, int speed, GameConsole gc, bool state, int type,int angle)
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
                g.DrawImage(GC.imageEnemyBullet, X, Y, Width, Height);
            }
            else
            {
                this.GC.enemyBullets.Remove(this);
            }
        }

        public void BumpPlane()
        {
            if (this.GetBulletsRectangle().IntersectsWith(GC.myPlane.GetMyPlaneRectangle()))
            {
                this.State = false;
                GC.myPlane.HP--;
            }
        }
    }
}
