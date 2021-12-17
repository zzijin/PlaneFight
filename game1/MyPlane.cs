using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game1
{
    /// <summary>
    /// 我机类
    /// </summary>
    public class MyPlane
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Speed { get; set; }
        public bool State { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int myBulletsNumber { get; set; }
        public Direction direction { get; set; }
        private bool keyUp = false, keyDown = false, keyRight = false, keyLeft = false, fire = false;

        public GameConsole GC { get; set; }

        public MyPlane(int x, int y, int height, int width, int speed, GameConsole gc,bool state,int maxhp,int mybulletsn)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
            Speed = speed;
            GC = gc;
            State = state;
            MaxHP = maxhp;
            HP = MaxHP;
            direction = Direction.Center;
            myBulletsNumber = mybulletsn;
        }

        public void Draw(Graphics g)
        {
            if (HP <= 0)
            {
                this.State = false;
            }
            if (this.State)
            {
                Move();
                g.DrawImage(this.GC.imageMyPlane, X, Y, Width, Height);
                //g.DrawRectangle(new Pen(Color.Blue), GetMyPlaneRectangle());
            }
            else
            {
                GC.explodes.Add(new Explode(X, Y, 70, 70, GC, true));
                GC.soundMyExplode.PlayAsync();
                GC.gameState = GameState.Over;
                X = -100; Y = -100;
            }
        }

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.W:
                    keyUp = true;
                    break;
                case Keys.S:
                    keyDown = true;
                    break;
                case Keys.A:
                    keyLeft = true;
                    break;
                case Keys.D:
                    keyRight = true;
                    break;
                case Keys.J:
                    fire = true;
                    break;
            }
            SetDirection();
        }

        public void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.W:
                    keyUp = false;
                    break;
                case Keys.S:
                    keyDown = false;
                    break;
                case Keys.A:
                    keyLeft = false;
                    break;
                case Keys.D:
                    keyRight = false;
                    break;
                case Keys.J:
                    fire = false;
                    break;
            }
            SetDirection();
        }

        //设置飞机行动方向
        public void SetDirection()
        {
            //上右
            if (keyUp && keyRight && !keyDown && !keyLeft)
            {
                direction = Direction.RightUp;
            }
            //上左
            else if (keyUp && !keyRight && !keyDown && keyLeft)
            {
                direction = Direction.LeftUp;
            }
            //下右
            else if (!keyUp && keyRight && keyDown && !keyLeft)
            {
                direction = Direction.RightDown;
            }
            //下左
            else if (!keyUp && !keyRight && keyDown && keyLeft)
            {
                direction = Direction.LeftDown;
            }

            //上
            else if (keyUp && !keyRight && !keyDown && !keyLeft)
            {
                direction = Direction.Up;
            }
            //下
            else if (!keyUp && !keyRight && keyDown && !keyLeft)
            {
                direction = Direction.Down;
            }
            //右
            else if (!keyUp && keyRight && !keyDown && !keyLeft)
            {
                direction = Direction.Right;
            }
            //左
            else if (!keyUp && !keyRight && !keyDown && keyLeft)
            {
                direction = Direction.Left;
            }
            else
            {
                direction = Direction.Center;
            }
        }

        public void Move()
        {
            switch(direction)
            {
                case Direction.Up:
                        Y -= Speed;GC.imageMyPlane = GC.imageMyPlaneCenter;
                    break;
                case Direction.Down:
                        Y += Speed; GC.imageMyPlane = GC.imageMyPlaneCenter;
                    break;
                case Direction.Left:
                        X -= Speed; GC.imageMyPlane = GC.imageMyPlaneLeft;
                    break;
                case Direction.Right:
                        X += Speed; GC.imageMyPlane = GC.imageMyPlaneRight;
                    break;
                case Direction.RightUp:
                        X += Speed;Y -= Speed; GC.imageMyPlane = GC.imageMyPlaneRight;
                    break;
                case Direction.RightDown:
                        X += Speed;Y += Speed; GC.imageMyPlane = GC.imageMyPlaneRight;
                    break;
                case Direction.LeftUp:
                        X -= Speed;Y -= Speed; GC.imageMyPlane = GC.imageMyPlaneLeft;
                    break;
                case Direction.LeftDown:
                        X -= Speed;Y += Speed; GC.imageMyPlane = GC.imageMyPlaneLeft;
                    break;
                case Direction.Center:
                    GC.imageMyPlane = GC.imageMyPlaneCenter;
                    break;
            }
            if (X < 5) X = 5;
            if (X > GC.Width - Width-5) X = GC.Width - Width-5;
            if (Y < 40) Y = 40;
            if (Y > GC.Height - Height-20) Y = GC.Height - Height-20;
        }

        //矩形结构
        public Rectangle GetMyPlaneRectangle()
        {
            Rectangle r = new Rectangle(this.X, this.Y, this.Width, this.Height);
            return r;
        }

        //子弹
        public void Fire()
        {
            if (fire)
            {
                GC.soundMyBullets.PlayAsync();
                for (int i = -(myBulletsNumber/2); i < Math.Round((decimal)myBulletsNumber /2, MidpointRounding.AwayFromZero); i++)
                {
                    GC.myBullets.Add(new MyBullets(X+Width, Y + Height / 2, 10, 10, Speed * 2, GC, true, 1, i*3));
                }
            }
        }
    }
}
