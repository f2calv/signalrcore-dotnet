using CasCap.Models;
using System.Threading.Tasks;
namespace CasCap.Interfaces
{
    /// <summary>
    /// These are the methods that we can invoke on the server.
    /// </summary>
    public interface IMyHubServer
    {
        Task SendMessage(string message);
        Task SendObject(MyObject obj);
    }

    /// <summary>
    /// These are the methods that we can invoke on the client.
    /// </summary>
    public interface IMyHubClient
    {
        Task ReceiveMessage(string message);
        Task ReceiveObject(MyObject obj);
    }
}