namespace AsnycAwait原理1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(1);
            string html = string.Empty;
            using (HttpClient httpClient = new HttpClient())
            {
                //调用异步一定得加await
                html = await httpClient.GetStringAsync("https://www.baidu.com");
                Console.WriteLine(html);
            }
            string txt = " hellow gh";
            string filename = @"D:\test\1.txt";
            await File.WriteAllTextAsync(filename, txt);
            string readText = await File.ReadAllTextAsync(filename);
            Console.WriteLine(readText);

        }
    }
}