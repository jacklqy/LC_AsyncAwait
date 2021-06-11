using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zhaoxi.NETFramework47.WinformProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"This is btnSync_Click Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");

            //var task = this.CalculationAsync(1_000_000);
            //task.Wait();
            //long lResult = task.Result;//同步阻塞，主线程在等结果

            long lResult = this.GetCalculationAsync(1_000_000);//换个线程去调用async方法，有点恶心

            Console.WriteLine($"This is btnSync_Click   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");

            this.lblSync.Text = lResult.ToString();
        }

        private async void btnAsync_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"This is btnAsync_Click Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");

            long lResult = await this.CalculationAsync(1_000_000);

            Console.WriteLine($"This is btnAsync_Click   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");

            this.lblAsyncResult.Text = lResult.ToString();//必须是UI线程，更新能成功是因为winform的UI线程特殊性，await之后的线程一定是UI线程
        }
        //21  12  11  22

        private long Calculation(long total)
        {
            long lResult = 0;
            for (int i = 0; i < total; i++)
            {
                lResult += i;
            }
            return lResult;
        }


        private long GetCalculationAsync(long total)
        {
            var taskLong = Task.Run(() =>
             {
                 var task = this.CalculationAsync(total);
                 long lResult = task.Result;//子线程
                 return lResult;
             });
            return taskLong.Result;//主线程在等Result
        }

        private async Task<long> CalculationAsync(long total)
        {
            var result = await Task.Run(() =>
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

            return result;//await后面的代码，会要求主线程来执行
        }

    }
}
