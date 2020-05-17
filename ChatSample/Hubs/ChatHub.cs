using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatSample.Hubs
{
    public class ChatHub : Hub
    {
        private static Random rnd = new Random();
        private readonly IMessageService messageService;

        public ChatHub(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public override async Task OnConnectedAsync()
        {
            var room = 1;
            await Groups.AddToGroupAsync(Context.ConnectionId, room.ToString());
            await Clients.Caller.SendAsync("aggregation", "[{chatId: string, unreaded: 12}]");
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