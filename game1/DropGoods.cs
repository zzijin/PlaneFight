using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game1
{
    /// <summary>
    /// 掉落物
    /// </summary>
    public class DropGoods
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Speed { get; set; }
        public bool State { get; set; }
        public int Type { get; set; }
        public Direction direction { get; set; }

        public GameConsole GC { get; set; }

        public DropGoods(int x, int y, int height, int width, int speed, GameConsole gc, bool state, Direction dir, int type)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
            Speed = speed;
            GC = gc;
            State = state;
            Type = type;
            switch(dir)
            {
                case Direction.Up:direction = Direction.LeftUp;break;
                case Direction.Down:direction = Direction.LeftDown;break;
                default:direction = dir;break;
            }
        }

        public void Draw(Graphics g)
        {
            if (this.State)
            {
                Move();
                g.DrawImage(this.GC.imageDropGoods[Type], X, Y, Width, Height);
            }
            else
            {
                GC.soundDropGoods.PlayAsync();
                this.GC.dropGoods.Remove(this);
            }
        }

        public void Move()
        {
            switch (direction)
            {
                case Direction.Up:
                    if (X > GC.Width - Width - 50)
                        X--;
                    else
                        Y -= Speed;
                    break;
                case Direction.Down:
                    if (X > GC.Width - Width - 50)
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
                this.GC.dropGoods.Remove(this);
            }
            if (Y > GC.Height - Height - 5)
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
            if (this.GetEnemyPlaneRectangle().IntersectsWith(GC.myPlane.GetMyPlaneRectangle()))
            {
                this.State = false;
                switch (Type)
                {
                    case 0:
                        if (GC.myPlane.HP < GC.myPlane.MaxHP)
                            GC.myPlane.HP++;
                        break;
                    case 1:
                        GC.myPlane.myBulletsNumber++;
                        break;
                    
                }
            }
        }
    }
}
