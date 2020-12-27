using System;
using System.Collections.Generic;

namespace DblDip.Domain.Features.Accounts
{
    public class AccountDto
    {
        public Guid AccountId { get; init; }
        public Guid DefaultProfileId { get; init; }
        public string Name { get; init; }
        public Guid AccountHolderUserId { get; init; }
        public ICollection<dynamic> Profiles { get; init; }
    }
}
