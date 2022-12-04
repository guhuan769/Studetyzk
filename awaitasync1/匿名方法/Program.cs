namespace 匿名方法
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //没有方法名称 只要带delegate 属于匿名方法且有方法体 带返回值使用Func 不带返回值使用Action
            Action action = delegate () { Console.WriteLine("我是AAAA"); };
            action();


            Action action11 = () => Console.WriteLine("action11"); Console.WriteLine("123"); ;
            action11();

            Action<int, string> action1 = delegate (int n, string ns)
            {
                Console.WriteLine($"{n}{ns}我是AAAA");
            };
            action1(8888, "GH");

            Func<int, string> action2 = delegate (int n)
            {
                Console.WriteLine($"{n}我是AAAA");
                return "123";
            };
            string a = action2(8888);
            Console.WriteLine(a);

            //匿名方法可以写成lambda表达式 
            Func<int, int, int> func = (int i, int j) => { return i + j; };
            Console.WriteLine(func(5, 8));

            //也可以将参数类型隐藏
            Func<int, int, int> func1 = (i, j) => { return i + j; };
            Console.WriteLine(func1(5, 8));

            Func<int, int, int> func2 = (i, j) => i + j;
            Console.WriteLine(func2(5, 8));

            Action<int> func3 = i => Console.WriteLine(i); ;
            func3(5);

            //作业
            Action<int> f1 = s => Console.WriteLine(s);
            f1(5);

            Func<int, int> f11 = delegate (int s) { Console.WriteLine(s); return s; };
            f11(888);
            //匿名
            Func<int, bool> fb1 = i => i > 5;
            fb1(999);

            Func<int, bool> afb1 = delegate (int i) { Console.WriteLine(i > 5); return i > 5; };
            afb1(11);
        }
    }
}