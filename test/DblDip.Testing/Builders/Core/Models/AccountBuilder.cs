using DblDip.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DblDip.Testing.Builders
{
    public class AccountBuilder
    {
        private Account _account;

        public static Account WithDefaults()
        {
            return new Account(default, default, default);
        }

        public AccountBuilder(Guid profileId, Guid userId)
        {
            _account = new Account(profileId, "Quinntyne", userId);
        }

        public Account Build()
        {
            return _account;
        }
    }
}
