using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game1
{
    public partial class GameConsole : Form
    {
        //封面
        public Image imgGameStart = Picture.GameStart;
        public Image imgGameOver = Picture.Gameover;
        //背景
        public Image imageBackGround1 = Picture.back1;
        //我的飞机
        public Image imageMyPlane = Picture.plane;
        public Image imageMyPlaneCenter = Picture.plane;
        public Image imageMyPlaneLeft = Picture.left;
        public Image imageMyPlaneRight = Picture.right;
        //敌方飞机
        public Image[] imageEnemyPlane =
            {
            Picture.enemyplane1,Picture.enemyplane2
            };
        //子弹
        public Image imageMyBullet = Picture.bullet;
        public Image imageEnemyBullet = Picture.enemybullet;
        //生命
        public Image imageMyHP = Picture.life;
        //爆炸
        public Image[] imageExplode =
        {
            Picture.explode1,
            Picture.explode2,
            Picture.explode3,
            Picture.explode4
        };
        //分数
        public int Grade=0;
        public Image[] imageGrade =
        {
            Picture._0,Picture._1,Picture._2,Picture._3,Picture._4,
            Picture._5,Picture._6,Picture._7,Picture._8,Picture._9
        };
        //道具
        public Image[] imageDropGoods =
        {
            Picture.lifemissile,Picture.bulletbox1
        };
        

        //背景音乐
        public Sound soundBackGound=new Sound(@"music/bgmusic.mp3");
        //准备音乐
        public Sound soundGameStart = new Sound(@"music/gamebegin.mp3");
        //子弹
        public Sound soundMyBullets = new Sound(@"music/mybullet.mp3");
        public Sound soundEnemyBullets = new Sound(@"music/enemybullet.mp3");
        //爆炸
        public Sound soundExplode = new Sound(@"music/explode.mp3");
        public Sound soundMyExplode = new Sound(@"music/myplaneexplode.mp3");
        //游戏结束
        public Sound soundGameOver = new Sound(@"music/gameover.mp3");
        //掉落
        public Sound soundDropGoods = new Sound(@"music/bullet.mp3");

        public Random random = new Random();
        public GameState gameState = GameState.Prepare;

        //各种类
        public BackGround backGround;
        public MyPlane myPlane;
        public List<EnemyPlane> enemyPlanes;
        public List<MyBullets> myBullets;
        public List<EnemyBullets> enemyBullets;
        public List<Explode> explodes;
        public StatusBar statusBar;
        public List<DropGoods> dropGoods;
        public int enemyMaxHP { get; set; }

        public GameConsole()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.AllPaintingInWmPaint, true);

            this.Width = 980;
            this.Height = 500;
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //双缓冲，防闪烁
            this.DoubleBuffered = true;
            //悬浮在最前
            //this.TopMost = true;
             
            backGround= new BackGround(0, 0, this.Height, this.Width, 4, this);

            myPlane = new MyPlane(30, 240, 40, 80, 2, this, true, 15,2);

            enemyPlanes = new List<EnemyPlane>();

            myBullets = new List<MyBullets>();

            enemyBullets = new List<EnemyBullets>();

            explodes = new List<Explode>();

            statusBar = new StatusBar(5,5,this.Width-10,40,true,this);

            dropGoods = new List<DropGoods>();

            soundGameStart.PlayRepeat();
        }
        
        //画面刷新率
        private void Run()
        {
            while(true)
            {
                Thread.Sleep(20);
                CreatEnemyPlane();
                this.Invalidate();
                enemyMaxHP = Grade / 10000;
            }
        }
        
        //重构基类函数
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (gameState == GameState.Prepare)
            {
                e.Graphics.DrawImage(imgGameStart, 0, 0, this.Width, this.Height);
            }
            else if (gameState == GameState.Finish)
            {
                e.Graphics.DrawImage(imgGameOver, 0, 0, this.Width, this.Height);
            }
            else
            {
                backGround.Draw(e.Graphics);

                statusBar.Draw(e.Graphics);

                for (int i = 0; i < enemyPlanes.Count; i++)
                {
                    enemyPlanes[i].Draw(e.Graphics);
                }

                myPlane.Draw(e.Graphics);

                for (int i = 0; i < myBullets.Count; i++)
                {
                    myBullets[i].Draw(e.Graphics);
                }

                for (int i = 0; i < enemyBullets.Count; i++)
                {
                    enemyBullets[i].Draw(e.Graphics);
                }

                for (int i = 0; i < explodes.Count; i++)
                {
                    explodes[i].Draw(e.Graphics);
                }

                for (int i = 0; i < dropGoods.Count; i++)
                {
                    dropGoods[i].Draw(e.Graphics);
                }
            }
        }
        
        //计时器
        private void AllTime()
        {
            int Fire=0;
            int overTime=0;
            int explodesTime=0;
            while(true)
            {
                Thread.Sleep(50);
                Fire++;explodesTime++;
                if(gameState==GameState.Over)
                {
                    overTime++;
                }
                if(Fire==4)
                {
                    myPlane.Fire();
                    Fire = 0;
                }
                if(overTime==20)
                {
                    overTime = 0;
                    gameState = GameState.Finish;
                    soundGameOver.PlayRepeat();
                }
                if (explodesTime == 4)
                {
                    for (int i = 0; i < explodes.Count; i++)
                    {
                        explodes[i].Time();
                    }
                    explodesTime = 0;
                }
            }
        }

        #region//控制按键
        private void GameConsole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Environment.Exit(0);
            }
            else if(gameState==GameState.Prepare&&e.KeyData==Keys.Space)
            {
                Thread t = new Thread(new ThreadStart(Run));
                t.Start();
                t.IsBackground = true;

                Thread timer = new Thread(new ThreadStart(AllTime));
                timer.Start();
                timer.IsBackground = true;

                gameState = GameState.Start;
                soundBackGound.PlayRepeat();
            }
            else
                myPlane.KeyDown(e);
        }

        private void GameConsole_KeyUp(object sender, KeyEventArgs e)
        {
            myPlane.KeyUp(e);
        }
        #endregion

        //创造敌机
        public void CreatEnemyPlane()
        {
            Direction d;
            switch(random.Next(10))
            {
                case 1:d = Direction.Up;break;
                case 2:d = Direction.Down;break;
                case 3:d = Direction.LeftDown;break;
                case 4:d = Direction.LeftUp;break;
                default:d = Direction.Left;break;
            }
            if(random.Next(100)<(Grade/5000)+1)
            {
                enemyPlanes.Add(new EnemyPlane(980, random.Next(45,this.Height - 30), 40, 70, random.Next(1,3), this, true, d, EnemyPlaneType()));
            }
        }

        public int EnemyPlaneType()
        {
            switch(random.Next(5))
            {
                case 0:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
