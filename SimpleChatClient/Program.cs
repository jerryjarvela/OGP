using System.Text;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main(string[] args)
    {
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(new IPEndPoint(IPAddress.Parse("91.156.58.156"), 88));

        // Reading the server message
        byte[] data = new byte[1024];
        int count = clientSocket.Receive(data);
        string msgReceive = Encoding.UTF8.GetString(data, 0, count);
        Console.WriteLine(msgReceive);

        while(true)
        {
            // Sending message to server
            string msgSend = Console.ReadLine();
            clientSocket.Send(Encoding.UTF8.GetBytes(msgSend));
        }

        Console.ReadKey();
        clientSocket.Close();
    }
}