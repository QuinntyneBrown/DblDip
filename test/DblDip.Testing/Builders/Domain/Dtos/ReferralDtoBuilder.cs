using DblDip.Domain.Features.Referrals;

namespace DblDip.Testing.Builders
{
    public class ReferralDtoBuilder
    {
        private ReferralDto _referralDto;

        public static ReferralDto WithDefaults()
        {
            return new ReferralDto();
        }

        public ReferralDtoBuilder()
        {
            _referralDto = new ReferralDto();
        }

        public ReferralDto Build()
        {
            return _referralDto;
        }
    }
}
