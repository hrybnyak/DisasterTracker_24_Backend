using Microsoft.AspNetCore.SignalR;

namespace DisasterTracker.BL.Services.SignalR
{
    internal class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.GetHttpContext().Request.Query["userId"];
        }
    }
}
