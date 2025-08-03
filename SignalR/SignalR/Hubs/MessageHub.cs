using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string sender, string message)
        {
            // Gửi message tới tất cả client đang kết nối
            await Clients.All.SendAsync("ReceiveMessage", sender, message);
        }
    }
}
