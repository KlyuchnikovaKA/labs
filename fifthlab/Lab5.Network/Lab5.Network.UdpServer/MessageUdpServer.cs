// See https://aka.ms/new-console-template for more information
using Lab5.Network.Common;

internal class MessageUdpServer : UdpServerBase, IMessageApi
{
    public MessageUdpServer(Uri listenAddress) : base(listenAddress)
    {
    }

    public Task<bool> SendMessage(string message)
    {
        Console.WriteLine($"MESSAGE: {message}");

        return Task.FromResult(true);
    }

    protected override async Task ProcessCommandAsync(Command command)
    {
        var commandCode = (CommandCode)command!.Code;
        Console.WriteLine($"+ command: {commandCode}");

        switch (commandCode)
        {
            case CommandCode.SendMessage:
                await SendMessage(command.Arguments["Data"]?.ToString() ?? string.Empty);
                break;
            case CommandCode.OrderPizza:
                var pizzaType = command.Arguments["Data"]?.ToString() ?? string.Empty;
                await OrderVideo(pizzaType); // Обработка команды заказа пиццы
                break;

            default:
                Console.WriteLine("Неизвестная команда.");
                break;
        }
        //await SendMessage(command.Arguments["Data"]?.ToString() ?? string.Empty);
    }
    public Task<bool> OrderVideo(string pizzaType)
    {
        string orderMessage = $"Прислано сообщение: {pizzaType}.";
        Console.WriteLine(orderMessage);
        return Task.FromResult(true);
    }
}