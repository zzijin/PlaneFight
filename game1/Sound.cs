using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//API函数引用
using System.Runtime.InteropServices;
using System.Threading;
using System.Media;

namespace game1
{
    /// <summary>
    /// 声音类
    /// </summary>
    public class Sound
    {
        [DllImport("winmm.dll", SetLastError = true)]
        static extern int mciSendString(string strCommand, string strReturn, int iReturnLength, int hwndCallback);

        public string Path { get; set; }
        

        public Sound(string path)
        {
            this.Path = path;
        }

        //播放声音文件
        public void Play()
        {
            mciSendString(@"close music", null, 0, 0);
            mciSendString(@"open " + this.Path + " alias music", null, 0, 0);
            mciSendString("play music", null, 0, 0);
        }

        //循环播放声音文件
        public void PlayRepeat()
        {
            mciSendString(@"close music", null, 0, 0);
            mciSendString(@"open " + this.Path + " alias music", null, 0, 0);
            mciSendString("play music repeat", null, 0, 0);
        }

        //异步线程播放声音
        //λ  Lambda表达式
        public void PlayAsync()
        {
            ThreadStart th = new ThreadStart(() =>
            {
                Play();
            });
            th.BeginInvoke(null, null);
        }

    }
}
 