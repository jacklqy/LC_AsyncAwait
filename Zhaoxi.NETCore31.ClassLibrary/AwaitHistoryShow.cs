using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.NETCore31.ClassLibrary
{
    public class AwaitHistoryShow
    {
        /// <summary>
        /// 同步方法--串行的，阻塞---有先后顺序
        /// </summary>
        public void SyncInvoke()
        {
            Console.WriteLine($"This is {nameof(SyncInvoke)}1");
            Thread.Sleep(1000);

            Console.WriteLine($"This is {nameof(SyncInvoke)}2");
            Thread.Sleep(1000);

            Console.WriteLine($"This is {nameof(SyncInvoke)}3");
            Thread.Sleep(1000);

            Console.WriteLine($"This is {nameof(SyncInvoke)}4");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// 多线程方法---并行(速度快)非阻塞---没有顺序
        /// </summary>
        public void TaskInvoke()
        {
            Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(TaskInvoke)}1");
                Thread.Sleep(1000);
            });

            Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(TaskInvoke)}2");
                Thread.Sleep(1000);
            });

            Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(TaskInvoke)}3");
                Thread.Sleep(1000);
            });

            Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(TaskInvoke)}4");
                Thread.Sleep(1000);
            });
        }

        /// <summary>
        /// 多线程方法---非阻塞---有顺序
        /// 不太爽---nodejs---回调式写法，嵌套是要疯掉
        /// </summary>
        public void TaskInvokeContinue()
        {
            Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(TaskInvoke)}1");
                Thread.Sleep(1000);
            }).ContinueWith(t =>
            {
                Console.WriteLine($"This is {nameof(TaskInvoke)}2");
                Thread.Sleep(1000);
            }).ContinueWith(t =>
            {
                Console.WriteLine($"This is {nameof(TaskInvoke)}3");
                Thread.Sleep(1000);
            }).ContinueWith(t =>
            {
                Console.WriteLine($"This is {nameof(TaskInvoke)}4");
                Thread.Sleep(1000);
            });
        }

        /// <summary>
        /// 要求有顺序，但是又不阻塞(线程忙碌等待)
        /// task.continuue
        /// 
        /// 用同步的方式去写异步回调
        /// 这里也不并发了，还不如搞同步方法--有顺序自然不能并发的，意义和价值，还得晚点讨论
        /// </summary>
        /// <returns></returns>
        public async Task AsyncInvoke()
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(AsyncInvoke)}1");
                Thread.Sleep(1000);
            });
            await Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(AsyncInvoke)}2");
                Thread.Sleep(1000);
            });
            await Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(AsyncInvoke)}3");
                Thread.Sleep(1000);
            });
            await Task.Run(() =>
            {
                Console.WriteLine($"This is {nameof(AsyncInvoke)}4");
                Thread.Sleep(1000);
            });
        }

    }
}
