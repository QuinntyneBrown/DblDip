using DblDip.Core.Models;
using DblDip.Domain.Features.Conversations;

namespace DblDip.Testing.Builders
{
    public class ConversationDtoBuilder
    {
        private ConversationDto _conversationDto;

        public static ConversationDto WithDefaults()
        {
            return new ConversationDto();
        }

        public ConversationDtoBuilder()
        {
            _conversationDto = WithDefaults();
        }

        public ConversationDto Build()
        {
            return _conversationDto;
        }
    }
}
