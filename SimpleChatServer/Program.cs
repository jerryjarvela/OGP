using System.Net.Sockets;
using System.Net;
using System.Text;

class Program
{
    static Socket serverSocket, clientSocket;
    static IPEndPoint ipEndpoint;
    static IPAddress ipAddress;
    static int port = 88;
    static byte[] dataBuffer = new byte[1024];

    static void Main(string[] args)
    {
        ASyncServer();
    }

    private static void SyncServer()
    {
        // Configs
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ipAddress = IPAddress.Parse("91.156.58.156");
        ipEndpoint = new IPEndPoint(ipAddress, port);
        serverSocket.Bind(ipEndpoint);
        serverSocket.Listen(0);
        clientSocket = serverSocket.Accept();

        // Sending message to client
        string msg = "Hello client, from server";
        byte[] Byte = Encoding.UTF8.GetBytes(msg);
        clientSocket.Send(Byte);

        // Read message from client
        byte[] datBuffer = new byte[1024];
        int count = clientSocket.Receive(datBuffer);
        string msgReveice = Encoding.UTF8.GetString(datBuffer, 0, count);
        Console.WriteLine(msgReveice);

        // Close sockets
        Console.ReadKey();
        clientSocket.Close();
        serverSocket.Close();
    }

    private static void ASyncServer()
    {
        // Configs
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ipAddress = IPAddress.Parse("91.156.58.156");
        ipEndpoint = new IPEndPoint(ipAddress, port);
        serverSocket.Bind(ipEndpoint);
        serverSocket.Listen(0);
        clientSocket = serverSocket.Accept();

        // Sending message to client
        string msg = "Hello client, from server";
        byte[] Byte = Encoding.UTF8.GetBytes(msg);
        clientSocket.Send(Byte);

        // Read message from client
        clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallback, null);
        

        // Close sockets
        Console.ReadKey();
        clientSocket.Close();
        serverSocket.Close();
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        int count = clientSocket.EndReceive(ar);
        string msgReceive = Encoding.UTF8.GetString(dataBuffer, 0, (int)count);
        Console.WriteLine(msgReceive);
        clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallback, null);
    }
}
