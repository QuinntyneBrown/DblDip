using DblDip.Core.Models;
using DblDip.Domain.Features.Messages;

namespace DblDip.Domain.Features
{
    public static class MessageExtensions
    {
        public static MessageDto ToDto(this Message message)
        {
            return new MessageDto
            {

            };
        }
    }
}
