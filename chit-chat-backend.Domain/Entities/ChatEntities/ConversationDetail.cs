using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chit_chat_backend.Domain.Entities.ChatEntities
{
    public class ConversationDetail:BaseEntity
    {
        public Guid ConversationId { get; set; }
        public string userId { get; set; }
        public Conversation Conversation { get; set; }  
        public UserApplication User { get; set; }
    }
}
