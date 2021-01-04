using DblDip.Core.Models;
using DblDip.Domain.Features.Messages;

namespace DblDip.Testing.Builders
{
    public class MessageDtoBuilder
    {
        private MessageDto _messageDto;

        public static MessageDto WithDefaults()
        {
            return new MessageDto();
        }

        public MessageDtoBuilder()
        {
            _messageDto = WithDefaults();
        }

        public MessageDto Build()
        {
            return _messageDto;
        }
    }
}
