using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.NETCore31.ClassLibrary
{
    public class OnlyAsync
    {
        public async void SimpleMethod()
        {
            //Task.Run(() =>
            //{
            //    Thread.Sleep(1000);
            //    Console.WriteLine("In async Method");
            //});
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
            Console.WriteLine("******************");
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
