namespace 没有async的异步方法
{
    internal class Program
    {
        /*
            async方法缺点:
        1 异步方法会生成一个类,运行效率没有普通方法高；
        2 可能会占用非常多的线程；
        返回值为Task的不一定用async 标注async只是为了让我们更好的await而已；
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        static Task<string> ReadAsync(int num)
        {
            if (num == 0)
                return File.ReadAllTextAsync(@"D:\test\1.txt");
            else
                throw new Exception();
        }
    }
}