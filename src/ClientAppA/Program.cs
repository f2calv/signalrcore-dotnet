using CasCap.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
namespace CasCap;

class Program : ProgramBase
{
    static async Task Main(string[] args)
    {
        //delay client startup to allow for hub to fully launch
        await DelayStartup();

        await Connect2Hub();

        //while (true)
        //{
        //    var msg = $"{AppDomain.CurrentDomain.FriendlyName} message sent @ {DateTime.UtcNow:HH:mm:ss.fff} (echo'd back)...";
        //    await connection.InvokeAsync(nameof(serverMethod.SendMessage), msg + " (InvokeAsync)");//waits for a completion message from the server
        //    await connection.SendAsync(nameof(serverMethod.SendMessage), msg + " (SendAsync)");//fire and forget
        //    await Task.Delay(new Random().Next(0, 5) * 1000);
        //}

        /*
        var msg = $"{AppDomain.CurrentDomain.FriendlyName} message sent @ {DateTime.UtcNow:HH:mm:ss.fff} (echo'd back)...";
        var limit = 1_000;

        var sw = Stopwatch.StartNew();
        for (var i = 0; i < limit; i++)
            await connection.InvokeAsync(nameof(serverMethod.SendMessage), msg);//waits for a completion message from the server
        sw.Stop();
        var count1 = sw.ElapsedMilliseconds;

        sw.Restart();
        for (var i = 0; i < limit; i++)
            await connection.SendAsync(nameof(serverMethod.SendObject), msg);//fire and forget
        var count2 = sw.ElapsedMilliseconds;
        sw.Stop();
        */

        while (true)
        {
            var obj = new MyObject { str = Environment.MachineName, myenum = MyEnum.ABC, val1 = DateTime.UtcNow.Second, val2 = DateTime.UtcNow.Hour * DateTime.UtcNow.Second };
            //await connection.InvokeAsync(nameof(serverMethod.SendMessage), obj);//waits for a completion message from the server
            await connection.SendAsync(nameof(serverMethod.SendObject), obj);//fire and forget
            await Task.Delay(new Random().Next(0, 5) * 1000);
        }

        //todo: performance tests between InvokeAsync & SendObject

        Console.WriteLine("hit any key to exit...");
        Console.ReadKey();
    }
}
