using Microsoft.EntityFrameworkCore;

namespace EFCoreOneToMany
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                #region 一对多的关系插入数据 EF
                //Article article = new Article();
                //article.Title = "顾欢被平为全世界最牛程序员";
                //article.Content = "小到消息";
                //article.Message = "Message";


                //Comment comment = new Comment();
                //comment.Message = "牛逼大了";
                //article.Comments.Add(comment);

                //Comment comment1 = new Comment();
                //comment1.Message = "牛逼大了1";
                //article.Comments.Add(comment1);

                //Comment comment2 = new Comment();
                //comment2.Message = "吹啦";
                //article.Comments.Add(comment2);
                //ctx.Add(article);
                #endregion
                //ef CORE 顺杆爬 
                //Article article = new Article();
                //article.Title = "顾欢被平为全世界最牛程序员";
                //article.Content = "小到消息";
                //article.Message = "Message";


                //Comment comment = new Comment();
                //comment.Message = "牛逼大了";
                //article.Comments.Add(comment);

                //Comment comment1 = new Comment();
                //comment1.Message = "牛逼大了1";
                //article.Comments.Add(comment1);

                //Comment comment2 = new Comment();
                //comment2.Message = "吹啦";
                //article.Comments.Add(comment2);


                #region 一对多的关系获取数据 关联的时候加Include就可以带出数据 查询的时候不仅差主 有关联的 也查
#if false
                //Article article = ctx.Articles.Single(x => x.Content.Equals("小到消息"));
                //Console.WriteLine($"ID{article.Id} Message {article.Message}");
                //foreach (var item in article.Comments)
                //{
                //    Console.WriteLine($"ID{item.Id} Message {item.Message}");
                //}
                //Include 相当于Join
                Article article = ctx.Articles.Include(a => a.Comments).Single(x => x.Content.Equals("小到消息"));
                Console.WriteLine($"ID{article.Id} Message {article.Message}");
                foreach (var item in article.Comments)
                {
                    Console.WriteLine($"ID{item.Id} Message {item.Message}");
                } 
#endif
                #endregion


                #region 那么现在开始查询Comment 看是否可以带出主表数据
                //Comment comment = ctx.Comments.Include(x=>x.Article).Single(x => x.Id == 4);
                //Console.WriteLine($"Comment Id{comment.Id} Message {comment.Message} Article id {comment.Article.Id} ");
                #endregion

                #region  指定字段查询
                //select(x=>{NEW 一个匿名类型})
                //var article = ctx.Articles.Select(x => new { x.Id, x.Title }).First();
                //Console.WriteLine($"{article.Id} {article.Title}");

                //指定字段多表字段拼接
                //var comment = ctx.Comments.Include(x => x.Article).Select(x => new { x.Id, AID = x.Article.Id }).Single(X => X.Id == 4);
                //Console.WriteLine(comment.Id);
                #endregion


                await ctx.SaveChangesAsync();
            }
        }
    }
}