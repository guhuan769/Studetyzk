using System.Text;

namespace 线程切换1
{
    internal class Program
    {
        /*
         await 调用的等待期间,。net会把当前的线程返回给线程池,等异步方法调用执行完毕后，框架会从线程池再取出来一个线程执行后续的代码
         */
        static async Task Main(string[] args)
        {
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < 10000; i++)
            //{
            //    sb.Append("XXXXXXXXXXXXXXXXXXXXXXXX");
            //}
            //await File.WriteAllTextAsync(@"D:\test\1.txt", sb.ToString());
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("之前:" + Thread.CurrentThread.ManagedThreadId);
            double r = await CalcAsnc(5000);
            Console.WriteLine("之前:" + Thread.CurrentThread.ManagedThreadId);
        }

        private static async Task<double> CalcAsnc(int v)
        {
            return await Task.Run(() =>
            {

                Console.WriteLine("CalcAsnc:" + Thread.CurrentThread.ManagedThreadId);
                double result = 0;
                Random rand = new Random();
                for (int i = 0; i < v * v; i++)
                {
                    result += rand.NextDouble();
                }
                return result;
            });



        }
    }
}