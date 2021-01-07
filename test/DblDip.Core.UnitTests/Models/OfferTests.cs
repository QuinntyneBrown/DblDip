using DblDip.Core.DomainEvents;
using DblDip.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DblDip.Core.UnitTests
{
    public class OfferTests
    {

        public OfferTests()
        {

        }

        [Fact]
        public void Should_CreateOfferFromStreamOfEvents()
        {
            Guid expectedId = Guid.NewGuid();

            var events = new List<object>
            {
                new OfferCreated(expectedId)
            };

            var sut = new Offer(events);

            
            Assert.Equal(expectedId, sut.OfferId);
        }
    }
}
