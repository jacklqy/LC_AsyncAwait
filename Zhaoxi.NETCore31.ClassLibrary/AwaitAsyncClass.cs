using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.NETCore31.ClassLibrary
{
    /// <summary> 
    /// 
    /// </summary>
    public class AwaitAsyncClass
    {
        public static void TestShow()
        {
            Test();
        }

        private async static Task Test()
        {
            Console.WriteLine($"当前主线程id={Thread.CurrentThread.ManagedThreadId.ToString("00")}");
            {
                //NoReturnNoAwait();
            }
            {
                //NoReturn();
                //for (int i = 0; i < 10; i++)
                //{
                //    Thread.Sleep(300);
                //    Console.WriteLine($"Main Thread Task ManagedThreadId={Thread.CurrentThread.ManagedThreadId.ToString("00")} i={i}");
                //}
            }
            //{
            //    Task t = NoReturnTask();
            //    Console.WriteLine($"Main Thread Task ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
            //    //t.Wait();//主线程等待Task的完成  阻塞的
            //    //await t;//await后的代码会由线程池的线程执行  非阻塞
            //}
            {
                Task<long> t = SumAsync();
                Console.WriteLine($"Main Thread Task ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
                long lResult = t.Result;//访问result,阻塞式   主线程等待所有的任务挖成 //如果访问Result，就相当于是同步方法！
                Console.WriteLine("lResult=" + lResult);
                //t.Wait();//等价于上一行,阻塞式--同步
                //await t;//非阻塞，
            }
            {
                //Task<int> t = SumFactory();
                //Console.WriteLine($"Main Thread Task ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
                //long lResult = t.Result;//没有await和async 普通的task
                //t.Wait();
            }
            //Console.WriteLine($"Test Sleep Start {Thread.CurrentThread.ManagedThreadId}");
            //Thread.Sleep(10000);
            //Console.WriteLine($"Test Sleep End {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Main Thread Task ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
            Console.Read();
        }

        /// <summary>
        /// 只有async没有await
        /// 跟普通方法没有区别
        /// </summary>
        private static async void NoReturnNoAwait()
        {
            //主线程执行
            Task task = Task.Run(() =>//启动新线程完成任务
            {
                Console.WriteLine($"NoReturnNoAwait Sleep3000 before,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Console.WriteLine($"NoReturnNoAwait Sleep3000 after,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            });
            //主线程执行
            Console.WriteLine($"NoReturnNoAwait Sleep after Task,ThreadId={Thread.CurrentThread.ManagedThreadId}");
        }

        private static async void NoReturn()
        {
            //主线程执行
            Console.WriteLine($"NoReturn Sleep before await,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            TaskFactory taskFactory = new TaskFactory();
            Task task = taskFactory.StartNew(() =>
            {
                Console.WriteLine($"NoReturn Sleep3000 before,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(3000);
                Console.WriteLine($"NoReturn Sleep3000 after,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            });
            //task.ContinueWith(t => //这是一个回调
            //{
            //    Console.WriteLine($"NoReturn Sleep after await,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            //});

            await task;
            //主线程到await这里就返回了，执行主线程任务
            //同时task的子线程就开始工作，直到Task完成，然后继续后续任务（后续任务的线程ID不一定是这个子线程，可以是子线程，也可以是其他线程，还可以是主线程） 
            //像什么？ 效果上等价于continuewith
            //task.ContinueWith(t =>
            //{
            //    Console.WriteLine($"NoReturn Sleep after await,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            //});

            Console.WriteLine($"NoReturn Sleep after await,ThreadId={Thread.CurrentThread.ManagedThreadId}");
        }

        /// <summary>
        /// 无返回值  async Task == async void
        /// Task和Task<T>能够使用await, Task.WhenAny, Task.WhenAll等方式组合使用。Async Void 不行
        /// </summary>
        /// <returns></returns>
        private static async Task NoReturnTask() //在async/await方法里面如果没有返回值，默认返回一个Task
        {
            //这里还是主线程的id
            Console.WriteLine($"NoReturnTask Sleep before await,ThreadId={Thread.CurrentThread.ManagedThreadId}");

            Task task = Task.Run(() =>
            {
                Console.WriteLine($"NoReturnTask Sleep3000 before,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Console.WriteLine($"NoReturnTask Sleep3000 after,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            });
            await task;
            Console.WriteLine($"NoReturnTask Sleep after await,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            //return;
            //return new TaskFactory().StartNew(() => { });  //不能return  没有async才行
        }

        /// <summary>
        /// 带返回值的Task  
        /// 要使用返回值就一定要等子线程计算完毕
        /// </summary>
        /// <returns>async 就只返回long</returns>
        private static async Task<long> SumAsync()
        {
            Console.WriteLine($"SumAsync 111 start ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
            long result = 0;

            await Task.Run(() =>
            {
                for (int k = 0; k < 10; k++)
                {
                    Console.WriteLine($"SumAsync {k} await Task.Run ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(1000);
                }
                for (long i = 0; i < 5; i++)
                {
                    result += i;
                }
            });
            Console.WriteLine("result=" + result);
            Console.WriteLine($"SumFactory 111   end ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
            
            await Task.Run(() =>
            {
                for (int k = 0; k < 10; k++)
                {
                    Console.WriteLine($"SumAsync {k} await Task.Run ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(1000);
                }
                for (long i = 0; i < 5; i++)
                {
                    result += i;
                }
            });
            Console.WriteLine("result=" + result);
            Console.WriteLine($"SumFactory 111   end ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");

            await Task.Run(() =>
            {
                for (int k = 0; k < 10; k++)
                {
                    Console.WriteLine($"SumAsync {k} await Task.Run ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(1000);
                }

                for (long i = 0; i < 5; i++)
                {
                    result += i;
                }
            });
            Console.WriteLine("result=" + result);
            Console.WriteLine($"SumFactory 111   end ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");

            return result;
        }

        /// <summary>
        /// 真的返回Task  不是async  
        /// 
        /// 要使用返回值就一定要等子线程计算完毕
        /// </summary>
        /// <returns>没有async Task</returns>
        private static Task<int> SumFactory()
        {
            Console.WriteLine($"SumFactory 111 start ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
            TaskFactory taskFactory = new TaskFactory();
            Task<int> iResult = taskFactory.StartNew<int>(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"SumFactory 123 Task.Run ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
                return 123;
            });
            //Console.WriteLine($"This is {iResult.Result}");
            Console.WriteLine($"SumFactory 111   end ManagedThreadId={Thread.CurrentThread.ManagedThreadId}");
            return iResult;
        }

        #region foreach await
        private async Task Test8()
        {
            await foreach (var i in this.GenerateSequence())
            {
                Console.WriteLine($"this is {i} foreach await");
            }
        }

        public async IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }
        #endregion
    }
}

