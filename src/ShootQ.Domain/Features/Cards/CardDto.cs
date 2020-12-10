using System;

namespace ShootQ.Domain.Features.Cards
{
    public class CardDto
    {
        public Guid CardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
