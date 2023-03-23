using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerClient
{
    public class Server
    {
        // создаём серверный сокет для прослушивания
        private TcpListener serverSocket;
        // и список всех клиентов
        private List<ClientObject> clients;

        public Server()
        {
            serverSocket = new TcpListener(IPAddress.Any, 8080);
            clients = new List<ClientObject>();
        }

        // основной метод для прослушивания клиентов
        public async Task ListenAsync()
        {
            try
            {
                serverSocket.Start();
                Console.WriteLine("Сервер запущен, ожидается подключение клиентов...");

                while (true)
                {
                    // получаем клиента после выполнения у него метода Connect(host, port)
                    TcpClient clientSocket = await serverSocket.AcceptTcpClientAsync();

                    ClientObject client = new ClientObject(clientSocket, this);
                    clients.Add(client);
                    Task.Run(client.ChatAsync);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        // в этом методе сообщение, полученное от одного клиента, будет
        // отправлено всем остальным клиентам
        public async Task BroadcastMessageAsync(string message, string id)
        {
            foreach (var client in clients)
            {
                // именно для этого места нам и нужно было Id
                // чтобы не отправить сообщение о подключении самому себе
                if (client.Id != id)
                {
                    await client.Sw.WriteLineAsync(message);
                    await client.Sw.FlushAsync();
                }
            }
        }

        public void RemoveConnection(string id)
        {
            ClientObject? removedClient = clients.FirstOrDefault(cl => cl.Id == id);
            if (removedClient != null)
                clients.Remove(removedClient);
            // и обязательно запускаем метод на очищение потоков записи и чтения у клиента
            removedClient?.Close();
        }

        // метод, который выполняется после закрытия сервера
        public void Disconnect()
        {
            // сначала закрываем всех клиентов
            foreach (var client in clients)
            {
                client.Close();
            }
            // затем останавливаем сервер
            serverSocket.Stop();
        }
    }
}
