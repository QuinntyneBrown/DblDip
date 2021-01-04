using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features
{
    public class PhotographerDto
    {
        public Guid PhotographerId { get; init; }
        public string Name { get; init; }
        public Email Email { get; init; }
        public string PhoneNumber { get; init; }
        public Location PrimaryLocation { get; init; }
    }
}
