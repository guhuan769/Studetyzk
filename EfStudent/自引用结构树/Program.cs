using EFCORETEST2;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;
using 自引用结构树.Model;

namespace 自引用结构树
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            #region 插入数据

            //插入根节点
            OrgUnits orgUnitsRoot = new OrgUnits() { Name = "小欢集团世界总部", };
            OrgUnits orgUnitsAsia = new OrgUnits() { Name = "小欢集团亚太总部", };
            orgUnitsAsia.Parent = orgUnitsRoot;
            orgUnitsRoot.Children.Add(orgUnitsAsia);
            OrgUnits orgAmeraica = new OrgUnits() { Name = "小欢集团美洲总部", };
            orgAmeraica.Parent = orgUnitsRoot;
            orgUnitsRoot.Children.Add(orgAmeraica);
            OrgUnits orgUsa = new OrgUnits() { Name = "小欢美美国", };
            orgUsa.Parent = orgAmeraica;
            orgAmeraica.Children.Add(orgUsa);
            OrgUnits orgCan = new OrgUnits() { Name = "小欢加拿大", };
            orgCan.Parent = orgAmeraica;
            orgAmeraica.Children.Add(orgCan);
            OrgUnits orgChina = new OrgUnits() { Name = "中科集团(中国)", };
            orgChina.Parent = orgUnitsAsia;
            orgUnitsAsia.Children.Add(orgChina);
            OrgUnits orgSg = new OrgUnits() { Name = "中科集团(新加坡)", };
            orgSg.Parent = orgUnitsAsia;
            orgUnitsAsia.Children.Add(orgSg);
            //保存数据库
            using (MyDbDataContext ctx = new MyDbDataContext())
            {
                //ctx.OrgUnits.Add(orgUnitsRoot);
                //await ctx.SaveChangesAsync();
                var org = ctx.OrgUnits.Single(o => o.Parent == null);//寻找根节点
                Console.WriteLine(org.Name);
                var dd = PrintChildren(1, ctx, org).ToList();
            }
            #endregion


        }

        /// <summary>
        /// 缩进打印parent的所有的子节点
        /// </summary>
        /// <param name="identLevel"></param>
        /// <param name="ctx"></param>
        /// <param name="org"></param>
        static IQueryable<OrgUnits> PrintChildren(int identLevel, MyDbDataContext ctx, OrgUnits parent)
        {
            IQueryable<OrgUnits> children = ctx.OrgUnits.Where(x => x.Parent == parent);

            foreach (var child in children)
            {
                //if (identLevel == 0)
                //    Console.WriteLine(new String('\t', identLevel) + parent.Name);
                Console.WriteLine(new String('\t', identLevel) + child.Name);
                PrintChildren(identLevel + 1, ctx, child);//打印以我为父节点的子节点
            }
            return children;
        }
    }
}