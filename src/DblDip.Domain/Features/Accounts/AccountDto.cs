using DblDip.Core.Models;
using System;
using System.Collections.Generic;

namespace DblDip.Domain.Features.Accounts
{
    public class AccountDto
    {
        public AccountDto(Guid accountId, Guid defaultProfileId, string name, Guid accountHolderUserId, ICollection<Guid> profileIds)
        {
            AccountId = accountId;
            DefaultProfileId = defaultProfileId;
            Name = name;
            AccountHolderUserId = accountHolderUserId;
            ProfileIds = profileIds;
        }

        public Guid AccountId { get; init; }
        public Guid DefaultProfileId { get; init; }
        public string Name { get; init; }
        public Guid AccountHolderUserId { get; init; }
        public ICollection<Guid> ProfileIds { get; init; }
    }
}
