using StackExchange.Redis;
using System.Text.Json;
using TTS.Dto;

namespace TTS.Service
{
    public class MessageService
    {
        private readonly IDatabase _redis;

        // Dictionary cache - lưu tin nhắn trong RAM
        private static readonly Dictionary<string, List<ChatMessage>> _messageCache = new();

        public MessageService(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        // Lấy tin nhắn
        public async Task<List<ChatMessage>> GetMessagesAsync(string roomId)
        {
            // 1. Kiểm tra cache trước
            if (_messageCache.ContainsKey(roomId))
            {
                return _messageCache[roomId];
            }

            // 2. Không có trong cache -> lấy từ Redis
            var redisKey = $"messages:{roomId}";
            var redisData = await _redis.ListRangeAsync(redisKey);

            var messages = new List<ChatMessage>();
            foreach (var item in redisData)
            {
                var message = JsonSerializer.Deserialize<ChatMessage>(item);
                messages.Add(message);
            }

            // 3. Lưu vào cache
            _messageCache[roomId] = messages;
            return messages;
        }

        // Thêm tin nhắn mới
        public async Task AddMessageAsync(string roomId, ChatMessage message)
        {
            // 1. Thêm vào Redis
            var redisKey = $"messages:{roomId}";
            var messageJson = JsonSerializer.Serialize(message);
            await _redis.ListRightPushAsync(redisKey, messageJson);

            // 2. Thêm vào cache
            if (!_messageCache.ContainsKey(roomId))
            {
                _messageCache[roomId] = new List<ChatMessage>();
            }
            _messageCache[roomId].Add(message);

            // 3. Giới hạn 50 tin nhắn
            if (_messageCache[roomId].Count > 50)
            {
                _messageCache[roomId].RemoveAt(0);
                await _redis.ListTrimAsync(redisKey, -50, -1);
            }
        }
    }
}
