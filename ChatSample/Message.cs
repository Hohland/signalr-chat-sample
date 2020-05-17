using System;

namespace ChatSample
{
    public class Message
    {
        public long AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt  { get; set; }
    }
}