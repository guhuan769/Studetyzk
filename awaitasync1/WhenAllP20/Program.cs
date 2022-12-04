using System.Threading.Tasks;

namespace WhenAllP20
{
    internal class Program
    {
        /*
         yield 配合linq 可以提升程序效率
         */
        static async Task Main(string[] args)
        {
            //Task<string> task1 = File.ReadAllTextAsync(@"D:\test\1.txt");
            //Task<string> task2 = File.ReadAllTextAsync(@"D:\test\2.txt");
            //Task<string> task3 = File.ReadAllTextAsync(@"D:\test\3.txt");
            //string[] strs = await Task.WhenAll(task1, task2, task3);
            //foreach (var item in strs)
            //{
            //    Console.WriteLine(item);
            //}
            string[] files = Directory.GetFiles(@"D:\test");
            Task<int>[] counts = new Task<int>[files.Length];
            int i = 0;
            foreach (string file in files)
            {
                Task<int> task = ReadCharCount(file);
                counts[i] = task;
                i++;
            }
            int[] c = await Task.WhenAll(counts);
            Console.WriteLine(c.Sum());

            IEnumerable<string> aa = Test();
            foreach (var item in aa)
            {
                Console.WriteLine(item);
            }
            //测试yield
        }

        static  IEnumerable<string> Test()
        {
            yield return "123";
            yield return "321";
            yield return "gh";
        }

        //读一个文件的字符个数
        static async Task<int> ReadCharCount(string filename)
        {
            string s = await File.ReadAllTextAsync(filename);
            return s.Length;
        }
    }
}