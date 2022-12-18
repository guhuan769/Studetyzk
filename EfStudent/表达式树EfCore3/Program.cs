using System.Linq.Expressions;
using 表达式树EfCore3;

Expression<Func<Book, bool>> expression = b => b.BookPrice > 5;
Expression<Func<Book, Book, double>> expression1 = (b1, b2) => b1.BookPrice + b2.BookPrice;

Console.WriteLine(1);