using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var connFactory = new ConnectionFactory();
connFactory.HostName = "localhost";//127.0.0.1
connFactory.DispatchConsumersAsync = true;
var connection = connFactory.CreateConnection();//创建链接
string exchangeName = "Exchange1";
string queueName = "queue1";
string routingKey = "key1";
using var channel = connection.CreateModel();
channel.ExchangeDeclare(exchangeName, "direct");
channel.QueueDeclare(queueName, durable: true, exclusive: false, arguments: null);
channel.QueueBind(queue: queueName, exchangeName, routingKey);
//创建一个消费者 consumer
AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
consumer.Received += Consumer_Received;
channel.BasicConsume(queueName, autoAck: false, consumer: consumer);
Console.WriteLine("按回车键退出");
Console.ReadLine();
async Task Consumer_Received(object sender, BasicDeliverEventArgs _event)
{
	try
	{
		byte[] bytes = _event.Body.ToArray();
		string text = Encoding.UTF8.GetString(bytes);
		Console.WriteLine(DateTime.Now + "收到消息:" + text);
		//DeliveryTag 就是消息的编号
		channel.BasicAck(_event.DeliveryTag, multiple: false);
		await Task.Delay(800);
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
		channel.BasicReject(_event.DeliveryTag, true);
	}
}