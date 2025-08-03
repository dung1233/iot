using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TTS.Dto;
using TTS.Service;

// Thêm dòng này để yêu cầu xác thực JWT cho SignalR
public class ChatHub : Hub
{
    private readonly MessageService _messageService;

    public ChatHub(MessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task SendMessage(string username, string message)
    {
        var chatMessage = new ChatMessage
        {
            Username = username,
            Content = message,
            Timestamp = DateTime.Now
        };

        // Lưu tin nhắn
        await _messageService.AddMessageAsync("general", chatMessage);

        // Gửi đến tất cả clients
        await Clients.All.SendAsync("ReceiveMessage", username, message);
    }

    public async Task GetMessages()
    {
        var messages = await _messageService.GetMessagesAsync("general");
        await Clients.Caller.SendAsync("LoadMessages", messages);
    }
}
