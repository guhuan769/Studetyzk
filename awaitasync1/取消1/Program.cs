namespace 取消1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //如果用户相同回车取消
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(5000);
            CancellationToken token = cancellationTokenSource.Token;
            DowloadAsync3("https://www.taobao.com", 100, token);
            while (Console.ReadLine() != "q")//等于q
            {

            }
            cancellationTokenSource.Cancel();
            Console.ReadLine();
        }
        static async Task Main1(string[] args)
        {
            //5秒没结束就终止
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(5000);
            CancellationToken token = cancellationTokenSource.Token;
            await DowloadAsync3("https://www.taobao.com", 200, token);
        }

        static async Task DowloadAsync(string url, int n)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    //如果10分钟没下完我就提前终止此任务 那么此任务该怎么实现呢？
                    string html = await httpClient.GetStringAsync(url);
                    Console.WriteLine($"{DateTime.Now}- {html}");
                }

            }
        }

        static async Task DowloadAsync(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    //如果10分钟没下完我就提前终止此任务 那么此任务该怎么实现呢？
                    string html = await httpClient.GetStringAsync(url);
                    Console.WriteLine($"{DateTime.Now}- {html}");
#if false 
                    //介意写法这样写 这样的写法最好 可以手动取消请求
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("请求被取消");
                        break;
                    } 
#endif
                    //这样也能达到终止的目的 发现请求被取消了则会抛异常 
                    cancellationToken.ThrowIfCancellationRequested();
                }

            }
        }

        static async Task DowloadAsync3(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    //如果10分钟没下完我就提前终止此任务 那么此任务该怎么实现呢？
                    var httpResponse = await httpClient.GetAsync(url, cancellationToken);
                    //那么获取HTML内容应该怎么去写呢
                    string html = await httpResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"{DateTime.Now}- {html}");
                    //写入记事本的时候也可以用CancellationToken
                    //await File.WriteAllTextAsync(url, html, cancellationToken);
#if false 
                    //介意写法这样写 这样的写法最好
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("请求被取消");
                        break;
                    } 
#endif
                    //这样也能达到终止的目的 发现请求被取消了则会抛异常
                    //cancellationToken.ThrowIfCancellationRequested();
                }

            }
        }
    }
}