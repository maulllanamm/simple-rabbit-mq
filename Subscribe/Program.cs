// See https://aka.ms/new-console-template for more information


using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

var queue = "grup_ml";
var exchange = "notification_mabar";

channel.ExchangeDeclare(exchange: exchange,
    type: ExchangeType.Fanout
);

channel.QueueDeclare(queue: queue,
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

channel.QueueBind(queue: queue,
    exchange: exchange,
    routingKey: string.Empty
);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
};
channel.BasicConsume(queue: queue,
    autoAck: true,
    consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();