using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class MessageBuilder
    {
        private Message _message;

        public static Message WithDefaults()
        {
            return new Message();
        }

        public MessageBuilder()
        {
            _message = WithDefaults();
        }

        public Message Build()
        {
            return _message;
        }
    }
}
