using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.NETFramework47.ClassLibrary
{
    public class OnlyAsync
    {
        public async void SampleMethod()
        {
            await this.CreateTask();
            Console.WriteLine("******************");
            await this.CreateTask();
            Console.WriteLine("&&&&&&&&&&&&&&&&");
            await this.CreateTask();
            Console.WriteLine("((((((((((((((((");
            await this.CreateTask();
            Console.WriteLine("******************");
            await this.CreateTask();
            Console.WriteLine("&&&&&&&&&&&&&&&&");
            await this.CreateTask();
            Console.WriteLine("((((((((((((((((");
            await this.CreateTask();
        }

        private Task CreateTask()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("In async Method");
            });
        }
    }
}
