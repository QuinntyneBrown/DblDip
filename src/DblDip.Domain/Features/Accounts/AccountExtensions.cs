using DblDip.Core.Models;
using System.Linq;

namespace DblDip.Domain.Features
{
    public static class AccountExtensions
    {
        public static AccountDto ToDto(this Account account)
            => new AccountDto(account.AccountId, account.DefaultProfileId, account.Name, account.UserId, account.Profiles.Select(x => x.ProfileId).ToList());
    }
}
