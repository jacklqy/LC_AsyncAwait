using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.NETCore31.ClassLibrary
{
    public class AwaitAsyncILSpy
    {
        public static void Show()
        {
            Console.WriteLine($"start1 {Thread.CurrentThread.ManagedThreadId.ToString("00")}");
            Async();
            Console.WriteLine($"aaa2 {Thread.CurrentThread.ManagedThreadId.ToString("00")}");
        }
        public static async void Async()
        {
            Console.WriteLine($"ddd5 {Thread.CurrentThread.ManagedThreadId.ToString("00")}");
            await Task.Run(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine($"bbb3 {Thread.CurrentThread.ManagedThreadId.ToString("00")}");
            });
            Console.WriteLine($"ccc4 {Thread.CurrentThread.ManagedThreadId.ToString("00")}");
        }
    }
}
