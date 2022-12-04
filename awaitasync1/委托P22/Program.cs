namespace 委托P22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            D1 d1 = F1;
            d1();
            d1 = F2;
            d1();

            D2 d2 = Add;

            Action action = F1;
            action();

            Func<int,int,int>  d = Add;
            Func<int,int,string>  f3 = F3;
            Console.WriteLine(d(5,8));
            Console.WriteLine(f3(5,8));
            Action<int,string> action1 = F3;
            action1(1,"SEX");
        }

        static void F1() { Console.WriteLine("我是F1"); }
        static void F2() { Console.WriteLine("我是F2"); }
        static string F3(int i, int j) { return "xxxxx"; }
        static void F3(int i, string j) { Console.WriteLine($"{i}{j}VOID"); }

        static int Add(int i, int j)
        {
            return i * j;
        }
    }

    delegate void D1();
    delegate int D2(int i, int j);
}