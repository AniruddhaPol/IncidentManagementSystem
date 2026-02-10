using Azure.Storage.Queues;
using System.Text.Json;

namespace IncidentManagementApi.Services
{
    public class QueueService
    {
        private readonly QueueClient _queue;

        public QueueService(string connectionString, string queueName)
        {
            _queue = new QueueClient(connectionString, queueName);
            _queue.CreateIfNotExists();
        }

        public async Task EnqueueAsync<T>(T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            await _queue.SendMessageAsync(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json)));
        }
    }
}
