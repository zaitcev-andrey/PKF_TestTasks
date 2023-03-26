using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string? UserName { get; set; }

        public string? Message { get; set; }

        private TcpClient client;
        private StreamReader? sr;
        private StreamWriter? sw;
        private bool isSend;

        private StringBuilder stringBuilder;
        public MainWindow()
        {
            Host = "127.0.0.1";
            Port = 8080;
            UserName = "";

            stringBuilder = new StringBuilder();

            client = new TcpClient();
            sr = null;
            sw = null;
            isSend = false;

            InitializeComponent();

            // для Binding
            this.DataContext = this;
        }

        async Task SendMessageAsync(StreamWriter sw)
        {
            // первым делом отправляем имя
            await sw.WriteLineAsync(UserName);
            await sw.FlushAsync();

            while (true)
            {
                // добавим небольшую задержку, чтобы поберечь ресурсы
                await Task.Delay(10);
                if(isSend)
                {
                    if(!string.IsNullOrEmpty(Message))
                    {
                        await sw.WriteLineAsync(Message);
                        await sw.FlushAsync();
                    }
                    
                    isSend = false;
                }                
            }
        }

        // этот метод принимает сообщения от сервера после его метода BroadcastMessageAsync
        async Task ReceiveMessageAsync(StreamReader sr)
        {
            while (true)
            {
                try
                {
                    string? message = await sr.ReadLineAsync();
                    if (!string.IsNullOrEmpty(message))
                    {
                        stringBuilder.AppendLine(message);
                        // обязательно оборачиваем в Dispatcher, чтобы мы обратились к UI компоненту в главном потоке
                        Dispatcher.Invoke(() => this.textBoxAllMessages.Text = stringBuilder.ToString());
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(Host) || Port == 0 || string.IsNullOrEmpty(UserName))
                {
                    stringBuilder.AppendLine("Не хватает данных для подключения, проверьте их и попробуйте снова");
                    this.textBoxAllMessages.Text = stringBuilder.ToString();
                    return;
                }

                // после выполнения Connect сервер принимает клиента методом AcceptTcpClientAsync()
                client.Connect(Host, Port);

                var stream = client.GetStream();
                sr = new StreamReader(stream);
                sw = new StreamWriter(stream);

                if (sr is null || sw is null)
                    return;

                // запускаем методы на получение и отправку сообщений
                Task.Run(() => ReceiveMessageAsync(sr));
                Task.Run(() => SendMessageAsync(sw));
            }
            catch (Exception ex)
            {
                stringBuilder.AppendLine(ex.Message);
                this.textBoxAllMessages.Text = stringBuilder.ToString();
            }
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            Message = this.textBoxMessage.Text;
            if (!string.IsNullOrEmpty(Message))
            {
                isSend = true;
                stringBuilder.AppendLine($"{UserName}: " + Message);
                this.textBoxAllMessages.Text = stringBuilder.ToString();
                this.textBoxMessage.Clear();
            }
        }
    }
}
