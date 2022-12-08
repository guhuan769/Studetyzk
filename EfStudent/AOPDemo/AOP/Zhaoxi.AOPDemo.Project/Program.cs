// See https://aka.ms/new-console-template for more information
using Zhaoxi.AOPDemo.Project;
using Zhaoxi.AOPDemo.Project.CustomAOP;

try
{
    Console.WriteLine("欢迎来到朝夕教育课堂");

    {
        //Console.WriteLine("*************POPShow**********");
        //POPShow.Show();
        //Console.WriteLine();

        //Console.WriteLine("*************OOPShow**********");
        //OOPShow.Show();
        //Console.WriteLine();

        //Console.WriteLine("*************AOPShow**********");
        //AOPShow.Show();
        //Console.WriteLine();
    }

    {
        CastleAOPShow.Show();
    }
}
catch (Exception)
{

    throw;
}

