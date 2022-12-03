using System;
using System.Data.SqlTypes;

// See https://aka.ms/new-console-template for more information
string filename = @"D:\test\1.txt";
await File.WriteAllTextAsync(filename, "hello");//异步方法写入
Console.WriteLine("Hello, World!");
//读出文件
string text = await File.ReadAllTextAsync(filename);
Console.WriteLine($"{text}");
main();
void main()
{
    Console.WriteLine("132");

}
