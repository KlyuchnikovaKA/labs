internal class Program
{
    public static void Main(string[] args)
    {
        var listenAdress = new Uri("tcp://0.0.0.0:5555");

        var userApi = new UserApi();

        var tcpServer = new ApiTcpServer(userApi, listenAdress);
        Console.WriteLine($"Start server and listen at {listenAdress}");
        tcpServer.StartAsync().Wait();
    }
}
