using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game1
{
    /// <summary>
    /// 敌机类
    /// </summary>
    public class EnemyPlane
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Speed { get; set; }
        public bool State { get; set; }
        public int Type { get; set; }
        public int HP { get; set; }
        public Direction direction { get; set; }
        public List<Bullets> bullets;

        public GameConsole GC { get; set; }

        public EnemyPlane(int x, int y, int height, int width, int speed, GameConsole gc, bool state, Direction dir,int type)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
            Speed = speed;
            GC = gc;
            State = state;
            direction = dir;
            Type = type;
            bullets = new List<Bullets>();
            switch(type)
            {
                case 0:
                    HP = GC.enemyMaxHP+1;
                    break;
                case 1:
                    HP =GC.enemyMaxHP*2+1;
                    break;
            }
        }

        public void Draw(Graphics g)
        {
            if(this.State)
            {
                Move();
                Fire();
                g.DrawImage(this.GC.imageEnemyPlane[Type], X, Y, Width, Height);
            }
            else
            {
                GC.explodes.Add(new Explode(X, Y, 40, 40, GC, true));
                if(GC.random.Next(5)>2)
                {
                    GC.dropGoods.Add(new DropGoods(X, Y, 30, 30, Speed, GC, true, direction, DropType()));
                }
                GC.soundExplode.PlayAsync();
                this.GC.enemyPlanes.Remove(this);
            }
        }

        public int DropType()
        {
            switch(GC.random.Next(6))
            {
                case 0:return 1;
                default:return 0;
            }
        }

        public Direction DropDireation()
        {
            if (direction == Direction.Up)
                return Direction.LeftUp;
            else if (direction == Direction.Down)
                return Direction.LeftDown;
            else
                return direction;
        }

        public void Move()
        {
            switch (direction)
            {
                case Direction.Up:
                    if (X > GC.Width - Width -50)
                        X--;
                    else
                        Y -= Speed;
                    break;
                case Direction.Down:
                    if (X > GC.Width - Width -50)
                        X--;
                    else
                        Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
                case Direction.RightUp:
                    X += Speed; Y -= Speed;
                    break;
                case Direction.RightDown:
                    X += Speed; Y += Speed;
                    break;
                case Direction.LeftUp:
                    X -= Speed; Y -= Speed;
                    break;
                case Direction.LeftDown:
                    X -= Speed; Y += Speed;
                    break;
                case Direction.Center:
                    break;
            }
            if (X < 0)
            {
                State = false;
                this.GC.enemyPlanes.Remove(this);
            }
            if (Y > GC.Height-Height-5)
            {
                if (direction == Direction.Down)
                    direction = Direction.Up;
                else
                    direction = Direction.LeftUp;
            }
            if (Y < 40)
            {
                if (direction == Direction.Up)
                    direction = Direction.Down;
                else
                    direction = Direction.LeftDown;
            }

            BumpPlane();
        }

        //矩形结构
        public Rectangle GetEnemyPlaneRectangle()
        {
            Rectangle r = new Rectangle(this.X, this.Y, this.Width, this.Height);
            return r;
        }

        //检查飞机碰撞
        public void BumpPlane()
        {
            if(this.GetEnemyPlaneRectangle().IntersectsWith(GC.myPlane.GetMyPlaneRectangle()))
            {
                this.State = false;
                GC.myPlane.HP=GC.myPlane.HP-(Type+1)*2;
            }
        }

        //
        public void Fire()
        {
            if (GC.random.Next(100) < 2)
            {
                GC.soundEnemyBullets.PlayAsync();
                int r = GC.random.Next(-3,10);
                int angle = 180;
                if (r < 4)
                    angle += 5 * r;
                GC.enemyBullets.Add(new EnemyBullets(X + 5, Y + Height / 2, 10, 10, Speed + 4, GC, true, 1, angle));
            }
        }
    }
}
