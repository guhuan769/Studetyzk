using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCORETEST2
{

    /*
     Scaffold-DbContext
     */
    internal class Program
    {

        static async Task Main(string[] args)
        {
            using (MyDbDataContext ctx = new MyDbDataContext())
            {

                //Person person = new Person() { Name = "gh" };
                //ctx.Add(person);
                //var personQuery = ctx.Person;
                //Console.WriteLine(12);
                //await ctx.SaveChangesAsync();

                var a = ctx.Person;
                //获取单一SQL   
                Console.WriteLine(a.ToQueryString());
                foreach (Person item in a)
                {
                    //Console.WriteLine(a.Name);
                }
            }
        }
    }
}