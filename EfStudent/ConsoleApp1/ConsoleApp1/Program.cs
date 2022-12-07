using EFCore;

namespace ConsoleApp1
{
    /*
     Add-Migration Init  初始化
     Update-Database    更改数据库  
     Add-Migration AddBirth
     */
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //using避免资源泄露 myDbContext逻辑上的数据库
            using (MyDbContext myDbContext = new MyDbContext())
            {
                #region 新增
                //Dog dog = new Dog();
                //dog.DogName = "test";
                ////把Dog加入到逻辑上的表里头
                //myDbContext.dog.Add(dog);

                //Dog dog1 = new Dog();
                //dog1.DogName = "test1";
                ////把Dog加入到逻辑上的表里头
                //myDbContext.dog.Add(dog);

                //Book book = new Book()
                //{ AuthorName="gh", Price=3, PubTime = DateTime.Now, Title="wa" };
                //myDbContext.Books.Add(book); 
                #endregion

                #region 查询
                //IQueryable<Book> books = myDbContext.Books.Where(x => x.Id == 1);

                //foreach (var item in books)
                //{
                //    Console.WriteLine(item.Title);
                //}

                //var bookOne = myDbContext.Books.Single(x => x.Title.Equals("wa"));
                //bookOne.Title = "美丽的第一本书";
                //Console.WriteLine(bookOne.PubTime); 
                #endregion

                #region 删除
                //IQueryable<Book> books = myDbContext.Books.Where(x => x.Id == 1);
                var b = myDbContext.Books.Single(x => x.Id == 1);
                myDbContext.Remove(b);
                #endregion
                int row = await myDbContext.SaveChangesAsync();//Update-Database 
                Console.WriteLine($"影响行{row}");
            }
        }
    }
}