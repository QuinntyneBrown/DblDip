using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using DblDip.Core.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace DblDip.Core.UnitTests
{
    public class OfferTests
    {
        [Fact]
        public void Should_CreateOfferFromStreamOfEvents()
        {
            Guid expectedId = Guid.NewGuid();

            var sut = new Offer(new List<IEvent>
            {
                new OfferCreated(expectedId)
            });

            Assert.Equal(expectedId, sut.OfferId);
        }
    }
}
