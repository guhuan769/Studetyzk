using aspnetcorecancelMVC1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace aspnetcorecancelMVC1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public static async Task DowloadAsync3(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    //如果10分钟没下完我就提前终止此任务 那么此任务该怎么实现呢？
                    var httpResponse = await httpClient.GetAsync(url, cancellationToken);
                    //那么获取HTML内容应该怎么去写呢
                    string html = await httpResponse.Content.ReadAsStringAsync();
                    //在APS.NET 经量通过Debug来处理
                    Debug.WriteLine(html);
                }

            }
        }
        //所有的ASP.NET方法控制器都可以加 CancellationToken cancellationToken 也并不需要赋值 如果不用CancellationToken 浏览器已经访问其他网页时那么服务器依然会继续完成任务所以必须使用 CancellationToken 
        //如果我访问其他网址服务器就停止任务
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            await DowloadAsync3("https://youzack.com", 100, cancellationToken);
            return View();
        }

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