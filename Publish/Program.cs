// See https://aka.ms/new-console-template for more information

using System.Text;
using RabbitMQ.Client;

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

const string message = "Gas Mabar";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: exchange,
    routingKey: "grup_ml",
    basicProperties: null,
    body: body
);

Console.WriteLine($"[x] Sent {message}");
Console.WriteLine($"Press [Enter] to exit");
Console.ReadLine();