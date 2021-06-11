using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.NETCore31.ConsoleProject
{
    public class STATest
    {
        public void Show()
        {
            Console.WriteLine($"This is STATest Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");

            var task = this.CalculationAsync(1_000_000);
            task.Wait();
            long lResult = task.Result;

            //long lResult = this.GetCalculationAsync(1_000_000);

            Console.WriteLine($"This is STATest   End,ThreadId={Thread.CurrentThread.ManagedThreadId}  lResult={lResult}");
        }

        private long GetCalculationAsync(long total)
        {
            var taskLong = Task.Run(() =>
            {
                var task = this.CalculationAsync(total);
                long lResult = task.Result;
                return lResult;
            });
            return taskLong.Result;
        }

        private async Task<long> CalculationAsync(long total)
        {
            var result= await Task.Run(() =>
            {
                Console.WriteLine($"This is CalculationAsync Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
                long lResult = 0;
                for (int i = 0; i < total; i++)
                {
                    lResult += i;
                }
                Console.WriteLine($"This is CalculationAsync   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");

                return lResult;
            });

            Console.WriteLine($"This is CalculationAsync  Out,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            return result;
        }
    }
}
