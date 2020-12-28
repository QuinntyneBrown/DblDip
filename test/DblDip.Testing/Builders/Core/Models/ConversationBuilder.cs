using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class ConversationBuilder
    {
        private Conversation _conversation;

        public static Conversation WithDefaults()
        {
            return new Conversation();
        }

        public ConversationBuilder()
        {
            _conversation = WithDefaults();
        }

        public Conversation Build()
        {
            return _conversation;
        }
    }
}
