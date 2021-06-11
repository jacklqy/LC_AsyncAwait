using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.NETCore31.ClassLibrary
{
    public class AwaitAsyncTest
    {
        public static void Show()
        {
            #region ReadFile
            {
                Console.WriteLine("******************ReadFile***************");
                string path = "D:\\test\\1.zip";
                int loopNum = 10;//30
                {
                    Console.WriteLine("*****************Async****************");
                    List<Task> taskList = new List<Task>();
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    for (int i = 0; i < loopNum; i++)
                    {
                        taskList.Add(ReadAsync(path, i));
                    }
                    Task.WaitAll(taskList.ToArray());
                    stopwatch.Stop();
                    Console.WriteLine($"Async耗时{stopwatch.ElapsedMilliseconds}ms,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                }
                Thread.Sleep(3000);
                {
                    Console.WriteLine("*****************Task****************");
                    List<Task> taskList = new List<Task>();
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    for (int i = 0; i < loopNum; i++)
                    {
                        taskList.Add(ReadTask(path, i));
                    }
                    Task.WaitAll(taskList.ToArray());
                    stopwatch.Stop();
                    Console.WriteLine($"Task耗时{stopwatch.ElapsedMilliseconds}ms,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                }
                Thread.Sleep(3000);
                {
                    Console.WriteLine("*****************Sync****************");
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    for (int i = 0; i < loopNum; i++)
                    {
                        ReadSync(path, i);
                    }
                    stopwatch.Stop();
                    Console.WriteLine($"Sync耗时{stopwatch.ElapsedMilliseconds}ms,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                }
            }
            #endregion

            #region InvokeWeb
            {
                Console.WriteLine("******************InvokeWeb***************");
                string url = "http://localhost:8080/home/Sleep";
                int loopNum = 10;//5
                int second = 5;
                {
                    Console.WriteLine("*****************Task****************");
                    List<Task> taskList = new List<Task>();
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    for (int i = 0; i < loopNum; i++)
                    {
                        taskList.Add(WebTask(url, i, second));
                    }
                    Task.WaitAll(taskList.ToArray());
                    stopwatch.Stop();
                    Console.WriteLine($"Task耗时{stopwatch.ElapsedMilliseconds}ms,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                }
                Thread.Sleep(3000);
                {
                    Console.WriteLine("*****************Async****************");
                    List<Task> taskList = new List<Task>();
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    for (int i = 0; i < loopNum; i++)
                    {
                        taskList.Add(WebAsync(url, i, second));
                    }
                    Task.WaitAll(taskList.ToArray());
                    stopwatch.Stop();
                    Console.WriteLine($"Async耗时{stopwatch.ElapsedMilliseconds}ms,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                }
                Thread.Sleep(3000);
                {
                    Console.WriteLine("*****************Sync****************");
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    for (int i = 0; i < loopNum; i++)
                    {
                        WebSync(url, i, second);
                    }
                    stopwatch.Stop();
                    Console.WriteLine($"Sync耗时{stopwatch.ElapsedMilliseconds}ms,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                }
            }
            #endregion

            #region DoCalculation
            {
                Console.WriteLine("******************DoCalculation***************");
                int loopNum = 10;//5
                long total = 1_000_000_000;
                {
                    Console.WriteLine("*****************Task****************");
                    List<Task> taskList = new List<Task>();
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    for (int i = 0; i < loopNum; i++)
                    {
                        taskList.Add(DoCalculationTask(total, i));
                    }
                    Task.WaitAll(taskList.ToArray());
                    stopwatch.Stop();
                    Console.WriteLine($"Task耗时{stopwatch.ElapsedMilliseconds}ms,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                }
                Thread.Sleep(3000);
                {
                    Console.WriteLine("*****************Async****************");
                    List<Task> taskList = new List<Task>();
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    for (int i = 0; i < loopNum; i++)
                    {
                        taskList.Add(DoCalculationAsync(total, i));
                    }
                    Task.WaitAll(taskList.ToArray());
                    stopwatch.Stop();
                    Console.WriteLine($"Async耗时{stopwatch.ElapsedMilliseconds}ms,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                }
                Thread.Sleep(3000);
                {
                    Console.WriteLine("*****************Sync****************");
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    for (int i = 0; i < loopNum; i++)
                    {
                        DoCalculationSync(total, i);
                    }
                    stopwatch.Stop();
                    Console.WriteLine($"Sync耗时{stopwatch.ElapsedMilliseconds}ms,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                }
            }
            #endregion
        }

        #region Read
        private static async Task<byte[]> ReadAsync(string path, int num)
        {
            Console.WriteLine($"This is ReadAsync{num} Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            var result = await File.ReadAllBytesAsync(path);
            Console.WriteLine($"This is ReadAsync{num}   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            return result;
        }

        private static Task<byte[]> ReadTask(string path, int num)
        {
            Console.WriteLine($"This is ReadTask{num} Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            var result = Task.Run(() =>
            {
                Console.WriteLine($"This is ReadTask Ing,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                return File.ReadAllBytes(path);
            });
            Console.WriteLine($"This is ReadTask{num}   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            return result;
        }

        private static byte[] ReadSync(string path, int num)
        {
            Console.WriteLine($"This is ReadSync{num} Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            var result = File.ReadAllBytes(path);
            Console.WriteLine($"This is ReadSync{num}   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            return result;
        }
        #endregion

        #region InvokeWebRequest
        private static string InvokeWebRequest(string url)
        {
            string html = null;
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;//模拟请求
                request.Timeout = 30 * 1000;//
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)//发起请求
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Console.WriteLine($"抓取{url}地址返回失败,response.StatusCode为{response.StatusCode}");
                    }
                    else
                    {
                        StreamReader sr = new StreamReader(response.GetResponseStream());
                        html = sr.ReadToEnd();//读取数据
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Message.Equals("远程服务器返回错误: (306)。"))
                {
                    Console.WriteLine($"抓取{url}地址返回失败,远程服务器返回错误: (306)");
                    html = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"抓取{url}地址返回失败,远程服务器返回错误{ex.Message}");
                html = null;
            }
            return html;
        }
        private static async Task<string> InvokeWebRequestAsync(string url)
        {
            string html = null;
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;//模拟请求
                request.Timeout = 30 * 1000;//
                using (HttpWebResponse response = (await request.GetResponseAsync()) as HttpWebResponse)//发起请求
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Console.WriteLine($"抓取{url}地址返回失败,response.StatusCode为{response.StatusCode}");
                    }
                    else
                    {
                        StreamReader sr = new StreamReader(response.GetResponseStream());
                        return await sr.ReadToEndAsync();//异步
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Message.Equals("远程服务器返回错误: (306)。"))
                {
                    Console.WriteLine($"抓取{url}地址返回失败,远程服务器返回错误: (306)");
                    html = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"抓取{url}地址返回失败,远程服务器返回错误{ex.Message}");
                html = null;
            }
            return html;
        }
        private static async Task<string> WebAsync(string url, int num, int second)
        {
            url = $"{url}?loop={num}&type=Async&second={second}";
            Console.WriteLine($"This is InvokeAsync{url} Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            var result = await InvokeWebRequestAsync(url);
            Console.WriteLine($"This is InvokeAsync{url}   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            return result;
        }
        private static Task<string> WebTask(string url, int num, int second)
        {
            url = $"{url}?loop={num}&type=Task&second={second}";
            Console.WriteLine($"This is WebTask{num} Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            var result = Task.Run(() =>
            {
                Console.WriteLine($"This is WebTask Ing,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                return InvokeWebRequest(url);
            });
            Console.WriteLine($"This is WebTask{num}   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            return result;
        }
        private static string WebSync(string url, int num, int second)
        {
            url = $"{url}?loop={num}&type=Sync&second={second}";
            Console.WriteLine($"This is WebSync{num} Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            var result = InvokeWebRequest(url);
            Console.WriteLine($"This is WebSync{num}   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            return result;
        }
        #endregion

        #region DoCalculation CPU密集型
        private static long Calculation(long total)
        {
            long lResult = 0;
            for (int i = 0; i < total; i++)
            {
                lResult += i;
            }
            return lResult;
        }

        private static async Task<long> CalculationAsync(long total)
        {
            return await Task.Run(() =>
             {
                 long lResult = 0;
                 for (int i = 0; i < total; i++)
                 {
                     lResult += i;
                 }
                 return lResult;
             });
        }

        private static async Task<long> DoCalculationAsync(long total, int num)
        {
            Console.WriteLine($"This is DoCalculationAsync{num} Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            var result = await CalculationAsync(total);
            Console.WriteLine($"This is DoCalculationAsync{num}   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            return result;
        }

        private static Task<long> DoCalculationTask(long total, int num)
        {
            Console.WriteLine($"This is DoCalculationTask{num} Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            var result = Task.Run(() =>
            {
                Console.WriteLine($"This is DoCalculationTask Ing,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                return Calculation(total);
            });
            Console.WriteLine($"This is DoCalculationTask{num}   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            return result;
        }
        private static long DoCalculationSync(long total, int num)
        {
            Console.WriteLine($"This is DoCalculationSync{num} Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            var result = Calculation(total);
            Console.WriteLine($"This is DoCalculationSync{num}   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            return result;
        }
        #endregion
    }
}
