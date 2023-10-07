using Microsoft.VisualBasic.ApplicationServices;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GoldCard
{
    public partial class Form1 : Form
    {
        TcpListener listener;
        TcpClient client;
        int port = 12345;

        List<DataImporter> customers = new List<DataImporter>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
            }
            catch (Exception error) { MessageBox.Show(error.Message, Text); return; }

            StartReceiving();
            //DataImporter.ImportCustomerData();
        }

        public async void StartReceiving()
        {
            try
            {
                client = await listener.AcceptTcpClientAsync();
            }
            catch (Exception error) { MessageBox.Show(error.Message, Text); return; }

            StartReading(client);
        }

        public async void StartReading(TcpClient client)
        {
            byte[] bytes = new byte[1024];

            int n = 0;
            try
            {
                n = await client.GetStream().ReadAsync(bytes, 0, bytes.Length);
            }
            catch (Exception error) { MessageBox.Show(error.Message, Text); return; }

            string receivedData = Encoding.Unicode.GetString(bytes, 0, n); // I stored the converted bytes to a string into a string variable instead of (textBox1.AppendText)
            var verfiedClients = DataImporter.VerfiyCustomerData(receivedData);
            SendToClient(string.Join(",", verfiedClients.Select(c => c.ID)));

            StartReading(client);
        }

        public async void SendToClient(string message)
        {
            byte[] outData = Encoding.Unicode.GetBytes(message);

            try
            {
                await client.GetStream().WriteAsync(outData, 0, outData.Length);
            }
            catch (Exception error) { MessageBox.Show(error.Message, Text); return; }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            /*var _customers = DataImporter.ImportCardData();
            foreach (var customer in _customers)
            {
                textBox1.Add(customer.ToString());
            }*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}