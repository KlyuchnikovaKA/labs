// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using Lab5.Network.Common;

public class NetUdpClient : IDisposable
{
    private readonly UdpClient _udpClient = new UdpClient();
    private readonly IPEndPoint _endpoint;

    private readonly int _serverPort;

    public NetUdpClient(Uri serverAddress)
    {
        ServerAddress = serverAddress;

        _endpoint = new IPEndPoint(IPAddress.Parse(serverAddress.Host), serverAddress.Port);
        _serverPort = serverAddress.Port;
    }

    public async Task SendAsync(Command command)
    {
        var dataBytes = command.SerializeCommand();

        await _udpClient.SendAsync(dataBytes, _endpoint);
    }

    public void Dispose()
    {
        _udpClient.Close();
        _udpClient.Dispose();
    }

    public Uri ServerAddress { get; }
}
