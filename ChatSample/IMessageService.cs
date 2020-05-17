using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatSample
{
    public interface IMessageService
    {
        Task<IList<Message>> GetMessages();

        Task<IList<AggregatedChatState>> GetAggregatedChatStates(); 

        Task AddMessage();
    }
}