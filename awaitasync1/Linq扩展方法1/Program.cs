namespace Linq扩展方法1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee() { Id = 1, Name = "姓名1", Age = 28, Gender = true, Salary = 90000 });
            employees.Add(new Employee() { Id = 2, Name = "姓名2", Age = 18, Gender = true, Salary = 7777 });
            employees.Add(new Employee() { Id = 3, Name = "姓名3", Age = 8, Gender = true, Salary = 90000 });
            employees.Add(new Employee() { Id = 4, Name = "姓名4", Age = 18, Gender = true, Salary = 7000 });
            employees.Add(new Employee() { Id = 5, Name = "姓名5", Age = 22, Gender = true, Salary = 80000 });
            employees.Add(new Employee() { Id = 6, Name = "姓名6", Age = 33, Gender = true, Salary = 3333 });
            employees.Add(new Employee() { Id = 7, Name = "姓名7", Age = 44, Gender = true, Salary = 5000 });
            employees.Add(new Employee() { Id = 8, Name = "姓名8", Age = 22, Gender = true, Salary = 5555 });
            employees.Add(new Employee() { Id = 9, Name = "姓名9", Age = 19, Gender = true, Salary = 4444 });
            //获取年龄大于30 Linq中Count性能没有Any高 因为 Count 需要数完 然而any 只要有1条就停止循环返回true
            Console.WriteLine($"{string.Join(",", employees.Where(x => x.Age > 30).ToList())} 总条数 {employees.Count(e => e.Age > 30)}");
            //Console.WriteLine($"{string.Join(",", employees.Single())} 总条数 {employees.Count(e => e.Age > 30)}");

            //根据年龄进行分组 
            var aa = employees.GroupBy(x => x.Age);

            foreach (var item in aa)
            {
                Console.WriteLine($"每组人数{item.Key}");
                Console.WriteLine($"每组人数{employees.Where(x => x.Age == Convert.ToInt32(item.Key)).ToList().Count} {item.Max(e => e.Salary)}");

                //可以让每组的集合遍历处理
                foreach (var item1 in item)
                {
                    Console.WriteLine($"每组人数{item1}");
                }
            }
        }
    }
}