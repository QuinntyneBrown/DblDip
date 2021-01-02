using DblDip.Core.Models;
using System;

namespace DblDip.Testing.Builders.Core.Models
{
    public class ProfileBuilder
    {
        private Profile _profile;

        public static Profile WithDefaults()
        {
            return new Profile(default, default, typeof(Profile));
        }

        public ProfileBuilder AddAccountId(Guid accountId)
        {
            _profile.UpdateAccountId(accountId);

            return this;
        }

        public ProfileBuilder()
        {
            _profile = WithDefaults();
        }

        public Profile Build()
        {
            return _profile;
        }
    }
}
