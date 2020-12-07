using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SingletonChatApi.Models;

namespace SingletonChatApi.Services
{
    public class MessageService : IHostedService, IDisposable
    {
        private List<Message> _messages;
        private Timer _timer;
        private ILogger<MessageService> _logger;

        public MessageService(ILogger<MessageService> logger)
        {
            _messages = new List<Message>();
            _logger = logger;
        }

        public void InsertMessage(Message message)
        {
            _messages.Add(message);
        }

        public IEnumerable<Message> GetMessages() => _messages;


        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(15));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation(
                $"Deleting {_messages.Count} message(s)...");
            _messages = new List<Message>();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}