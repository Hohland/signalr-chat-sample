using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatSample.Hubs
{
    public class ChatHub : Hub
    {
        private static Random rnd = new Random();
        private static int connected = 0;
        private readonly IMessageService messageService;

        public ChatHub(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public override async Task OnConnectedAsync()
        {
            Interlocked.Increment(ref connected);
            await Clients.All.SendAsync("inMessage", "Tech", $"Connected - {connected}");
            var room = 1;
            await Groups.AddToGroupAsync(Context.ConnectionId, room.ToString());
            await Clients.Caller.SendAsync("aggregation", "[{chatId: string, unreaded: 12}]");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Interlocked.Decrement(ref connected);
            await Clients.All.SendAsync("inMessage", "Tech", $"Connected - {connected}");
        }

        public async Task Send(string name, string message, string room)
        {
            await Clients.OthersInGroup(room).SendAsync("inMessage", name, message);
        }

        public async Task SendAggregation(string name)
        {
            await Clients.Client("name").SendAsync("");
        }
    }
}