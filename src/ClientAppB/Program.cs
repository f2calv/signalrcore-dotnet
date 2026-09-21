using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
namespace CasCap;

class Program : ProgramBase
{
    private static async Task Main(string[] args)
    {
        //delay client startup to allow for hub to fully launch
        await DelayStartup();

        await Connect2Hub();

        while (true)
        {
            var msg = $"{AppDomain.CurrentDomain.FriendlyName} message sent @ {DateTime.UtcNow:HH:mm:ss.fff} (echo'd back)...";
            await connection.InvokeAsync(nameof(serverMethod.SendMessage), msg + " (InvokeAsync)");//waits for a completion message from the server
            await connection.SendAsync(nameof(serverMethod.SendMessage), msg + " (SendAsync)");//fire and forget
            await Task.Delay(RandomNumberGenerator.GetInt32(0, 5) * 1000);
        }
    }
}
