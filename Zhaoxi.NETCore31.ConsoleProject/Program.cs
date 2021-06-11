using System;
using System.Threading.Tasks;
using Zhaoxi.NETCore31.ClassLibrary;

namespace Zhaoxi.NETCore31.ConsoleProject
{

    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                {
                    //AwaitAsyncClass.TestShow();
                }
                {
                    AwaitAsyncTest.Show();
                }
                {
                    //new STATest().Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
