using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.Photographers
{
    public class PhotographerDto
    {
        public Guid PhotographerId { get; set; }
        public string Name { get; set; }
        public Email Email { get; set; }
        public string PhoneNumber { get; set; }
        public Location PrimaryLocation { get; set; }
    }
}
