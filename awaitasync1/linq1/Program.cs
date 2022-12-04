namespace linq1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[] { 1, 3, 213, 2, 3, 21, 3, 2, 13, 2, 13, 2, 32, 13, 2, 3, 2, 32 };
            IEnumerable<int> b = a.Where(a => a > 10).ToList();
            //var 在JAVASCRIPT里面属于动态类型根据赋值时定 但是NET里面的就不行 根据第一次赋值定类型
            var sa = MyWhere(a, a => a > 10).ToList();
            Console.WriteLine(string.Join(",", MyWhere(a, a => a > 10)));
        }
        static IEnumerable<int> MyWhereY(IEnumerable<int> items, Func<int, bool> func)
        {
            List<int> list = new List<int>();
            foreach (int item in items)
            {
                if (func(item) == true)
                {
                    //这样写的好处数据处理更高 一边获取数据一边处理数据
                    yield return item;
                }
            }
        }
#if true
        /// <summary>
        /// 自定义lambda表达式Where方法
        /// </summary>
        /// <param name="items"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        static IEnumerable<int> MyWhere(IEnumerable<int> items, Func<int, bool> func)
        {
            List<int> list = new List<int>();
            foreach (int item in items)
            {
                if (func(item) == true)
                {
                    list.Add(item);
                }
            }
            return list;
        } 
#endif
    }
}