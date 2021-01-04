using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class BoardBuilder
    {
        private Board _board;

        public static Board WithDefaults()
        {
            return new Board(default);
        }

        public BoardBuilder()
        {
            _board = WithDefaults();
        }

        public Board Build()
        {
            return _board;
        }
    }
}
