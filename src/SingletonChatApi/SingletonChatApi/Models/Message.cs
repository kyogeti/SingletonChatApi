using System;
using System.ComponentModel.DataAnnotations;

namespace SingletonChatApi.Models
{
    public class Message
    {

        private string _sender;

        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Received { get; set; } = DateTime.UtcNow;
        public string? Sender
        {
            get => _sender;
            set => _sender = value ?? "Anonymous";
        }

        [StringLength(150, MinimumLength = 1, ErrorMessage = "Message should have at least one character and 150 max characters!")]
        public string Content { get; set; }
    }
}