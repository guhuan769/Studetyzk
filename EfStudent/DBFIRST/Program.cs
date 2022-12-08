namespace DBFIRST
{
    /*
     Scaffold-DbContext "Server=.;Database=Demo3;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer
     EF core 的反向工程 
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (GhContext ctx = new GhContext())
            {
                Console.WriteLine(ctx.Users.Count());
            }
        }
    }
}