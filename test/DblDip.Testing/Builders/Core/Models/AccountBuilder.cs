using DblDip.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DblDip.Testing.Builders.Core.Models
{
    public class AccountBuilder
    {
        private Account _account;

        public static Account WithDefaults()
        {
            return new Account(default, default, default, default);
        }

        public AccountBuilder(List<Guid> profileIds, Guid userId)
        {
            _account = new Account(profileIds, profileIds.First(), "Quinntyne", userId);
        }

        public Account Build()
        {
            return _account;
        }
    }
}
