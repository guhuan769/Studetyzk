namespace async1
{
     class Program
    {
        static async Task Main(string[] args)
        {
            //注意路径别写错了 小例子 打印当前网站的html
            await DownloadHtmlAsync("https://www.youzack.com/", @"D:\test\1.txt");
            Console.WriteLine("ok");
        }

        static async Task DownloadHtmlAsync(string url, string fileName)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string html = await httpClient.GetStringAsync(url);
                await File.WriteAllTextAsync(fileName, html);
            }

        }
    }
}