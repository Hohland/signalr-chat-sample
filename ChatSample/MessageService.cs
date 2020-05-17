using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatSample
{
    public class MessageService : IMessageService
    {
        public Task AddMessage()
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<AggregatedChatState>> GetAggregatedChatStates()
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Message>> GetMessages()
        {
            throw new System.NotImplementedException();
        }
    }
}