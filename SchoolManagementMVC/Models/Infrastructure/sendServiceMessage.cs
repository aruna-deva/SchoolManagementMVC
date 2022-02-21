
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System;

namespace SchoolManagementMVC.Models.Infrastructure
{
    public class sendServiceMessage
    {
        private readonly ILogger _logger;

        public IConfiguration _configuration;

        public ServiceBusClient _client;

        public ServiceBusSender _clientSender;

        public sendServiceMessage(IConfiguration _configuration,
            ILogger<sendServiceMessage> logger)
        {
            _logger = logger;
            var _serviceBusConnectionString= _configuration["ServiceBusConnectionString"];
            string _queueName = _configuration["ServiceBusQueueName"];
            _client = new ServiceBusClient(_serviceBusConnectionString);
            _clientSender = _client.CreateSender(_queueName);
        }
        public async Task SendServiceMessage(serviceMessageData Message)
        {
            var messagePayload = JsonSerializer.Serialize(Message);
            ServiceBusMessage ServiceBusMessageData = new ServiceBusMessage(messagePayload);
            try
            {
                await _clientSender.SendMessageAsync(ServiceBusMessageData);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
