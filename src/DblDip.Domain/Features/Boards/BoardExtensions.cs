using DblDip.Core.Models;

namespace DblDip.Domain.Features
{
    public static class BoardExtensions
    {
        public static BoardDto ToDto(this Board board)
        {
            return new BoardDto
            {
                BoardId = board.BoardId
            };
        }
    }
}
