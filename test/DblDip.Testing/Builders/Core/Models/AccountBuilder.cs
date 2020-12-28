using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class AccountBuilder
    {
        private Account _account;

        public static Account WithDefaults()
        {
            return new Account(default, default, default, default);
        }

        public AccountBuilder()
        {
            _account = WithDefaults();
        }

        public Account Build()
        {
            return _account;
        }
    }
}
