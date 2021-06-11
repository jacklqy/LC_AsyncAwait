using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zhaoxi.NETCore31.AspNetCoreProject.Models;

namespace Zhaoxi.NETCore31.AspNetCoreProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sleep(string type, int loop, int second)
        {
            Console.WriteLine($"This is {type},{loop} times start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(second * 1000);
            Console.WriteLine($"This is {type},{loop} times   end,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            return View();
        }

        public IActionResult Sync()
        {
            Console.WriteLine($"This is Sync Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            var task = this.CalculationAsync(1_000_000);
            task.Wait();
            long lResult = task.Result;
            //long lResult = this.GetCalculationAsync(1_000_000);
            Console.WriteLine($"This is Sync   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");
            base.ViewBag.lResult = lResult;
            return View();
        }

        public async Task<IActionResult> Async1()
        {
            Console.WriteLine($"This is Async Start,ThreadId={Thread.CurrentThread.ManagedThreadId}");

            long lResult = await this.CalculationAsync(1_000_000);

            Console.WriteLine($"This is Async   End,ThreadId={Thread.CurrentThread.ManagedThreadId}");

            base.ViewBag.lResult = lResult;
            return View();
        }

        #region Async
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
            return await Task.Run(() =>
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
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
