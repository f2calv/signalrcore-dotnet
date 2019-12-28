using CasCap.Hubs;
using CasCap.Interfaces;
using CasCap.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
namespace CasCap.Pages
{
    //https://docs.microsoft.com/en-us/aspnet/core/signalr/hubcontext?view=aspnetcore-3.1
    public class IndexModel : PageModel
    {
        readonly ILogger<IndexModel> _logger;

        IHubContext<MyHub, IMyHubClient> _hubContext { get; }
        //readonly IHubContext<ChatHub> _hubContext;

        public IndexModel(ILogger<IndexModel> logger,
            //IHubContext<ChatHub> hubContext
            IHubContext<MyHub, IMyHubClient> hubContext
            )
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task OnGet()
        {
            await Task.Delay(0);
            //await _hubContext.Clients.All.SendAsync("SendMessage", new MyMessage { user = "website", message = $"Home page loaded at: {DateTime.Now}" });
            await _hubContext.Clients.All.ReceiveMessage("some message from the website...");
            //await _hubContext.Clients.All.SendAsync("SendMessage", "some message from the website...");
            await _hubContext.Clients.All.ReceiveObject(new MyObject { str = Environment.MachineName, myenum = MyEnum.ABC, val1 = DateTime.UtcNow.Second, val2 = DateTime.UtcNow.Hour * DateTime.UtcNow.Second });
        }
    }
}