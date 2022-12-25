// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

var connFactory = new ConnectionFactory();
connFactory.HostName = "localhost";//127.0.0.1
connFactory.DispatchConsumersAsync = true;
var connection = connFactory.CreateConnection();//创建链接

while (true)
{
    using var channel = connection.CreateModel();
    var prop = channel.CreateBasicProperties();
    prop.DeliveryMode = 2;
    string exchangeName = "Exchange1";
    //声明交换机
    channel.ExchangeDeclare(exchangeName, "direct");
    byte[] buffer = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
    channel.BasicPublish(exchangeName, routingKey: "key1", mandatory: true, basicProperties: prop, body: buffer);
    Console.WriteLine("OK"+DateTime.Now);
    Thread.Sleep(1000);
}