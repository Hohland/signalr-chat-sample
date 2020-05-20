using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder().WithUrl("https://localhost:5001/chat")
                .WithAutomaticReconnect()
                .AddJsonProtocol().Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<string, string>("inMessage", (name, text) =>
            {
                Console.WriteLine($"{name}: {text}");
            });

            connection.On<string>("aggregation", (aggregation) =>
            {
                Console.WriteLine(aggregation);
            });

            await connection.StartAsync();

            while (true)
            {
                var line = Console.ReadLine();
                if (line == "q")
                {
                    break;
                }

                await connection.SendAsync("Send", "Console", line, "1");
            }
            await connection.StopAsync();
        }
    }
}
