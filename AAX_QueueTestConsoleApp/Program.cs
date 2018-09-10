using Microsoft.ServiceBus.Messaging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;


namespace AAX_QueueTestConsoleApp
{
    class Program
    {
        static void sendMessage()
        {
            string connectionString = "Endpoint=sb://aaxtest1.servicebus.windows.net/;SharedAccessKeyName=AAXTEST1SharedAccess;SharedAccessKey=tqPyCRjcMk5pVzpfFO81aJz8LFqGbWV542ayJCO6GNM=;EntityPath=aaxtest1queue";
            QueueClient queueClient;
            string messageBody = "Test Message " + System.Guid.NewGuid();
 
            BrokeredMessage message = new BrokeredMessage(messageBody);

            queueClient = QueueClient.CreateFromConnectionString(connectionString, ReceiveMode.PeekLock);

            String s = message.GetBody<String>();

            queueClient.Send(message);

            queueClient.Close();

            Console.WriteLine("message written: " + s );
        }

        static void receiveMessage()
        {
            string connectionString = "Endpoint=sb://aaxtest1.servicebus.windows.net/;SharedAccessKeyName=AAXTEST1SharedAccess;SharedAccessKey=tqPyCRjcMk5pVzpfFO81aJz8LFqGbWV542ayJCO6GNM=;EntityPath=aaxtest1queue";
            QueueClient queueClient;
            BrokeredMessage message;

            queueClient = QueueClient.CreateFromConnectionString(connectionString, ReceiveMode.PeekLock);

            message = queueClient.Receive();

            string s = message.GetBody<String>();

            queueClient.Complete(message.LockToken);

            queueClient.Close();

            Console.WriteLine("received message: " + s);
        }
        static void Main(string[] args)
        {
            sendMessage();
            receiveMessage();

            Console.ReadLine();
        }
    }
}
