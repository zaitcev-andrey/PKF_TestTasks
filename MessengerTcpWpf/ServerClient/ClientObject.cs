using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerClient
{
    public class ClientObject
    {
        public string Id { get; set; }
        public StreamReader Sr { get; set; }
        public StreamWriter Sw { get; set; }


        private TcpClient clientSocket;
        private Server serverObject;

        public ClientObject(TcpClient client, Server serverObj)
        {
            clientSocket = client;
            serverObject = serverObj;

            // идентифицируем каждого клиента уникальным Id
            Id = Guid.NewGuid().ToString();
            // получаем поток для взаимодействия с сервером
            var stream = clientSocket.GetStream();
            // поток для чтения данных
            Sr = new StreamReader(stream);
            // поток для записи данных
            Sw = new StreamWriter(stream);
        }

        // в этом методе реализован протокол для общения с клиентом
        public async Task ChatAsync()
        {
            try
            {
                string? userName = await Sr.ReadLineAsync();
                string? message = $"{userName} вошёл в чат";

                // первым делом посылаем сообщение о входе нового
                // пользователя в чат всем подключенным клиентам
                await serverObject.BroadcastMessageAsync(message, Id);
                Console.WriteLine(message);

                // и теперь в бесконечном цикле будем получать сообщения от клиента
                while (true)
                {
                    try
                    {
                        message = await Sr.ReadLineAsync();
                        // защита от пустого сообщения, когда нажали enter, ничего не вводив
                        if (message == null)
                            continue;
                        message = $"{userName}: {message}";
                        Console.WriteLine(message);
                        await serverObject.BroadcastMessageAsync(message, Id);
                    }
                    catch
                    {
                        message = $"{userName} покинул чат";
                        Console.WriteLine(message);
                        await serverObject.BroadcastMessageAsync(message, Id);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // после break в цикле нужно закрыть подключение для клиента
                serverObject.RemoveConnection(Id);
            }
        }

        // в этом методе закрываются все потоки для клиента
        public void Close()
        {
            Sr.Close();
            Sw.Close();
            clientSocket.Close();
        }
    }
}
