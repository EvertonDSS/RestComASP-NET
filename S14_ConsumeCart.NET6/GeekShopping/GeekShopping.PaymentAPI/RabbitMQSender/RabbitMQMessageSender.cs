using GeekShopping.MessageBus;
using GeekShopping.PaymentAPI.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.PaymentAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        private const string exchangeName = "DirectPaymentUpdateExchange";
        private const string paymentEmailUpdateQueueName= "PaymentEmailUpdateQueueName";
        private const string paymentOrderUpdateQueueName= "paymentOrderUpdateQueueName";

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _username = "guest";
        }

        public void SendMessage(BaseMessage message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _username,
                Password = _password,
            };
            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: false);
            channel.QueueDeclare(paymentEmailUpdateQueueName, false, false, false, null);
            channel.QueueDeclare(paymentOrderUpdateQueueName, false, false, false, null);
            channel.QueueBind(paymentEmailUpdateQueueName, exchangeName, "PaymentEmail");
            channel.QueueBind(paymentOrderUpdateQueueName, exchangeName, "PaymentOrder");

            byte[] body = GetMessageAsByteArray(message);
            channel.BasicPublish(exchange: exchangeName, "PaymentEmail", basicProperties: null, body: body);
            channel.BasicPublish(exchange: exchangeName, "PaymentOrder", basicProperties: null, body: body);
            
        }

        private byte[] GetMessageAsByteArray(object message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize<UpdatePaymentResultMessage>((UpdatePaymentResultMessage)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;

        }

        private void CreateConnection()
        {
            try
            {

            }
            catch (Exception)
            {

            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return _connection != null;
           
        }

    }
}
