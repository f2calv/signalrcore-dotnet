using CasCap.Interfaces;
using CasCap.Models;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
namespace CasCap
{
    public abstract class ProgramBase
    {
        protected static IMyHubClient clientMethod { get; } = null;//only used for the strongly typed names it provides
        protected static IMyHubServer serverMethod { get; } = null;//only used for the strongly typed names it provides

        protected static bool IsConnected = false;
        protected static HubConnection connection = null;

        protected static async Task Connect2Hub(string url = null)
        {
            url = url ?? "https://localhost:5001/myhub";
            connection = new HubConnectionBuilder()
                .AddJsonProtocol(options =>
                {
                    //options.PayloadSerializerSettings.ContractResolver = new DefaultContractResolver();
                })
                .WithUrl(url/*, HttpTransportType.WebSockets | HttpTransportType.LongPolling*/)
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Information);
                    logging.AddConsole();
                })
                .AddMessagePackProtocol()
                //.AddMessagePackProtocol(options =>
                //{
                //    options.FormatterResolvers = new List<IFormatterResolver> { NativeDateTimeResolver.Instance, StandardResolver.Instance };
                //})
                .WithAutomaticReconnect()
                .Build();

            connection.On<MyObject>(nameof(clientMethod.ReceiveObject), async (obj) =>
            {
                await Task.Delay(0);
                Console.WriteLine($"incoming object: {obj}");
                await Task.Delay(0);
            });
            connection.On<string>(nameof(clientMethod.ReceiveMessage), (message) =>
            {
                var newMessage = $"incoming message: {message}";
                Console.WriteLine(newMessage);
            });
            connection.Closed += async (error) =>
            {
                Debug.Assert(connection.State == HubConnectionState.Disconnected);
                // Notify users the connection has been closed or manually try to restart the connection.
                await Task.Delay(new Random().Next(0, 5) * 1000);
                IsConnected = await ConnectWithRetryAsync();
                //return Task.CompletedTask;
            };
            connection.Reconnecting += error =>
            {
                Debug.Assert(connection.State == HubConnectionState.Reconnecting);
                // Notify users the connection was lost and the client is reconnecting.
                // Start queuing or dropping messages.
                return Task.CompletedTask;
            };
            connection.Reconnected += connectionId =>
            {
                Debug.Assert(connection.State == HubConnectionState.Connected);
                // Notify users the connection was reestablished.
                // Start dequeuing messages queued while reconnecting if any.
                return Task.CompletedTask;
            };

            IsConnected = await ConnectWithRetryAsync();
            if (IsConnected)
            {
                var msg = $"{AppDomain.CurrentDomain.FriendlyName} now connected to hub @ {url}";
                //_logger.LogInformation(msg);
                await connection.InvokeAsync(nameof(serverMethod.SendMessage), $"{msg}, sent via InvokeAsync");
                await connection.SendAsync(nameof(serverMethod.SendMessage), $"{msg}, sent via SendAsync");
            }

            async Task<bool> ConnectWithRetryAsync(CancellationToken token = default)
            {
                // Keep trying to until we can start or the token is canceled.
                while (true)
                {
                    try
                    {
                        await connection.StartAsync(token);
                        Debug.Assert(connection.State == HubConnectionState.Connected);
                        return true;
                    }
                    catch when (token.IsCancellationRequested)
                    {
                        return false;
                    }
                    catch
                    {
                        // Failed to connect, trying again in 5000 ms.
                        Debug.Assert(connection.State == HubConnectionState.Disconnected);
                        await Task.Delay(5000);
                    }
                }
            }
        }


        protected static async Task DelayStartup()
        {
            for (var i = 10; i > 0; i--)
            {
                Console.WriteLine($"{AppDomain.CurrentDomain.FriendlyName} starting in {i} ...");
                await Task.Delay(500);
            }
        }
    }
}