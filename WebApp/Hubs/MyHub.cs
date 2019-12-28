using CasCap.Interfaces;
using CasCap.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
namespace CasCap.Hubs
{
    public class MyHub : Hub<IMyHubClient>, IMyHubServer
    {
        //calling SendMessage invokes ReceiveMessage on the client
        public Task SendMessage(string message)
        {
            return Clients.AllExcept(Context.ConnectionId).ReceiveMessage($"user says '{message}'");
        }

        //calling SendObject invokes ReceiveObject on the client
        public Task SendObject(MyObject obj)
        {
            return Clients.AllExcept(Context.ConnectionId).ReceiveObject(obj);
        }

        public async Task Broadcast(string message)
        {
            await Task.Delay(0);
            await Clients.All.ReceiveMessage(message);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}