using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{

    [Owned]
    public class ServiceReference
    {
        public Guid ServiceId { get; set; }
        public ServiceReference()
        {

        }

        public ServiceReference(Guid serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
